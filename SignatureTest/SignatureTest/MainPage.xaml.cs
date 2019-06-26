using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SignaturePad.Forms;
using System.IO;
using System.Drawing;

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
            try
            {
                var  stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
                AzureOperations db = new AzureOperations();
                await db.UploadFIleAsnyc(stream);

            }
            catch(Exception ex)
            {
               await Application.Current.MainPage.DisplayAlert("error", ex.ToString(),"cancel");
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://m.media-amazon.com/images/M/MV5BMTgyNDk3NDEwOV5BMl5BanBnXkFtZTgwODY5MTg5NzE@._V1_SX300.jpg");
            myImage.Source = ImageSource.FromUri(uri);
        }
    }
}
