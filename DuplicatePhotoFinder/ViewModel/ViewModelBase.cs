using System;
using System.ComponentModel;

namespace DuplicatePhotoFinder
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region IDataErrorInfo

        private String error = String.Empty;
        public string this[string columnName]
        {
            get
            {
                error = ValidateProperty(columnName);
                return error;
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
                return error;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

    }
}
