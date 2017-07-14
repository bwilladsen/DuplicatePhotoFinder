using System;
using System.Collections.ObjectModel;

namespace DuplicatePhotoFinder
{
    public class DuplicatePhotoViewModel
    {
        public DuplicatePhotoViewModel()
        {
            Id = String.Empty;
            DuplicatePictures = new ObservableCollection<PhotoViewModel>();
        }

        public string Id
        {
            get;
            set;
        }

        public ObservableCollection<PhotoViewModel> DuplicatePictures
        {
            get;
            private set;
        }
    }
}
