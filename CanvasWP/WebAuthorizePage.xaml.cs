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

namespace CanvasWP
{
    public partial class WebAuthorizePage : PhoneApplicationPage
    {
        public WebAuthorizePage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Uri authAddress = new Uri("https://canvas.instructure.com/login/oauth2/auth?client_id=63&response_type=code&redirect_uri=urn:ietf:wg:oauth:2.0:oob");
            AuthorizeWebBrowser.Navigate(authAddress);
        }

        private void AuthorizeWebBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.Query.Contains("code="))
            {
                App.ViewModel.RetrieveAccessToken(e.Uri.Query.Split('=')[1]);
                NavigationService.GoBack();
            }
            else
            {
                var paths = e.Uri.LocalPath.Split('/');
                switch (paths[paths.Length - 1])
                {
                    case "login":
                    case "auth":
                    case "confirm":
                    case "accept":
                        // ignore
                        break;
                    default:
                        MessageBox.Show("Authorization Failed");
                        NavigationService.GoBack();
                        break;
                }
            }
        }

        private void AuthorizeWebBrowser_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            MessageBox.Show("Authorization Failed");
            NavigationService.GoBack();
        }
    }
}