using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using SignatureTest.iOS;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SaveImageDependency))]
namespace SignatureTest.iOS
{
    public class SaveImageDependency : ISaveImageDependency
    {
        public string GetLocalPath()
        {

            return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public async Task<bool> SaveImage(string directory, string filename, ImageSource img)
        {
            NSData imgData = null;
            var renderer = new Xamarin.Forms.Platform.iOS.StreamImagesourceHandler();
            UIKit.UIImage photo = await renderer.LoadImageAsync(img);

            var savedImageFilename = System.IO.Path.Combine(directory, filename);
            NSFileManager.DefaultManager.CreateDirectory(directory, true, null);

            if (System.IO.Path.GetExtension(filename).ToLower() == ".png")
                imgData = photo.AsPNG();
            else
                imgData = photo.AsJPEG(100);

            NSError err = null;
            imgData.Save(savedImageFilename, NSDataWritingOptions.Atomic, out err);
            return true;
        }

        public void SavePicture(string name, Stream data, string location = "temp")
        {
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Orders", location);
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, name);

            byte[] bArray = new byte[data.Length];
            using(FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }
    }
}