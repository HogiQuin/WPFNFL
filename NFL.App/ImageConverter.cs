using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//added
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing; //add reference
using System.Drawing.Imaging;

class ImageConverter
{
    /// <summary>
    /// Converts image type from SQL Server (byte[]) to WPF BitmapImage
    /// </summary>
    /// <param name="array">SQL Image (byte[])</param>
    /// <returns></returns>
    public static BitmapImage ByteToBitmapImage(byte[] data)
    {
        MemoryStream ms1 = new MemoryStream();
        ms1.Write(data, 0, data.Length);
        ms1.Position = 0;
        Image image = Image.FromStream(ms1);
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        MemoryStream ms2 = new MemoryStream();
        image.Save(ms2, ImageFormat.Png);
        ms2.Seek(0, SeekOrigin.Begin);
        bitmapImage.StreamSource = ms2;
        bitmapImage.EndInit();
        return bitmapImage;
        
    }


    /// <summary>
    /// Converts Image File to SQL Image format (byte[])
    /// </summary>
    /// <param name="fileName">File name with full path</param>
    /// <returns></returns>
    public static byte[] FileToByte(string fileName)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, Convert.ToInt32(fs.Length));
        fs.Close();
        return data;
    }
}
