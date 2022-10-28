using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MurasoliAPI.Model
{
    public class ResizeImage
    {
        public void CompressImage(string SoucePath, string DestPath, int quality,string FileName)
        {
            try
            {
                DestPath = DestPath + "\\" + FileName;

                using (Bitmap bmp1 = new Bitmap(SoucePath))
                {

                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    //  System.Drawing.Imaging.PixelFormat QualityEncoder =  System.Drawing.Imaging.PixelFormat.Format24bppRgb
                    System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                    EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);

                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
         
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public  void  ImgResize(string source, string destination)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(source);
                Bitmap b1 = new Bitmap(img);
                System.Drawing.Image imgToResize = b1;
                Size size = new Size(100, 100);
                //Get the image current width  
                int sourceWidth = imgToResize.Width;
                //Get the image current height  
                int sourceHeight = imgToResize.Height;
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;
                //Calulate  width with new desired size  
                nPercentW = ((float)size.Width / (float)sourceWidth);
                //Calculate height with new desired size  
                nPercentH = ((float)size.Height / (float)sourceHeight);
                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;
                //New Width  
                int destWidth = (int)(sourceWidth * nPercent);
                //New Height  
                int destHeight = (int)(sourceHeight * nPercent);
                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // Draw image with new width and height  
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();
                b.Save(System.IO.Path.GetDirectoryName(destination));
                // return (System.Drawing.Image)b;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
