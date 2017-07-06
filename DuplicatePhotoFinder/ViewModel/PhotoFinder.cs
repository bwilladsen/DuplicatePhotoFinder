using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Security;

namespace DuplicatePhotoFinder
{
    public class PhotoFinder : BaseViewModel
    {
        private String picturePath;
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
    }
}
