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
    public class ImageViewModel : INotifyPropertyChanged
    {
        public ImageSource ImgSrc { get; set; }
        public string FullPath { get; set; }
        public ICommand SelectImageCommand { get; }

        public ImageViewModel()
        {
            ImgSrc = ImageSource.FromResource("XamarinTest.Images.Lena.png");
            FullPath = "XamarinTest.Images.Lena.png";
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

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
