using CanvasWP.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace CanvasWP
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ConversationViewModel> Conversations { get; private set; }
        public ObservableCollection<CourseViewModel> Courses { get; private set; }
        public ObservableCollection<StreamViewModel> StreamItems { get; private set; }
        public ProfileViewModel Profile { get; private set; }
        
        private CanvasAPI _api;
        private IsolatedStorageSettings _settings;
        private Dictionary<long, CourseViewModel> _courseDict = new Dictionary<long, CourseViewModel>();
        private bool _coursesLoaded = false;
        
        private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        private static Regex _newlineRegex = new Regex("[\r\n]+", RegexOptions.Compiled);
        private static Regex _spaceRegex = new Regex(" +", RegexOptions.Compiled);

        private string CleanHTML(String source)
        {
            if (source == null) return null;
            string cleaned = _htmlRegex.Replace(source, string.Empty);
            cleaned = _newlineRegex.Replace(cleaned, " ");
            cleaned = _spaceRegex.Replace(cleaned, " ");
            return cleaned.TrimStart(' ');
        }

        private string _baseURL;
        public string BaseURL
        {
            get
            {
                if (_baseURL == null && this._settings.Contains("BaseURL"))
                    _baseURL = this._settings["BaseURL"] as string;
                if (_baseURL == null)
                    _baseURL = "https://canvas.instructure.com";
                return _baseURL;
            }
            set
            {
                if (ConditionallyChangeProperty(value, ref _baseURL, "BaseURL"))
                {
                    this._api.BaseURL = _baseURL;
                    this._settings["BaseURL"] = _baseURL;
                }
            }
        }

        private string _accessToken;
        public string AccessToken
        {
            get
            {
                if (_accessToken == null && this._settings.Contains("AccessToken"))
                    _accessToken = this._settings["AccessToken"] as string;
                return _accessToken;
            }
            set 
            {
                if (ConditionallyChangeProperty(value, ref _accessToken, "AccessToken"))
                {
                    this._api.AccessToken = _accessToken;
                    this._settings["AccessToken"] = _accessToken;
                    NotifyPropertyChanged("IsLoggedIn");
                }
            }
        }

        private long? _userID;
        public long? UserID
        {
            get
            {
                if (_userID == null && this._settings.Contains("UserID"))
                    _userID = (long?)this._settings["UserID"];
                return _userID;
            }
            set
            {
                if (ConditionallyChangeProperty(value, ref _userID, "UserID"))
                {
                    this._api.UserID = _userID;
                    this._settings["UserID"] = _userID;
                }
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !String.IsNullOrWhiteSpace(AccessToken);
            }
        }

        private bool _isDataLoaded;
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            private set { ConditionallyChangeProperty(value, ref _isDataLoaded, "IsDataLoaded"); }
        }

        private int _loadCount = 0;
        private int LoadCount
        {
            get { return _loadCount; }
            set
            {
                _loadCount = value;
                NotifyPropertyChanged("IsLoading");
            }
        }
        public bool IsLoading
        {
            get { return _loadCount != 0; }
        }

        public MainViewModel()
        {
            this._settings = IsolatedStorageSettings.ApplicationSettings;

            this.Conversations = new ObservableCollection<ConversationViewModel>();
            this.Courses = new ObservableCollection<CourseViewModel>();
            this.StreamItems = new ObservableCollection<StreamViewModel>();
            this.Profile = new ProfileViewModel();

            this._api = new CanvasAPI(this.BaseURL, this.AccessToken);

            WireUpApiEvents();
        }

        private void WireUpApiEvents()
        {
            _api.GetAccessTokenCompleted += new AsyncCompletedEventHandler(_api_GetAccessTokenCompleted);
            _api.GetConversationsCompleted += new AsyncCompletedEventHandler<List<Conversation>>(_api_GetConversationsCompleted);
            _api.GetMoreConversationsCompleted += new AsyncCompletedEventHandler<List<Conversation>>(_api_GetMoreConversationsCompleted);
            _api.GetCoursesCompleted += new AsyncCompletedEventHandler<List<Course>>(_api_GetCoursesCompleted);
            _api.GetActivityStreamCompleted += new AsyncCompletedEventHandler<List<object>>(_api_GetActivityStreamCompleted);
            _api.GetMoreActivityStreamCompleted += new AsyncCompletedEventHandler<List<object>>(_api_GetMoreActivityStreamCompleted);
            _api.GetConversationCompleted += new AsyncCompletedEventHandler<Conversation>(_api_GetConversationCompleted);
            _api.GetProfileCompleted += new AsyncCompletedEventHandler<Profile>(api_GetProfileCompleted);
        }

        public void RetrieveAccessToken(string tempCode)
        {
            LoadCount++;
            _api.GetAccessTokenAsync(tempCode);
        }

        void _api_GetAccessTokenCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error == null)
                {
                    this.AccessToken = _api.AccessToken;
                    this.UserID = _api.UserID;
                }
                else
                {
                    MessageBox.Show("Error retrieving access token: " + e.Error.Message);
                }
            });
        }

        public void Deauthorize()
        {
            this.AccessToken = "";
        }

        public void LoadData()
        {
            if (this.IsDataLoaded) return;
            LoadConversations();
            LoadCourses();
            LoadProfile();
            LoadActivityStream();
            this.IsDataLoaded = true;
        }

        public void LoadConversations()
        {
            LoadCount++;
            _api.GetConversationsAsync();
        }

        void _api_GetConversationsCompleted(object sender, AsyncCompletedEventArgs<List<Conversation>> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    this.Conversations.Clear();
                    if (e.Result != null)
                    {
                        foreach (Conversation c in e.Result)
                        {
                            ConversationViewModel m = new ConversationViewModel();
                            m.Id = c.Id;
                            m.AvatarURL = c.AvatarUrl;
                            m.LastMessage = c.LastMessage;
                            m.Participants = String.Join(", ", (from User u in c.Participants
                                                                where u.Id != UserID
                                                                select u.Name));
                            this.Conversations.Add(m);
                        }
                    }
                }
            });
        }

        public void LoadMoreConversations()
        {
            if (LoadCount == 0 && _api.HasMoreConversations)
            {
                LoadCount++;
                _api.GetMoreConversationsAsync();
            }
        }

        void _api_GetMoreConversationsCompleted(object sender, AsyncCompletedEventArgs<List<Conversation>> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    if (e.Result != null)
                    {
                        foreach (Conversation c in e.Result)
                        {
                            ConversationViewModel m = new ConversationViewModel();
                            m.Id = c.Id;
                            m.AvatarURL = c.AvatarUrl;
                            m.LastMessage = c.LastMessage;
                            m.Participants = String.Join(", ", (from User u in c.Participants
                                                                where u.Id != UserID
                                                                select u.Name));
                            this.Conversations.Add(m);
                        }
                    }
                }
            });
        }

        public void LoadCourses()
        {
            LoadCount++;
            _api.GetCoursesAsync();
        }

        void _api_GetCoursesCompleted(object sender, AsyncCompletedEventArgs<List<Course>> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    this.Courses.Clear();
                    this._courseDict.Clear();
                    if (e.Result != null)
                    {
                        foreach (Course c in e.Result)
                        {
                            CourseViewModel m = new CourseViewModel();
                            m.Id = c.Id;
                            m.Name = c.Name;
                            m.CourseCode = c.CourseCode;
                            this.Courses.Add(m);
                            this._courseDict.Add(m.Id, m);
                        }
                    }
                }

                _coursesLoaded = true;
                foreach (var item in StreamItems.Where(i => i.CourseId.HasValue))
                {
                    string context = _courseDict[item.CourseId.Value].CourseCode;
                    if (item.UpdatedAt.HasValue)
                        context += " - " + item.UpdatedAt.Value.ToLongDateString();
                    item.Context = context;
                }

            });
        }

        public void LoadActivityStream()
        {
            LoadCount++;
            _api.GetActivityStreamAsync();
        }

        void _api_GetActivityStreamCompleted(object sender, AsyncCompletedEventArgs<List<object>> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    this.StreamItems.Clear();
                    if (e.Result != null)
                    {
                        PopulateActivityStreamItems(e.Result);
                    }
                }
            });
        }

        public void LoadMoreActivityStream()
        {
            if (LoadCount == 0 && _api.HasMoreActivityStream)
            {
                LoadCount++;
                _api.GetMoreActivityStreamAsync();
            }
        }

        void _api_GetMoreActivityStreamCompleted(object sender, AsyncCompletedEventArgs<List<object>> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    if (e.Result != null)
                    {
                        PopulateActivityStreamItems(e.Result);
                    }
                }
            });
        }

        private void PopulateActivityStreamItems(List<object> items)
        {
            foreach (object obj in items)
            {
                StreamViewModel model = new StreamViewModel();
                StreamItemBase item = (StreamItemBase)obj;
                model.Id = item.Id;
                model.ItemType = (StreamViewModel.ItemTypes)Enum.Parse(typeof(StreamViewModel.ItemTypes), item.TypeString, true);
                model.ContextType = item.ContextType;
                model.CreatedAt = item.CreatedAt;
                model.UpdatedAt = item.UpdatedAt;
                model.CourseId = item.CourseId;

                if (_coursesLoaded && model.CourseId.HasValue)
                {
                    string context = _courseDict[model.CourseId.Value].CourseCode;
                    if (model.UpdatedAt.HasValue)
                        context += " - " + model.UpdatedAt.Value.ToLongDateString();
                    model.Context = context;
                }

                switch (item.ItemType)
                {
                    case StreamItemBase.ItemTypes.Conversation:
                        StreamItemConversation sic = (StreamItemConversation)obj;
                        model.InnerId = sic.ConversationId;
                        model.Title = "...";
                        model.Message = "...";
                        LoadCount++;
                        _api.GetConversationAsync(sic.ConversationId, model);
                        break;
                    case StreamItemBase.ItemTypes.Message:
                        StreamItemMessage sim = (StreamItemMessage)obj;
                        model.Title = item.Title;
                        model.Message = CleanHTML(item.Message);
                        break;
                    //case StreamItemBase.ItemTypes.Submission:
                    //    StreamItemSubmission sis = (StreamItemSubmission)obj;
                    //    break;
                    default:
                        model.Title = item.Title;
                        model.Message = CleanHTML(item.Message);
                        break;
                }
                this.StreamItems.Add(model);
            }
        }

        public void LoadConversation(long id, ConversationDetailsViewModel model)
        {
            LoadCount++;
            _api.GetConversationAsync(id, model);
        }

        void _api_GetConversationCompleted(object sender, AsyncCompletedEventArgs<Conversation> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadCount--;

                if (e.Error != null) return;
                if (e.UserState is StreamViewModel)
                {
                    StreamViewModel model = e.UserState as StreamViewModel;
                    model.Title = String.Join(", ", (from u in e.Result.Participants
                                                     where u.Id != UserID
                                                     select u.Name));
                    model.Message = CleanHTML(e.Result.LastMessage);
                }
                else if (e.UserState is ConversationDetailsViewModel)
                {
                    ConversationDetailsViewModel model = e.UserState as ConversationDetailsViewModel;
                    model.Participants = String.Join(", ", (from u in e.Result.Participants
                                                            where u.Id != UserID
                                                            select u.Name));
                }
            });
        }

        public void LoadProfile()
        {
            LoadCount++;
            _api.GetProfileAsync("self");
        }

        void api_GetProfileCompleted(object sender, AsyncCompletedEventArgs<Profile> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                else
                {
                    if (e.Result != null)
                    {
                        Profile.AvatarURL = e.Result.AvatarUrl;
                        Profile.Domain = _api.Domain ?? " ";
                        Profile.PrimaryEmail = e.Result.PrimaryEmail ?? " ";
                        Profile.Id = e.Result.Id;
                        Profile.LoginId = e.Result.LoginId ?? " ";
                        Profile.Name = e.Result.Name ?? " ";
                    }
                }

                LoadCount--;
            });
        }

    }
}