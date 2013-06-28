using System;
using System.Collections.Generic;

namespace CanvasWP
{
    public class ConversationViewModel : ViewModelBase
    {
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
