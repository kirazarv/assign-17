using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for FileStorageTest
    /// </summary>
    [TestClass]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";

        public const string SPACE_STRING = " ";
        //public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "text";
        public const string REPEATED_STRING = "AA";
        public const string WRONG_SIZE_CONTENT_STRING = "TEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtext";
        //public const string TIC_TOC_TOE_STRING = "tictoctoe.game";

        public const int NEW_SIZE = 5;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        /* ПРОВАЙДЕРЫ */

        //static object[] NewFilesData =
        //{
        //    new object[] { new File(REPEATED_STRING, CONTENT_STRING) },
        //    new object[] { new File(SPACE_STRING, WRONG_SIZE_CONTENT_STRING) },
        //    new object[] { new File(FILE_PATH_STRING, CONTENT_STRING) }
        //};

        //static object[] FilesForDeleteData =
        //{
        //    new object[] { new File(REPEATED_STRING, CONTENT_STRING), REPEATED_STRING },
        //    new object[] { null, TIC_TOC_TOE_STRING }
        //};

        //static object[] NewExceptionFileData = {
        //    new object[] { new File(REPEATED_STRING, CONTENT_STRING) }
        //};

        /* Тестирование записи файла с корректным размером */
        [TestMethod]
        public void WriteTest()
        {
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            //act and assert
            Assert.True(storage.Write(file));
            storage.DeleteAllFiles();
        }
        [TestMethod]

        public void WriteTestWrongSize()
        {
            //arrange
            File file = new File(REPEATED_STRING, WRONG_SIZE_CONTENT_STRING);
            //act and assert
            Assert.False(storage.Write(file));

        }

        /* Тестирование записи дублирующегося файла */
        [TestMethod]
        public void WriteExceptionTest() {
            bool isException = false;
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            File file2 = new File(REPEATED_STRING, CONTENT_STRING);
            //act and assert
            try
            {
                storage.Write(file2);
                //Assert.False(storage.Write(file2));
                //storage.DeleteAllFiles();
            }
            catch (FileNameAlreadyExistsException)
            {
                isException = true;
            }
            Assert.True(isException, NO_EXPECTED_EXCEPTION_EXCEPTION);
            storage.DeleteAllFiles();
        }

        /* Тестирование проверки существования файла */
        [TestMethod]
        public void IsExistsTest() {
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            String name = file.GetFilename();
            storage.Write(file);
            Assert.True(storage.IsExists(name));
            storage.DeleteAllFiles();
        }

        /* Тестирование удаления файла */
        [TestMethod]
        public void DeleteTest() {
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            String name = file.GetFilename();
            //act and assert
            Assert.True(storage.Delete(name));
        }

        /* Тестирование получения файлов */
        [TestMethod]
        public void GetFilesTest()
        {
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            File file1 = new File("bb", "text1");
            storage.Write(file1);
            File file2 = new File("cc", "text2");
            storage.Write(file2);

            //assert
            foreach (File el in storage.GetFiles())
            {
                Assert.NotNull(el);
            }
            storage.DeleteAllFiles();
        }

        // Почти эталонный
        /* Тестирование получения файла */
        [TestMethod]
        public void GetFileTest()
        {
            //arrange
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            //act 
            File actualfile = storage.GetFile(file.GetFilename());
            //assert
            bool same = actualfile.GetFilename().Equals(file.GetFilename()) && actualfile.GetSize().Equals(file.GetSize());
            Assert.IsTrue(same, string.Format("There is some differences in {0} or {1}", file.GetFilename(), file.GetSize()));
        }
        /// <summary>
        /// тестирование функции уменьшения доступного места в хранилище строка 51 FileStorage.cs
        /// </summary>
        [TestMethod]
        public void WriteFilesRunOutOfSpace(){
            //arrange
            Console.WriteLine($"av size{storage.AvailableSize}");
            File file = new File("aa", "123456789112");
            Console.WriteLine(file.GetSize());
            bool isWrittenFile=storage.Write(file);
       
            File file1 = new File("bb", "1234567891");
            Console.WriteLine(file1.GetSize());
            bool isWrittenFile1= storage.Write(file1);
            Console.WriteLine($"av size{ storage.AvailableSize}");

            File file2 = new File("cc", "12345678911234");
            Console.WriteLine(file2.GetSize());
            bool isWrittenFile2=storage.Write(file2);
            Assert.True(isWrittenFile&&isWrittenFile1&&!isWrittenFile2);
            storage.DeleteAllFiles();
        }

        /* Тестирование удаления всех файлов */
        [TestMethod]
        public void DeleteAllTest()
        {
            //arrange
            File file = new File("vv", CONTENT_STRING);
            storage.Write(file);
            File file1 = new File("aa", CONTENT_STRING);
            storage.Write(file1);
            File file2 = new File("bb", CONTENT_STRING);
            storage.Write(file2);
            Console.WriteLine(storage.GetFiles().Count);
            //act 
            bool areAllFilesDeleted = storage.DeleteAllFiles();

            // Assert
            Assert.True(areAllFilesDeleted);
            Assert.AreEqual(0, storage.GetFiles().Count); // Проверка, что в хранилище нет файлов

        }
    }
}
