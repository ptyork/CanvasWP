using System;

namespace CanvasWP
{
    public class ConversationDetailsViewModel : ViewModelBase
    {
        public ConversationDetailsViewModel()
        {
            App.ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoading")
            {
                NotifyPropertyChanged("IsLoading");
            }
        }
        
        public bool IsLoading
        {
            get
            {
                return App.ViewModel.IsLoading;
            }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { ConditionallyChangeProperty(value, ref _id, "Id"); }
        }

        private string _avatarURL;
        public string AvatarURL
        {
            get { return _avatarURL; }
            set { ConditionallyChangeProperty(value, ref _avatarURL, "AvatarURL"); }
        }

        private string _lastMessage;
        public string LastMessage
        {
            get { return _lastMessage; }
            set { ConditionallyChangeProperty(value, ref _lastMessage, "LastMessage"); }
        }

        private string _participants;
        public string Participants
        {
            get { return _participants; }
            set { ConditionallyChangeProperty(value, ref _participants, "Participants"); }
        }
    }
}
