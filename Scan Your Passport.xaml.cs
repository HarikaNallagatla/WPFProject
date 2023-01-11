using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPFProject.ViewModel;
//using Emgu.CV;
//using Emgu.CV.OCR;
//using Emgu.CV.Structure;
//using OpenCvSharp;
using Window = System.Windows.Window;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for Scan_Your_Passport.xaml
    /// </summary>
    public partial class Scan_Your_Passport : Window
    {
        private System.Timers.Timer tmr;
        List<BitmapImage> lstbitmapImages = new List<BitmapImage>();
        static int imgcount = 0;
        DispatcherTimer timer = new DispatcherTimer();
        string culturecode = "en-US";
        public Scan_Your_Passport( string CultureCode)
        {
            InitializeComponent();


            imgdisplay.Source = new BitmapImage(new Uri(@"/Images/PassportScanScreen/Images/PassportScan.jpg", UriKind.Relative));
            string currentAssemblyPath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
            // string currentAssemblyParentPath = System.IO.Path.GetDirectoryName(currentAssemblyPath);
            //List<string> lstimages = Directory.EnumerateFiles(System.IO.Path.Combine(currentAssemblyPath, System.IO.Path.Combine("Images", "Carousel Images")), "*").ToList();


            //foreach (var img in lstimages)
            //{
            //    lstbitmapImages.Add(new BitmapImage(new Uri(img)));
            //}
            SetLanguage(CultureCode);
            culturecode = CultureCode;

        }
        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtsuccessmsg.Visibility = Visibility.Hidden;
                btnClickMe.Visibility = Visibility.Hidden;
                txtscaninprogress.Visibility = Visibility.Visible;
                imgdisplay.Visibility = Visibility.Hidden;
                mediaelement.Visibility = Visibility.Visible;
               // 

                //timer.Interval = TimeSpan.FromSeconds(2);
                //timer.Tick += timer_Tick;
                //timer.Start();
               // imgdisplay.Source = lstbitmapImages[imgcount];
                string currentAssemblyPath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
                //BitmapImage bitmapImage = new BitmapImage(new Uri(System.IO.Path.Combine(System.IO.Path.Combine(currentAssemblyPath, "Images"), "Sample video.mp4"), UriKind.Relative);
                mediaelement.Source = (new Uri(@"D:\VS Projects\WPFProject\WPFProject\Images\PassportScanScreen\Videos\AdVideo.mp4", UriKind.Absolute));
               
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Scan_Your_BoardingPass scan_Your_BoardingPass = new Scan_Your_BoardingPass(culturecode);
            scan_Your_BoardingPass.Show();
        }

        private void mediaelement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaelement.Position = TimeSpan.FromSeconds(1);
            txtsuccessmsg.Visibility = Visibility.Visible;
            btnNext.IsHitTestVisible=true;
        }
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (imgcount == lstbitmapImages.Count - 1)
        //        {
        //            imgcount = 0;
        //            timer.Stop();
        //            txtsuccessmsg.Visibility = Visibility.Visible;
        //            btnNext.IsHitTestVisible = true;
        //            txtscaninprogress.Visibility = Visibility.Hidden;
        //        }
        //        else
        //        {
        //            imgcount++;
        //            imgdisplay.Source = lstbitmapImages[imgcount];
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Application.Current.Dispatcher.Invoke(GetValue);
        //}
        //private void GetValue()
        //{
        //    if (tmr.Interval == 0.5 * 1000)
        //    {
        //        txtscaninprogress.Visibility = Visibility.Visible;
        //        tmr.Interval = 5 * 1000;

        //    }
        //    else
        //    {
        //        txtscaninprogress.Visibility = Visibility.Hidden;
        //        // tmr.Interval = 3 * 1000;
        //        txtsuccessmsg.Visibility = Visibility.Visible;
        //        btnNext.IsHitTestVisible = true;

        //        // btnhide_Click;
        //    }
        //}

        private void SetLanguage( string CultureCode)
        {
            try
            {
                string cultureCode = CultureCode;
                CultureInfo cultureInfo = new CultureInfo(cultureCode);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                var dictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                                  where d.Metadata.ContainsKey("Culture")
                                  && d.Metadata["Culture"].ToString().Equals(cultureCode)
                                  select d).FirstOrDefault();
                if (dictionary != null && dictionary.Value != null)
                {
                    this.Resources.MergedDictionaries.Clear();
                    this.Resources.MergedDictionaries.Add(dictionary.Value);
                }
                Application.Current.MainWindow.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            }
            catch (Exception ex)
            {
            }
        }

    }
}
