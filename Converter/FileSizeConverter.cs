using System;

namespace Converter
{
    public class FileSizeConverter
    {
        public string FileSizeConvert(double size)
        {
            double kilobytes = Math.Round(size / 1024);
            double megabytes = Math.Round(kilobytes / 1024);
            double gigabytes = Math.Round(megabytes / 1024);
            if (gigabytes > 1)
            {
                return gigabytes + " GB";
            }
            else if (megabytes > 1)
            {
                return megabytes + " MB";
            }
            else if (kilobytes > 1)
            {
                return kilobytes + " KB";
            }
            else
            {
                return size + " B";
            }
        }
    }
}
