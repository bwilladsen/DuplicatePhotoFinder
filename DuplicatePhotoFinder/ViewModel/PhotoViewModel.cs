using System;

namespace DuplicatePhotoFinder
{
    public class PhotoViewModel : BaseViewModel
    {
        public String Path { get; set; }
        public DateTime DateTaken { get; set; }
        public long Size { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1}", DateTaken.Ticks, Size);
        }
        
    }
}
