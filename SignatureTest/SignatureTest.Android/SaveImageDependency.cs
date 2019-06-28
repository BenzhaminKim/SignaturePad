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
        public string GetLocalPath()
        {
            return Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
        }

        public async Task<bool> SaveImage(string directory, string filename, ImageSource img)
        {
            var image = new Image();
            image.Source = img;
            var renderer = new Xamarin.Forms.Platform.Android.StreamImagesourceHandler();
            Bitmap photo = await renderer.LoadImageAsync(img, Android.App.Application.Context);

            var documentsPath = Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
            var filePath = System.IO.Path.Combine(documentsPath, filename);

            bool success;

            using(FileStream outputStream = new FileStream(filePath,FileMode.Create))
            {
               success = await photo.CompressAsync(Bitmap.CompressFormat.Png, 100, outputStream);
             
            }


            return success;

        }

        public void SavePicture(string name, Stream data, string location = "temp")
        {
            String documentsPath = Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;


            documentsPath = System.IO.Path.Combine(documentsPath,  "Orders", location);
            Directory.CreateDirectory(documentsPath);

            string filePath = System.IO.Path.Combine(documentsPath, name);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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