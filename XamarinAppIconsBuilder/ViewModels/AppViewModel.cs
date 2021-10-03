using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XamarinAppIconsBuilder.Components;

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

        ObservableCollection<string> _AndroidIcons;
        public ObservableCollection<string> AndroidIcons
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

        ObservableCollection<string> _IosIcons;
        public ObservableCollection<string> IosIcons
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

        ObservableCollection<string> _WindowsUniversalIcons;
        public ObservableCollection<string> WindowsUniversalIcons
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
                        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

                        DialogResult result = folderBrowserDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            XamarinSolutionPath = folderBrowserDialog1.SelectedPath;

                            var scanner = new XamarinSolutionScanner(XamarinSolutionPath);

                            var iosIcons = await scanner.GetIosIcons();

                            IosIcons = new ObservableCollection<string>(iosIcons);

                            var androidIcons = await scanner.GetAndroidIcons();

                            AndroidIcons = new ObservableCollection<string>(androidIcons);

                            var windowsUniversalIcons = await scanner.GetWindowsUniversalIcons();

                            WindowsUniversalIcons = new ObservableCollection<string>(windowsUniversalIcons);
                        }



                    }, param => true);
                }
                return _BrowseXamarinSolutionCommand;
            }
        }

    }
}
