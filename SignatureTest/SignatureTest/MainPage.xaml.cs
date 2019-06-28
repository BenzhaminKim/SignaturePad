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

        private async void BtnSaveImage_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
            //    ImageSource imageSource = ImageSource.FromStream(() => stream);
            //    await DependencyService.Get<ISaveImageDependency>().SaveImage("temp", "test.png", imageSource);

            //}
            //catch (Exception ex)
            //{
            //    await Application.Current.MainPage.DisplayAlert("error", ex.ToString(), "cancel");
            //    return;
            //}
            //finally
            //{
            //    await Application.Current.MainPage.DisplayAlert("Success", "저장되었습니다.", "cancel");
            //}

            try
            {
                var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);

                DependencyService.Get<ISaveImageDependency>().SavePicture("pics.txt", stream);
            }
            catch (Exception ex)
            {

            }
        }

        private  async void Button_Clicked(object sender, EventArgs e)
        {
            string path = DependencyService.Get<ISaveImageDependency>().GetLocalPath();

            var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
            string localPath = Path.Combine(path, "new4.png");
            byte[] imgBytes = ReadFully(stream);
            File.WriteAllBytes(localPath, imgBytes);

        }
        public static void SaveMemoryStream(Stream s, string FileName)
        {

            MemoryStream ms = new MemoryStream();
            s.CopyTo(ms);
               FileStream outStream = File.OpenWrite(FileName);
            ms.WriteTo(outStream);
            outStream.Flush();
            outStream.Close();
        }

        public static byte[] ReadFully(Stream input)
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

        private void ShowImag_Button_Clicked(object sender, EventArgs e)
        {
            string path = DependencyService.Get<ISaveImageDependency>().GetLocalPath();
            string localPath = Path.Combine(path, "new4.png");
            ImageSource source = ImageSource.FromFile(localPath);
            myImage.Source = source;
        }
    }
}
