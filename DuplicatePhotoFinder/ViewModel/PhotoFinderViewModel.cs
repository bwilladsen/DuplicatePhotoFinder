using System;

namespace DuplicatePhotoFinder
{
    public class PhotoFinderViewModel : BaseViewModel
    {
        #region Commands

        private static UICommand searchCommand = new UICommand(searchCanExecuteCommandHandler, searchExecuteCommandHandler);
        public static UICommand SearchCommand
        {
            get { return searchCommand; }
        }
        
        private static bool searchCanExecuteCommandHandler(object sender)
        {
           return false;
        }

        private static void searchExecuteCommandHandler(object sender)
        {

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
    }
}
