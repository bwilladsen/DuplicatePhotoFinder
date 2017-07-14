using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace DuplicatePhotoFinder
{
    public class PhotoFinderViewModel : BaseViewModel
    {
        #region Constructor

        public PhotoFinderViewModel()
        {
            DuplicatePictures = new ObservableCollection<DuplicatePhotoViewModel>();
        }

        #endregion

        #region Variables

        private Thread searchThread = null;
        private Dictionary<String, List<PhotoViewModel>> photos = new Dictionary<String, List<PhotoViewModel>>();
        private String picturePath = String.Empty;
        private UICommand searchCommand;
        private bool isSearching = false;

        #endregion

        #region Commands

        private bool searchCanExecuteCommandHandler(object sender)
        {
            return string.IsNullOrEmpty(Error) && !IsSearching;
        }

        private void searchExecuteCommandHandler(object sender)
        {
            IsSearching = true;
            searchThread = new Thread(() => startSearching());
            searchThread.IsBackground = true;
            searchThread.Name = "Picture Search Thread";
            searchThread.Start();
        }

        #endregion

        #region Properties

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

        public String PicturePath
        {
            get { return this.picturePath; }
            set
            {
                if (value != picturePath)
                {
                    picturePath = value;
                    OnPropertyChanged("PicturePath");
                }
            }
        }

        public bool IsSearching
        {
            get { return isSearching; }
            private set
            {
                if (isSearching != value)
                {
                    isSearching = value;
                    OnPropertyChanged("IsSearching");
                }

            }
        }

        public ObservableCollection<DuplicatePhotoViewModel> DuplicatePictures
        {
            get;
            private set;
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
            searchForDuplicatePictures(PicturePath);
            IsSearching = false;
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
                        int index = getDuplicatePhotoIndex(hash);
                        if (index == -1)
                        {
                            DuplicatePhotoViewModel duplicatePhotoViewModel = new DuplicatePhotoViewModel();
                            duplicatePhotoViewModel.Id = hash;
                            duplicatePhotoViewModel.DuplicatePictures.Add(photoViewModel);
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                DuplicatePictures.Add(duplicatePhotoViewModel);
                            });
                        }
                        else
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                DuplicatePictures[index].DuplicatePictures.Add(photoViewModel);
                            });
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

        private int getDuplicatePhotoIndex(string id)
        {
            for (int i = 0; i < DuplicatePictures.Count; i++)
            {
                if (DuplicatePictures[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
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
