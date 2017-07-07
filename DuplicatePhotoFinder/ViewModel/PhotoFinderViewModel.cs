using System;

namespace DuplicatePhotoFinder
{
    public class PhotoFinderViewModel : BaseViewModel
    {
        #region Commands

        private UICommand searchCommand;
        public UICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new UICommand(searchCanExecuteCommandHandler, searchExecuteCommandHandler);
                }
                return searchCommand;
            }
        }

        private bool searchCanExecuteCommandHandler(object sender)
        {
            return string.IsNullOrEmpty(Error) && !IsSearching;
        }

        private void searchExecuteCommandHandler(object sender)
        {
            IsSearching = true;
        }

        #endregion

        #region Properties

        private String picturePath = String.Empty;
        public String PicturePath
        {
            get { return this.picturePath; }
            set
            {
                if (value != picturePath)
                {
                    picturePath = value;
                    NotifyPropertyChanged("PicturePath");
                }
            }
        }

        public bool IsSearching
        {
            get;
            set;
        }

        #endregion

        #region Validation

        protected override string ValidateProperty(string propertyName)
        {
            String error = String.Empty;
            switch (propertyName)
            {
                case "PicturePath":
                    if (!PicturePath.IsValidPath())
                    {
                        error = "The path is invalid.  It must be a valid Windows path.";
                    }
                    break;
            }

            return error;
        }

        #endregion

        #region Searching

        private void searchForDuplicatePictures()
        {

        }

        #endregion
    }
}
