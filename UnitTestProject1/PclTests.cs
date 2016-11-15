using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCLStorage;
using FileAccess = PCLStorage.FileAccess;

namespace UnitTestProject1
{
    [TestClass]
    public class PclTests
    {

        [TestMethod]
        public void TestMethod1()
        {
            new PclTests().CreateRealFileAsync().Wait();
            System.Threading.Thread.Sleep(5000);

        }

        private IFolder rootFolder, folder;
        private IFile file;

        public async Task CreateRealFileAsync()
        {
            // get hold of the file system
            rootFolder = FileSystem.Current.RoamingStorage;

            // create a folder, if one does not exist already
            folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);

            // create a file, overwriting any existing file
            file = await folder.CreateFileAsync("MyFile.txt", CreationCollisionOption.ReplaceExisting);

            // populate the file with some text
            await file.WriteAllTextAsync("Sample Text...");
            var text = file.ReadAllTextAsync();
        }
    }
}
