using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KassaExpert.Util.LibTest
{
    internal static class Helper
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        public static IEnumerable<string> GetFilesInFolderSubFolder(string rootPath, string searchPattern)
        {
            var dir = new DirectoryInfo(rootPath);

            var files = dir.GetFiles(searchPattern, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                yield return file.FullName;
            }
        }
    }
}