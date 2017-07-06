using System;
using System.ComponentModel;

namespace DuplicatePhotoFinder
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                return ValidateProperty(columnName);
            }
        }

        protected virtual String ValidateProperty(String propertyName)
        {
            return String.Empty;
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
