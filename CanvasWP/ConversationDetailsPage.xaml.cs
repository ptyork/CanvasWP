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
    public partial class ConversationDetailsPage : PhoneApplicationPage
    {
        public ConversationDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = long.Parse(this.NavigationContext.QueryString["id"]);
            var model = new ConversationDetailsViewModel();
            this.DataContext = model;
            App.ViewModel.LoadConversation(id, model);
        }
    }
}