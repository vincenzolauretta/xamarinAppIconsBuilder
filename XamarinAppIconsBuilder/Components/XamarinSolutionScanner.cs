using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAppIconsBuilder.Components
{
    public class XamarinSolutionScanner
    {
        private string _xamarinSolutionRootPath { get; set; }

        public XamarinSolutionScanner(string xamarinSolutionRootPath)
        {
            _xamarinSolutionRootPath = xamarinSolutionRootPath;
        }

        public async Task<string[]> GetAndroidIcons()
        {
            return await Task.Run(async () =>
            {
                var mipmapFolders = Directory.GetDirectories(_xamarinSolutionRootPath, "*mipmap*", SearchOption.AllDirectories)
                .Where(z=>!z.Contains("\\obj\\"))
                .ToArray();

                List<string> icons = new List<string>();

                foreach (var f in mipmapFolders)
                {
                    icons = icons
                        .Concat(Directory.GetFiles(f, "*.png", SearchOption.TopDirectoryOnly))
                        .ToList();
                }

                return icons
                    .ToArray();
            });
        }

        public async Task<string[]> GetWindowsUniversalIcons()
        {
            return await Task.Run(async () =>
            {
                var assetsFolders = Directory.GetDirectories(_xamarinSolutionRootPath, "*Assets*", SearchOption.AllDirectories)
                .Where(z => !z.Contains("\\obj\\"))
                .Where(z => !z.Contains("\\bin\\"))
                .Where(z=>z.Contains(".UWP"))
                .ToArray();

                List<string> icons = new List<string>();

                foreach (var f in assetsFolders)
                {
                    icons = icons
                        .Concat(Directory.GetFiles(f, "*.png", SearchOption.TopDirectoryOnly))
                        .ToList();
                }

                return icons
                    .ToArray();
            });
        }

        public async Task<string[]> GetIosIcons()
        {
            return await Task.Run(async () =>
            {
                var assetsFolders = Directory.GetDirectories(_xamarinSolutionRootPath, "*AppIcon.appiconset*", SearchOption.AllDirectories)
                .Where(z => !z.Contains("\\obj\\"))
                .Where(z => !z.Contains(".macOS\\"));

                List<string> icons = new List<string>();

                foreach (var f in assetsFolders)
                {
                    icons = icons
                        .Concat(Directory.GetFiles(f, "*.png", SearchOption.TopDirectoryOnly))
                        .ToList();
                }

                return icons
                    .ToArray();
            });
        }
    }
}
