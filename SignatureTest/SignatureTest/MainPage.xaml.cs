using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SignaturePad.Forms;
using System.IO;
using System.Drawing;
using Xamarin.Essentials;

namespace SignatureTest
{
    public partial class MainPage : ContentPage
    {
        string uniqueCode = "test6";
        string imageSource;
        public string ImgSource
        {
            get { return imageSource; }
            set
            {
                if(imageSource != value)
                {
                    imageSource = value;
                }
            }
        }
        public MainPage()
        {
            InitializeComponent();
           
        }

        private async void BtnSaveImageAzure_Clicked(object sender, EventArgs e)
        {
            try
            {
                AzureOperations blobCloud = new AzureOperations();
                var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);

               await blobCloud.UploadFIleAsnyc(stream, uniqueCode + ".png");
            }
            catch (Exception ex)
            {

            }
        }

        private  async void SaveLocalButton_Clicked(object sender, EventArgs e)
        {
            string path = DependencyService.Get<ISaveImageDependency>().GetLocalPath();

            var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
            string localPath = Path.Combine(path, uniqueCode + ".png");
            byte[] imgBytes = ReadFully(stream);
            File.WriteAllBytes(localPath, imgBytes);

        }


        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void ShowImag_Button_Clicked(object sender, EventArgs e)
        {
            string path = DependencyService.Get<ISaveImageDependency>().GetLocalPath();
            string fileName = uniqueCode + ".png";
            string localPath = Path.Combine(path, fileName);
            if (!File.Exists(localPath))
            {
                AzureOperations blobCloud = new AzureOperations();
             await  blobCloud.DownloadFileFromBlob(fileName, localPath);
            }
            ImageSource source = ImageSource.FromFile(localPath);
            myImage.Source = source;
        }
    }
}
