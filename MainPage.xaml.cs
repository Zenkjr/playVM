using System;
using System.Text;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PlayerVideoMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public bool IsFastForwardButtonVisible { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            //Create a MediaElement and enable transport controls.
        }

        private async void BtnChoseVideo(object sender, RoutedEventArgs e)
        {
            await SetLocalMedia();
        }
        
        async private System.Threading.Tasks.Task SetLocalMedia()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".wma");

            var file = await openPicker.PickSingleFileAsync();
            // mediaPlayer is a MediaPlayerElement defined in XAML
            if (file != null)
            {
                mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                mediaPlayer.AreTransportControlsEnabled = true;
                mediaPlayer.MediaPlayer.Play();
            }
            
        }

        //Player song 
        private async void BtnChoseSong(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".wma");
            picker.FileTypeFilter.Add(".mp3");

            var files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                StringBuilder output = new StringBuilder("Picked files:\n");

                // Application now has read/write access to the picked file(s)
                foreach (Windows.Storage.StorageFile file in files)
                {
                    output.Append(file.Name + "\n");
                    MusicPlayer.Source = MediaSource.CreateFromStorageFile(file);
                }
                fileTest.Text = output.ToString();
                MusicPlayer.AreTransportControlsEnabled = true;
                MusicPlayer.MediaPlayer.Play();
            }
            else
            {
                this.fileTest.Text = "Operation cancelled.";
            }
            //await SetLocalSongs();
        }

        //async private System.Threading.Tasks.Task SetLocalSongs()
        //{
        //    var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

        //    openPicker.FileTypeFilter.Add(".mp3");
        //    openPicker.FileTypeFilter.Add(".wma");


        //    var file = await openPicker.PickSingleFileAsync();
        //    // mediaPlayer is a MediaPlayerElement defined in XAML
        //    if (file != null)
        //    {
        //        MusicPlayer.Source = MediaSource.CreateFromStorageFile(file);
        //        MusicPlayer.AreTransportControlsEnabled = true;
        //        MusicPlayer.MediaPlayer.Play();
        //    }

        //}
    }
}
