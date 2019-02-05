using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

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
        #endregion


        public SettingsViewModel()
        {
            AddDownloadCommand = new DelegateCommand(AddDownload);
        }


        #region Methodes

        private void AddDownload()
        {

        }

        #endregion

        #region Commands
        public ICommand AddDownloadCommand { get; set; }
        #endregion

    }
}
