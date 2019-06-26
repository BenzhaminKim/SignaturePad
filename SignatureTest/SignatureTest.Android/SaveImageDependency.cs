using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System.IO;
using Android.Graphics;
using SignatureTest.Droid;
using Java.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

[assembly: Xamarin.Forms.Dependency(typeof(SaveImageDependency))]
namespace SignatureTest.Droid
{
    public class SaveImageDependency : ISaveImageDependency
    {

    
        public async Task<bool> SaveImage(string directory, string filename, ImageSource img)
        {
            var image = new Image();
            image.Source = img;
            var renderer = new Xamarin.Forms.Platform.Android.StreamImagesourceHandler();
            Bitmap photo = await renderer.LoadImageAsync(img, Android.App.Application.Context);


            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filePath = System.IO.Path.Combine(documentsPath, "test.png");
            var stream = new FileStream(filePath, FileMode.OpenOrCreate,FileAccess.ReadWrite);
            photo.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Close();

            return true;

        }

    }
}