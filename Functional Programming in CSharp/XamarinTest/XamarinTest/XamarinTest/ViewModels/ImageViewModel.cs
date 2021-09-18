using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinTest.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        public ImageSource ImgSrc { get; set; }
        public string FullPath { get; set; }
        public ICommand SelectImageCommand { get; }

        public ImageViewModel()
        {
            ImgSrc = ImageSource.FromResource("XamarinTest.Images.MeatCanyon_Finn.jpg");
            FullPath = "XamarinTest.Images.MeatCanyon_Finn.jpg";
            SelectImageCommand = new Command(SelectImage);
        }

        async void SelectImage()
        {
            // TODO: Select image file from file system.
            await PickAndShow();
        }

        async Task<FileResult> PickAndShow()
        {
            try
            {
                var result = await FilePicker.PickAsync();
                FullPath = result.FullPath;

                ImgSrc = ImageSource.FromFile(FullPath);

                OnPropertyChanged(nameof(ImgSrc));
                OnPropertyChanged(nameof(FullPath));

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}
