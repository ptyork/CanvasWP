using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CanvasWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ApplicationBarIconButton _createMessageButton = new ApplicationBarIconButton();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            ((PanoramaItem)MainPanorama.Items[0]).Visibility = System.Windows.Visibility.Collapsed;
            ((PanoramaItem)MainPanorama.Items[1]).Visibility = System.Windows.Visibility.Collapsed;
            ((PanoramaItem)MainPanorama.Items[2]).Visibility = System.Windows.Visibility.Collapsed;
            ((PanoramaItem)MainPanorama.Items[3]).Visibility = System.Windows.Visibility.Collapsed;
            ((PanoramaItem)MainPanorama.Items[4]).Visibility = System.Windows.Visibility.Collapsed;

            DataContext = App.ViewModel;

            _createMessageButton.IconUri = new Uri("/Images/appbar.feature.email.rest.png", UriKind.Relative);
            _createMessageButton.Text = "Create Message";
            _createMessageButton.Click += new EventHandler(_createMessageButton_Click);

            HandleLogInOut();
        }

        private bool _pageEventsAttached = false;
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_pageEventsAttached)
            {
                App.ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);
                AttachVerticalScrollEvent(StreamListBox, new EventHandler<VisualStateChangedEventArgs>(StreamListBox_CurrentStateChanging));
                AttachVerticalScrollEvent(ConversationsListBox, new EventHandler<VisualStateChangedEventArgs>(ConversationsListBox_CurrentStateChanging));
                _pageEventsAttached = true;
            }
        }

        private void HandleLogInOut()
        {
            if (App.ViewModel.IsLoggedIn)
            {
                ((PanoramaItem)MainPanorama.Items[0]).Visibility = System.Windows.Visibility.Collapsed;
                ((PanoramaItem)MainPanorama.Items[1]).Visibility = System.Windows.Visibility.Visible;
                ((PanoramaItem)MainPanorama.Items[2]).Visibility = System.Windows.Visibility.Visible;
                ((PanoramaItem)MainPanorama.Items[3]).Visibility = System.Windows.Visibility.Visible;
                ((PanoramaItem)MainPanorama.Items[4]).Visibility = System.Windows.Visibility.Visible;
                MainPanorama.DefaultItem = 1;
                App.ViewModel.LoadData();
            }
            else
            {
                ((PanoramaItem)MainPanorama.Items[0]).Visibility = System.Windows.Visibility.Visible;
                ((PanoramaItem)MainPanorama.Items[1]).Visibility = System.Windows.Visibility.Collapsed;
                ((PanoramaItem)MainPanorama.Items[2]).Visibility = System.Windows.Visibility.Collapsed;
                ((PanoramaItem)MainPanorama.Items[3]).Visibility = System.Windows.Visibility.Collapsed;
                ((PanoramaItem)MainPanorama.Items[4]).Visibility = System.Windows.Visibility.Collapsed;
                MainPanorama.DefaultItem = 0;
            }
            MainPanorama.UpdateLayout();
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoggedIn")
                HandleLogInOut();
        }

        void _createMessageButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewMessagePage.xaml", UriKind.Relative));
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WebAuthorizePage.xaml", UriKind.Relative));
        }

        private void ServerTypeListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServerTypeListPicker == null) return;
            switch (ServerTypeListPicker.SelectedIndex)
            {
                case 0:
                    DomainTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                    DomainTextBox.Visibility = System.Windows.Visibility.Collapsed;
                    AccessTokenTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                    AccessTokenTextBox.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 1:
                    DomainTextBlock.Visibility = System.Windows.Visibility.Visible;
                    DomainTextBox.Visibility = System.Windows.Visibility.Visible;
                    AccessTokenTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                    AccessTokenTextBox.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 2:
                    DomainTextBlock.Visibility = System.Windows.Visibility.Visible;
                    DomainTextBox.Visibility = System.Windows.Visibility.Visible;
                    AccessTokenTextBlock.Visibility = System.Windows.Visibility.Visible;
                    AccessTokenTextBox.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        private void MainPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PanoramaItem)e.AddedItems[0]).Name)
            {
                case "PanoramaItemInbox":
                    ApplicationBar.Buttons.Add(_createMessageButton);
                    ApplicationBar.Mode = ApplicationBarMode.Default;
                    break;
                default:
                    ApplicationBar.Buttons.Remove(_createMessageButton);
                    ApplicationBar.Mode = ApplicationBarMode.Minimized;
                    break;
            }
        }

        private void ApplicationBarMenuItemDeauthorize_Click(object sender, EventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Log out and deauthorize this application?", "Deauthorize", MessageBoxButton.OKCancel);
            if (r == MessageBoxResult.OK)
            {
                App.ViewModel.Deauthorize();
            }
        }

        private void StreamListBox_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case "CompressionBottom":
                    App.ViewModel.LoadMoreActivityStream();
                    break;
                case "CompressionTop":
                case "NoVerticalCompression":
                    break;
            }
        }

        private void ConversationsListBox_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case "CompressionBottom":
                    App.ViewModel.LoadMoreConversations();
                    break;
                case "CompressionTop":
                case "NoVerticalCompression":
                    break;
            }
        }

        private void AttachVerticalScrollEvent(ListBox listBox, EventHandler<VisualStateChangedEventArgs> handler)
        {
            ScrollViewer sv = (ScrollViewer)FindElementRecursive(listBox, typeof(ScrollViewer));
            if (sv != null)
            {
                FrameworkElement element = VisualTreeHelper.GetChild(sv, 0) as FrameworkElement;
                if (element != null)
                {
                    VisualStateGroup vgroup = FindVisualState(element, "VerticalCompression");
                    if (vgroup != null)
                    {
                        vgroup.CurrentStateChanging += handler;
                    }
                }
            }
        }
        private UIElement FindElementRecursive(FrameworkElement parent, Type targetType)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            UIElement returnElement = null;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Object element = VisualTreeHelper.GetChild(parent, i);
                    if (element.GetType() == targetType)
                    {
                        return element as UIElement;
                    }
                    else
                    {
                        returnElement = FindElementRecursive(VisualTreeHelper.GetChild(parent, i) as FrameworkElement, targetType);
                    }
                }
            }
            return returnElement;
        }

        private VisualStateGroup FindVisualState(FrameworkElement element, string name)
        {
            if (element == null)
                return null;

            var groups = VisualStateManager.GetVisualStateGroups(element);
            foreach (VisualStateGroup group in groups)
                if (group.Name == name)
                    return group;

            return null;
        }

        private void ConversationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ConversationViewModel c = (ConversationViewModel)e.AddedItems[0];
                NavigationService.Navigate(new Uri("/ConversationDetailsPage.xaml?id=" + c.Id, UriKind.Relative));
            }
        }
    }
}