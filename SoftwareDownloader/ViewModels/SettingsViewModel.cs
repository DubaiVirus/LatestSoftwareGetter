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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SoftwareDownloader.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        #region Properties
        private string _downloadName;
        public string DownloadName
        {
            get => _downloadName;
            set => SetProperty(ref _downloadName, value);
        }

        private string _downloadLink;
        public string DownloadLink
        {
            get => _downloadLink;
            set => SetProperty(ref _downloadLink, value);
        }

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


        private readonly IXmlSerializer _serializer;
        #endregion


        public SettingsViewModel(IXmlSerializer serializer)
        {
            _serializer = serializer;

            AddDownloadCommand = new DelegateCommand(async () => await AddDownloadAsync().ConfigureAwait(false));
            DeleteDownloadCommand = new DelegateCommand(async () => await DeleteDownloadAsync().ConfigureAwait(false));

            Task.Run(async () => await LoadAllDownloadsAsync().ConfigureAwait(false));
        }


        #region Methodes

        private async Task AddDownloadAsync()
        {
            if (string.IsNullOrEmpty(DownloadName) && string.IsNullOrEmpty(DownloadLink))
                await ShowMessageAsync("Download details are missing!", "Error");
            else if (string.IsNullOrEmpty(DownloadName))
                await ShowMessageAsync("You have to enter a name first!", "Error");
            else if (string.IsNullOrEmpty(DownloadLink))
                await ShowMessageAsync("Link cannot be empty!", "Error");

            else
            {
                var downloads = await LoadAllDownloadsAsync();

                if (downloads.Count > 0 && downloads.Any(x => x.Link.ToLowerInvariant().Equals(DownloadLink)))
                {
                    await ShowMessageAsync("This software already exists!", "Error");
                    return;
                }

                downloads.Add(new Download { Name = DownloadName, Link = DownloadLink });
                await _serializer.SaveConfigAsync(downloads, MyStrings.XmlFileLocation);

                await LoadAllDownloadsAsync();

                await ShowMessageAsync("New software added successfully", "Done");

                DownloadName = DownloadLink = string.Empty;
            }
        }

        private async Task DeleteDownloadAsync()
        {
            var selectedDownload = SelectedDownload;
            if (selectedDownload == null)
                await ShowMessageAsync("No download is selected!", "Error");

            else
            {
                var downloads = await LoadAllDownloadsAsync();
                var downloadToDelete = downloads.First(x =>
                    x.Link.ToLowerInvariant().Equals(selectedDownload.Link.ToLowerInvariant()));
                if (downloadToDelete != null)
                {
                    downloads.Remove(downloadToDelete);

                    await _serializer.SaveConfigAsync(downloads, MyStrings.XmlFileLocation);

                    await ShowMessageAsync("Software removed successfully", "Done");
                }

                await LoadAllDownloadsAsync();
            }
        }

        private async Task<List<Download>> LoadAllDownloadsAsync()
        {
            var downloads = await _serializer.LoadConfigAsync<List<Download>>(MyStrings.XmlFileLocation) ?? new List<Download>();
            DownloadsList = downloads;
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
        public ICommand AddDownloadCommand { get; set; }
        public ICommand DeleteDownloadCommand { get; set; }
        #endregion

    }
}
