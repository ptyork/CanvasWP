using System;

namespace CanvasWP
{
    public class CourseViewModel : ViewModelBase
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

        private string _courseCode;
        public string CourseCode
        {
            get { return _courseCode; }
            set { ConditionallyChangeProperty(value, ref _courseCode, "CourseCode"); }
        }

    }
}
