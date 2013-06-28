using System;
using System.ComponentModel;

namespace CanvasWP
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool ConditionallyChangeProperty(object newValue, object currValue, string propertyName)
        {
            if (currValue != newValue)
            {
                currValue = newValue;
                NotifyPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
        protected bool ConditionallyChangeProperty<T>(T newValue, ref T currValue, string propertyName)
        {
            if (!object.Equals(currValue, newValue))
            {
                currValue = newValue;
                NotifyPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}
