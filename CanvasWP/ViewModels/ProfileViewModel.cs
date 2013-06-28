using System;

namespace CanvasWP
{
    public class ProfileViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { ConditionallyChangeProperty(value, ref _name, "Name"); }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { ConditionallyChangeProperty(value, ref _id, "Id"); }
        }

        private string _primaryEmail;
        public string PrimaryEmail
        {
            get { return _primaryEmail; }
            set { ConditionallyChangeProperty(value, ref _primaryEmail, "PrimaryEmail"); }
        }

        private string _loginId;
        public string LoginId
        {
            get { return _loginId; }
            set { ConditionallyChangeProperty(value, ref _loginId, "LoginId"); }
        }

        private string _domain;
        public string Domain
        {
            get { return _domain; }
            set { ConditionallyChangeProperty(value, ref _domain, "Domain"); }
        }

        private string _avatarURL;
        public string AvatarURL
        {
            get { return _avatarURL; }
            set { ConditionallyChangeProperty(value, ref _avatarURL, "AvatarURL"); }
        }
    }
}
