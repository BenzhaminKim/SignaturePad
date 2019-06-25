using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SignaturePad.Forms;
using System.IO;

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
              var  stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
              myImage.Source = ImageSource.FromStream(() => stream);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://m.media-amazon.com/images/M/MV5BMTgyNDk3NDEwOV5BMl5BanBnXkFtZTgwODY5MTg5NzE@._V1_SX300.jpg");
            myImage.Source = ImageSource.FromUri(uri);
        }
    }
}
