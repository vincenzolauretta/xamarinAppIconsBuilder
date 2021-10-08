using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamarinAppIconsBuilder.ViewModels
{
    public class IconFileViewModel : BaseViewModel
    {
        bool _IsUnsupported;
        public bool IsUnsupported
        {
            get { return _IsUnsupported; }
            set
            {
                if (_IsUnsupported != value)
                {
                    _IsUnsupported = value;
                    OnPropertyChanged(nameof(IsUnsupported));
                }
            }
        }


        int _Width;
        public int Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }
        int _Height;
        public int Height
        {
            get { return _Height; }
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }
        string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }
        string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                if (_FilePath != value)
                {
                    _FilePath = value;
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }
        byte[] _ImageBytes;
        public byte[] ImageBytes
        {
            get { return _ImageBytes; }
            set
            {
                if (_ImageBytes != value)
                {
                    _ImageBytes = value;
                    OnPropertyChanged(nameof(ImageBytes));
                }
            }
        }

        public IconFileViewModel()
        {

        }

        public IconFileViewModel(string filePath)
        {
            FilePath = filePath;

            FileName = System.IO.Path.GetFileName(FilePath);

            GetFileInfo();
        }

        async Task GetFileInfo()
        {
            await ReadPngImageSize(FilePath);
        }

        public async Task ReadPngImageSize(string Filename)
        {
            await Task.Run(() =>
            {
                using (var br = new BinaryReader(File.OpenRead(Filename)))
                {
                    br.BaseStream.Position = 16;
                    byte[] widthbytes = new byte[sizeof(int)];
                    for (int i = 0; i < sizeof(int); i++) widthbytes[sizeof(int) - 1 - i] = br.ReadByte();
                    this.Width = BitConverter.ToInt32(widthbytes, 0);
                    byte[] heightbytes = new byte[sizeof(int)];
                    for (int i = 0; i < sizeof(int); i++) heightbytes[sizeof(int) - 1 - i] = br.ReadByte();
                    this.Height = BitConverter.ToInt32(heightbytes, 0);
                }
            });
        }


        RelayCommand _OpenParentFolderCommand;
        public ICommand OpenParentFolderCommand
        {
            get
            {
                if (_OpenParentFolderCommand == null)
                {
                    _OpenParentFolderCommand = new RelayCommand(param =>
                    {
                        string filePath = param as string;
                        if (!File.Exists(filePath))
                        {
                            return;
                        }

                        // combine the arguments together
                        // it doesn't matter if there is a space after ','
                        string argument = "/select, \"" + filePath + "\"";

                        System.Diagnostics.Process.Start("explorer.exe", argument);

                    }, param => true);
                }
                return _OpenParentFolderCommand;
            }
        }

    }
}
