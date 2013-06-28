using System;

namespace CanvasWP
{
    public class StreamViewModel : ViewModelBase
    {
        public enum ItemTypes
        {
            DiscussionTopic,
            Conversation,
            Message,
            Submission,
            Conference,
            Collaboration,
            Announcement
        }

        public string ItemTypeImageURL
        {
            get
            {
                switch (_itemType)
                {
                    case ItemTypes.Announcement:
                        return "/CanvasWP;component/Images/StreamAnnouncement.png";
                    case ItemTypes.Collaboration:
                    case ItemTypes.DiscussionTopic:
                        return "/CanvasWP;component/Images/StreamMembership.png";
                    case ItemTypes.Conversation:
                        return "/CanvasWP;component/Images/StreamConversation.png";
                    case ItemTypes.Submission:
                        return "/CanvasWP;component/Images/StreamSubmission.png";
                    case ItemTypes.Message:
                        return "/CanvasWP;component/Images/StreamMessage-" + this.ContextType + ".png";
                    default:
                        return "/CanvasWP;component/Images/CanvasLogo.png";
                }
            }
        }

        private ItemTypes _itemType;
        public ItemTypes ItemType
        {
            get { return _itemType; }
            set {
                _itemType = value;
                ConditionallyChangeProperty(value, _itemType, "ItemType"); 
            }
        }

        private string _contextType;
        public string ContextType
        {
            get { return _contextType; }
            set { ConditionallyChangeProperty(value, ref _contextType, "ContextType"); }
        }

        private string _context;
        public string Context
        {
            get { return _context; }
            set { ConditionallyChangeProperty(value, ref _context, "Context"); }
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { ConditionallyChangeProperty(value, ref _createdAt, "CreatedAt"); }
        }

        private DateTime? _updatedAt;
        public DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { ConditionallyChangeProperty(value, ref _updatedAt, "UpdatedAt"); }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { ConditionallyChangeProperty(value, ref _id, "Id"); }
        }

        private long? _innerId;
        public long? InnerId
        {
            get { return _innerId; }
            set { ConditionallyChangeProperty(value, ref _innerId, "InnerId"); }
        }

        private long? _courseId;
        public long? CourseId
        {
            get { return _courseId; }
            set { ConditionallyChangeProperty(value, ref _courseId, "CourseId"); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { ConditionallyChangeProperty(value, ref _title, "Title"); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { ConditionallyChangeProperty(value, ref _message, "Message"); }
        }

    }
}
