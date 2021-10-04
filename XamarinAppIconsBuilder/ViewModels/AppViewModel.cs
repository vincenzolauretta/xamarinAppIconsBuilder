using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XamarinAppIconsBuilder.Components;
using XamarinAppIconsBuilder.Models;

namespace XamarinAppIconsBuilder.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        string _LogoFilePath;
        public string LogoFilePath
        {
            get { return _LogoFilePath; }
            set
            {
                if (_LogoFilePath != value)
                {
                    _LogoFilePath = value;
                    OnPropertyChanged(nameof(LogoFilePath));
                }
            }
        }

        string _LogoBackgroundColor = "#000000";
        public string LogoBackgroundColor
        {
            get { return _LogoBackgroundColor; }
            set
            {
                if (_LogoBackgroundColor != value)
                {
                    _LogoBackgroundColor = value;
                    OnPropertyChanged(nameof(LogoBackgroundColor));
                }
            }
        }

        string _XamarinSolutionPath;
        public string XamarinSolutionPath
        {
            get { return _XamarinSolutionPath; }
            set
            {
                if (_XamarinSolutionPath != value)
                {
                    _XamarinSolutionPath = value;
                    OnPropertyChanged(nameof(XamarinSolutionPath));
                }
            }
        }

        ObservableCollection<IconFileViewModel> _AndroidIcons;
        public ObservableCollection<IconFileViewModel> AndroidIcons
        {
            get { return _AndroidIcons; }
            set
            {
                if (_AndroidIcons != value)
                {
                    _AndroidIcons = value;
                    OnPropertyChanged(nameof(AndroidIcons));
                }
            }
        }
        ObservableCollection<IconFileViewModel> _PreviewAndroidIcons;
        public ObservableCollection<IconFileViewModel> PreviewAndroidIcons
        {
            get { return _PreviewAndroidIcons; }
            set
            {
                if (_PreviewAndroidIcons != value)
                {
                    _PreviewAndroidIcons = value;
                    OnPropertyChanged(nameof(PreviewAndroidIcons));
                }
            }
        }
        ObservableCollection<IconFileViewModel> _IosIcons;
        public ObservableCollection<IconFileViewModel> IosIcons
        {
            get { return _IosIcons; }
            set
            {
                if (_IosIcons != value)
                {
                    _IosIcons = value;
                    OnPropertyChanged(nameof(IosIcons));
                }
            }
        }
        ObservableCollection<IconFileViewModel> _PreviewIosIcons;
        public ObservableCollection<IconFileViewModel> PreviewIosIcons
        {
            get { return _PreviewIosIcons; }
            set
            {
                if (_PreviewIosIcons != value)
                {
                    _PreviewIosIcons = value;
                    OnPropertyChanged(nameof(PreviewIosIcons));
                }
            }
        }
        ObservableCollection<IconFileViewModel> _WindowsUniversalIcons;
        public ObservableCollection<IconFileViewModel> WindowsUniversalIcons
        {
            get { return _WindowsUniversalIcons; }
            set
            {
                if (_WindowsUniversalIcons != value)
                {
                    _WindowsUniversalIcons = value;
                    OnPropertyChanged(nameof(WindowsUniversalIcons));
                }
            }
        }
        ObservableCollection<IconFileViewModel> _PreviewWindowsUniversalIcons;
        public ObservableCollection<IconFileViewModel> PreviewWindowsUniversalIcons
        {
            get { return _PreviewWindowsUniversalIcons; }
            set
            {
                if (_PreviewWindowsUniversalIcons != value)
                {
                    _PreviewWindowsUniversalIcons = value;
                    OnPropertyChanged(nameof(PreviewWindowsUniversalIcons));
                }
            }
        }


        private List<SupportedIconItemModel> _supportedIconsIos;
        private List<SupportedIconItemModel> _supportedIconsAndroid;
        private List<SupportedIconItemModel> _supportedIconsWindowsUniversal;

        public AppViewModel()
        {
            Task.Run(() =>
            {
                _supportedIconsIos = JsonConvert.DeserializeObject<List<SupportedIconItemModel>>(System.IO.File.ReadAllText("supported.icons.ios.json"));
                _supportedIconsAndroid = JsonConvert.DeserializeObject<List<SupportedIconItemModel>>(System.IO.File.ReadAllText("supported.icons.android.json"));
                _supportedIconsWindowsUniversal = JsonConvert.DeserializeObject<List<SupportedIconItemModel>>(System.IO.File.ReadAllText("supported.icons.windows.json"));
            });
        }


        RelayCommand _BrowseLogoCommand;
        public ICommand BrowseLogoCommand
        {
            get
            {
                if (_BrowseLogoCommand == null)
                {
                    _BrowseLogoCommand = new RelayCommand(param =>
                    {


                    }, param => true);
                }
                return _BrowseLogoCommand;
            }
        }


        RelayCommand _BrowseXamarinSolutionCommand;
        public ICommand BrowseXamarinSolutionCommand
        {
            get
            {
                if (_BrowseXamarinSolutionCommand == null)
                {
                    _BrowseXamarinSolutionCommand = new RelayCommand(async param =>
                    {
                        LogoFilePath = @"U:\SOFTWARE\HackerspaceApp\resources\logo_transparent_for_icons.png";

                        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

                        DialogResult result = folderBrowserDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            XamarinSolutionPath = folderBrowserDialog1.SelectedPath;

                            var scanner = new XamarinSolutionScanner(XamarinSolutionPath);

                            var iosIcons = await scanner.GetIosIcons();

                            IosIcons = new ObservableCollection<IconFileViewModel>(iosIcons.Select(z => new IconFileViewModel(z)));

                            var androidIcons = await scanner.GetAndroidIcons();

                            AndroidIcons = new ObservableCollection<IconFileViewModel>(androidIcons.Select(z => new IconFileViewModel(z)));

                            var windowsUniversalIcons = await scanner.GetWindowsUniversalIcons();

                            WindowsUniversalIcons = new ObservableCollection<IconFileViewModel>(windowsUniversalIcons.Select(z => new IconFileViewModel(z)));
                        }



                    }, param => true);
                }
                return _BrowseXamarinSolutionCommand;
            }
        }


        RelayCommand _GenerateIconsCommand;
        public ICommand GenerateIconsCommand
        {
            get
            {
                if (_GenerateIconsCommand == null)
                {
                    _GenerateIconsCommand = new RelayCommand(param =>
                    {


                    }, param => true);
                }
                return _GenerateIconsCommand;
            }
        }

    }
}
