using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace DuplicatePhotoFinder
{
    public class PhotoFinderViewModel : BaseViewModel
    {
        #region Variables

        private Thread searchThread = null;
        Dictionary<String, List<PhotoViewModel>> photos = new Dictionary<String, List<PhotoViewModel>>();
        List<String> duplicatePhotos = new List<String>();

        #endregion

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
            startSearching();
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

        private void startSearching()
        {
            searchThread = new Thread(() => searchForDuplicatePictures(PicturePath));
            searchThread.IsBackground = true;
            searchThread.Name = "Picture Search Thread";
            searchThread.Start();
        }

        private void searchForDuplicatePictures(String path)
        {
            try
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(path);
                foreach (FileInfo currentFile in currentDirectory.GetFiles("*.jpg"))
                {
                    DateTime dateTaken = getDateTaken(currentFile.FullName);
                    PhotoViewModel photoViewModel = new PhotoViewModel();
                    photoViewModel.DateTaken = dateTaken;
                    photoViewModel.Path = currentFile.FullName;
                    photoViewModel.Size = currentFile.Length;
                    String hash = photoViewModel.ToString();
                    
                    if (!photos.ContainsKey(hash))
                    {
                        photos[hash] = new List<PhotoViewModel>();
                    }
                    else
                    {
                        if(!duplicatePhotos.Contains(hash))
                        {
                            duplicatePhotos.Add(hash);
                        }
                    }
                    photos[hash].Add(photoViewModel);
                }

                foreach (DirectoryInfo subDirectory in currentDirectory.GetDirectories())
                {
                    searchForDuplicatePictures(subDirectory.FullName);
                }
            }
            catch (Exception ex)
            {
                // TODO:  Logging
            }

        }

        private DateTime getDateTaken(string inFullPath)
        {
            DateTime returnDateTime = DateTime.MinValue;
            try
            {
                using (FileStream picStream = new FileStream(inFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BitmapSource bitSource = BitmapFrame.Create(picStream);
                    BitmapMetadata metaData = bitSource.Metadata as BitmapMetadata;
                    returnDateTime = DateTime.Parse(metaData.DateTaken);
                }
            }
            catch
            {
                // TODO:  Logging of some sorts  
            }
            return returnDateTime;
        }

        #endregion
    }
}
