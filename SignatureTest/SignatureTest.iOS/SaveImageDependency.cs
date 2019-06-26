using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using SignatureTest.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SaveImageDependency))]
namespace SignatureTest.iOS
{
    public class SaveImageDependency : ISaveImageDependency
    {
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
    }
}