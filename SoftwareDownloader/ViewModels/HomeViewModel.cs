using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using SoftwareDownloader.Model;

namespace SoftwareDownloader.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        #region Properties

        private List<Download> _downloadsList;

        public List<Download> DownloadsList
        {
            get => _downloadsList ?? new List<Download>();
            set => SetProperty(ref _downloadsList, value);
        }

        private Download _selectedDownload;
        public Download SelectedDownload
        {
            get => _selectedDownload;
            set => SetProperty(ref _selectedDownload, value);
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get => _isDownloading;
            set => SetProperty(ref _isDownloading, value);
        }

        private bool _isSettingsPanelOpen;
        public bool IsSettingsPanelOpen
        {
            get => _isSettingsPanelOpen;
            set => SetProperty(ref _isSettingsPanelOpen, value);
        }


        #endregion


        public HomeViewModel()
        {
            GetSoftwareCommand = new DelegateCommand(GetSoftware);
            ShowSettingsCommand = new DelegateCommand(ShowSettings);

            DownloadsList = new List<Download>
            {
                new Download
                {
                    Name = "Adobe Reader",
                    Link = "https://admdownload.adobe.com/bin/live/readerdc_en_xa_crd_install.exe"
                },new Download
                {
                    Name = "Test",
                    Link = "https://admdownload.adobe.com/bin/live/readerdc_en_xa_crd_install.exe"
                }
            };
        }


        #region Methodes

        private void GetSoftware()
        {
            var downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = $@"{downloadDir}\reader.exe";
            using (var client = new WebClient())
            {
                client.DownloadFile(SelectedDownload.Link, filePath);
            }


        }

        private void ShowSettings()
        {
            IsSettingsPanelOpen = !IsSettingsPanelOpen;
        }

        #endregion



        #region Commands
        public ICommand GetSoftwareCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }

        #endregion
    }
}
