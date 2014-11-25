using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Mime;
using System.Drawing;

namespace Codesharper.ImageResize
{
    internal class Program
    {
        private const string FileIn = @"Code-Sharper-031.png";
        private const string FileOut = @"Code-Sharper-031-resized.png";

        private static void Main(string[] args)
        {
            var imageIn = (Bitmap) Image.FromFile(FileIn, true);

            var imageOut = ResizeImage(imageIn, 50);

            if (File.Exists(FileOut)) File.Delete(FileOut);

            imageOut.Save(FileOut);

        }

        public static Bitmap ResizeImage(Bitmap imgToResize, double percent)
        {
            var width = Convert.ToInt16(imgToResize.Width*(percent/100));
            var height = Convert.ToInt16(imgToResize.Height*(percent/100));

            var b = new Bitmap(width, height);
            b.SetResolution(imgToResize.HorizontalResolution, imgToResize.VerticalResolution);

            using (var g = Graphics.FromImage(b))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, width, height);
            }

            return b;
        }


    }
}