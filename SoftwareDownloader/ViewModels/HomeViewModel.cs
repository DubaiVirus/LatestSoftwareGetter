using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using SoftwareDownloader.Helpers;
using SoftwareDownloader.Model;
using SoftwareDownloader.Serializers;
using SoftwareDownloader.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private readonly IXmlSerializer _serializer;
        #endregion


        public HomeViewModel(IXmlSerializer serializer)
        {
            _serializer = serializer;

            GetSoftwareCommand = new DelegateCommand(async () => await GetSoftwareAsync());
            ShowSettingsCommand = new DelegateCommand(async () => await ShowSettingsAsync().ConfigureAwait(false));

            Task.Run(async () => await LoadAllDownloadsAsync().ConfigureAwait(false));
        }


        #region Methodes

        private async Task GetSoftwareAsync()
        {
            IsDownloading = true;

            var downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = $@"{downloadDir}\{SelectedDownload.Name}.exe";

            try
            {
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(SelectedDownload.Link), filePath);
                }
                await ShowMessageAsync("Download finished", "Done");
            }
            catch (Exception)
            {
                await ShowMessageAsync("An unknown error has occured", "Error");
                IsDownloading = false;
            }

            IsDownloading = false;
        }

        private async Task ShowSettingsAsync()
        {
            IsSettingsPanelOpen = !IsSettingsPanelOpen;

            await LoadAllDownloadsAsync();
        }

        private async Task<List<Download>> LoadAllDownloadsAsync()
        {
            var lastSelectedDownload = SelectedDownload;

            var downloads = await _serializer.LoadConfigAsync<List<Download>>(MyStrings.XmlFileLocation) ?? new List<Download>();
            DownloadsList = downloads;

            if (downloads.Count > 0 && lastSelectedDownload != null)
            {
                var download = downloads.First(x => x.Link.ToLowerInvariant().Equals(lastSelectedDownload.Link.ToLowerInvariant())) ?? new Download();
                SelectedDownload = download;
            }
            return downloads;
        }

        private async Task ShowMessageAsync(string messageText, string title)
        {
            try
            {
                var homeView = Application.Current.Dispatcher.Invoke(() => Application.Current.Windows.OfType<HomeView>().FirstOrDefault());
                await Application.Current.Dispatcher.Invoke(() => homeView.ShowMessageAsync(title, messageText));
            }
            catch (Exception)
            {
                MessageBox.Show(messageText, title);
            }
        }

        #endregion



        #region Commands
        public ICommand GetSoftwareCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }

        #endregion
    }
}
