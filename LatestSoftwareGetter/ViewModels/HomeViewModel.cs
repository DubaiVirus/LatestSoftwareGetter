using LatestSoftwareGetter.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Input;

namespace LatestSoftwareGetter.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        #region Properties

        private List<Download> _downloadsList;

        public List<Download> DownloadsList
        {
            get => _downloadsList?? new List<Download>();
            set => SetProperty(ref _downloadsList, value);
        }

        private Download _selectedDownload;
        public Download SelectedDownload
        {
            get => _selectedDownload;
            set => SetProperty(ref _selectedDownload, value);
        }


        #endregion


        public HomeViewModel()
        {
            GetSoftwareCommand = new DelegateCommand(GetSoftware);

            DownloadsList = new List<Download>();
            DownloadsList.Add(new Download{ Name = "Adobe Reader", Link = "https://admdownload.adobe.com/bin/live/readerdc_en_xa_crd_install.exe" });
        }


        #region Methodes

        private void GetSoftware()
        {
            //var url = "https://get.adobe.com/reader";
            //var webRequest = WebRequest.Create(url);
            //var response = webRequest.GetResponse();

            //var webPageStream = response.GetResponseStream();
            //var reader = new StreamReader(webPageStream);
            //var source = reader.ReadToEnd();


            var downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = $@"{downloadDir}\reader.exe";
            using (var client = new WebClient())
            {
                client.DownloadFile("", filePath);
            }


        }

        #endregion



        #region Commands
        public ICommand GetSoftwareCommand { get; set; }

        #endregion
    }
}
