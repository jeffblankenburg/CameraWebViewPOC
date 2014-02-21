using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CameraWebViewPOC.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Windows.Resources;

namespace CameraWebViewPOC
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask cct = new CameraCaptureTask();
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            cct.Completed += cct_Completed;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        void cct_Completed(object sender, PhotoResult e)
        {
            BitmapImage bi = new BitmapImage();
            bi.SetSource(e.ChosenPhoto);
            SaveImage(bi);
            PageSource.InvokeScript("getImage", "pic.jpg");
        }

        private void SaveImage(BitmapImage bi)
        {
            String tempJPEG = "image.jpg";

            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {

                
                var wb = new WriteableBitmap(bi);

                using (var isoFileStream = isoStore.CreateFile("pic.jpg"))
                    Extensions.SaveJpeg(wb, isoFileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);

            }


        }

        private void PageSource_ScriptNotify(object sender, NotifyEventArgs e)
        {
            
            cct.Show();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}