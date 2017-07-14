using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
