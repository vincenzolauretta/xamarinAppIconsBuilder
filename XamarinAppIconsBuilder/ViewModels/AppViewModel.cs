using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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

        bool _UseSuggestedSizeAndroid;
        public bool UseSuggestedSizeAndroid
        {
            get { return _UseSuggestedSizeAndroid; }
            set
            {
                if (_UseSuggestedSizeAndroid != value)
                {
                    _UseSuggestedSizeAndroid = value;
                    OnPropertyChanged(nameof(UseSuggestedSizeAndroid));
                    SizeSliderEnabledAndroid = !value;
                }
            }
        }
        bool _SizeSliderEnabledAndroid;
        public bool SizeSliderEnabledAndroid
        {
            get { return _SizeSliderEnabledAndroid; }
            set
            {
                if (_SizeSliderEnabledAndroid != value)
                {
                    _SizeSliderEnabledAndroid = value;
                    OnPropertyChanged(nameof(SizeSliderEnabledAndroid));
                }
            }
        }


        int _LauncherIconSizePercentAndroid = 51;
        public int LauncherIconSizePercentAndroid
        {
            get { return _LauncherIconSizePercentAndroid; }
            set
            {
                if (_LauncherIconSizePercentAndroid != value)
                {
                    _LauncherIconSizePercentAndroid = value;
                    OnPropertyChanged(nameof(LauncherIconSizePercentAndroid));
                }
            }
        }
        int _IconSizePercentAndroid = 91;
        public int IconSizePercentAndroid
        {
            get { return _IconSizePercentAndroid; }
            set
            {
                if (_IconSizePercentAndroid != value)
                {
                    _IconSizePercentAndroid = value;
                    OnPropertyChanged(nameof(IconSizePercentAndroid));
                }
            }
        }



        private List<SupportedIconItemModel> _supportedIconsIos;
        private List<SupportedIconItemModel> _supportedIconsAndroid;
        private List<SupportedIconItemModel> _supportedIconsWindowsUniversal;

        public AppViewModel()
        {
            UseSuggestedSizeAndroid = true;

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
                        var openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                        openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF|All files (*.*)|*.*";
                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;

                        DialogResult dr = openFileDialog1.ShowDialog();
                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            LogoFilePath = openFileDialog1.FileName;
                        }

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
                        if (AndroidIcons == null)
                            return;

                        PreviewAndroidIcons = new ObservableCollection<IconFileViewModel>();

                        foreach (var icon in AndroidIcons)
                        {
                            var supportedIconConfig = _supportedIconsAndroid.FirstOrDefault(z => z.FileName == icon.FileName && z.Width == icon.Width && z.Height == icon.Height);

                            if (supportedIconConfig == null)
                            {
                                PreviewAndroidIcons.Add(new IconFileViewModel()
                                {
                                    Width = icon.Width,
                                    Height = icon.Height,
                                    ImageBytes = System.IO.File.ReadAllBytes(icon.FilePath)
                                });

                                continue;
                            }
                            else
                            {
                                var bmp = new Bitmap(icon.Width, icon.Height);

                                using (var logo = new Bitmap(LogoFilePath))
                                {
                                    FillBackgroundWithColor(ref bmp, LogoBackgroundColor);

                                    if (UseSuggestedSizeAndroid && supportedIconConfig.SuggestedSize > 0)
                                    {
                                        var scaledImage = ScaleImage(logo, supportedIconConfig.SuggestedSize, supportedIconConfig.SuggestedSize);
                                        var percent = supportedIconConfig.SuggestedSize * 100 / bmp.Height;

                                        int width = scaledImage.Width;
                                        int height = scaledImage.Height;

                                        int x = ((bmp.Width - width) / 2);
                                        int y = ((bmp.Height - height) / 2);

                                        CopyRegionIntoImage(logo, new Rectangle(0, 0, logo.Width, logo.Height), ref bmp, new Rectangle(x, y, width, height));

                                        PreviewAndroidIcons.Add(new IconFileViewModel()
                                        {
                                            Width = icon.Width,
                                            Height = icon.Height,
                                            ImageBytes = ImageToByte(bmp),
                                            FileName = icon.FileName
                                        });
                                    }
                                    else // use slider values
                                    {
                                        var percent = icon.FileName.Contains("launcher") ? LauncherIconSizePercentAndroid : IconSizePercentAndroid;

                                        int sliderSize = (int)((bmp.Height / 100.0f) * percent);
                                        var scaledImage2 = ScaleImage(logo, sliderSize, sliderSize);

                                        int width = scaledImage2.Width;
                                        int height = scaledImage2.Height;

                                        int x = ((bmp.Width - width) / 2);
                                        int y = ((bmp.Height - height) / 2);

                                        CopyRegionIntoImage(logo, new Rectangle(0, 0, logo.Width, logo.Height), ref bmp, new Rectangle(x, y, width, height));

                                        PreviewAndroidIcons.Add(new IconFileViewModel()
                                        {
                                            Width = icon.Width,
                                            Height = icon.Height,
                                            ImageBytes = ImageToByte(bmp),
                                            FileName = icon.FileName
                                        });
                                    }
                                }


                            }

                        }

                        // save to disk
                        //string outputPath = Guid.NewGuid().ToString();
                        //Directory.CreateDirectory(outputPath);
                        //foreach (var item in PreviewAndroidIcons)
                        //{
                        //    System.IO.File.WriteAllBytes($"{outputPath}\\{item.FileName}", item.ImageBytes);
                        //}

                    }, param => true);
                }
                return _GenerateIconsCommand;
            }
        }

        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
            {
                // Calculate x and y which center the image
                int y = (maxHeight / 2) - newHeight / 2;
                int x = (maxWidth / 2) - newWidth / 2;

                // Draw image on x and y with newWidth and newHeight
                graphics.DrawImage(image, x, y, newWidth, newHeight);
            }

            return newImage;
        }

        public static void FillBackgroundWithColor(ref Bitmap destBitmap, string hexColor)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                Color customColor = System.Drawing.ColorTranslator.FromHtml(hexColor);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                grD.FillRectangles(shadowBrush, new RectangleF[] { new RectangleF(0, 0, destBitmap.Width, destBitmap.Height) });
            }
        }
        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
