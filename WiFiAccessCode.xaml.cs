using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
//using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Authentication;
using System.Globalization;
using System.Threading;
using WPFProject.ViewModel;
using System.Windows.Markup;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for WiFiAccessCode.xaml
    /// </summary>
    public partial class WiFiAccessCode : Window
    {
        public WiFiAccessCode(string CultureCode)
        {

            InitializeComponent();
            SetLanguage(CultureCode);
            string randomnumber = string.Empty;
            Random random = new Random();
            randomnumber = (random.Next(100000, 999999)).ToString();
            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, SslPolicyErrors) => { return true; };
            //clientHandler.SslProtocols = SslProtocols.None;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44318/api/Home/GetPassCode");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.GetAsync("https://localhost:44318/api/Home/GetPassCode").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    MessageBox.Show(response.Content.ToString());
            //    randomnumber = response.Content.ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            //}
            txtWifiCode.Visibility = Visibility.Visible;
            txtWifiCode.Text = txtWifiCode.Text + "\n" + randomnumber;
        }

        private void SetLanguage(string CultureCode)
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
