using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace SignatureTest
{
    public interface ISaveImageDependency
    {
        Task<bool> SaveImage(string directory, string filename, ImageSource img);

        void SavePicture(string name, Stream data, string location = "temp");

        string GetLocalPath();
    }
}
