using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    [TestClass]
    public class FileTest
    {

        public const string SIZE_EXCEPTION = "Wrong size";
        public const string NAME_EXCEPTION = "Wrong name";
        public const string SPACE_STRING = " ";
       // public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string NAME_STRING = "AA";
        public double lenght;

        /* ПРОВАЙДЕР */
        //static object[] FilesData =
        //{
        //    new object[] {new File(FILE_PATH_STRING, CONTENT_STRING), FILE_PATH_STRING, CONTENT_STRING},
        //    new object[] { new File(SPACE_STRING, SPACE_STRING), SPACE_STRING, SPACE_STRING}
        //};

        /* Тестируем получение размера */
        [TestMethod]
        public void GetSizeTest()
        {
            //arrange
            File file = new File(NAME_STRING, CONTENT_STRING);
            //act
            double size = file.GetSize();
            lenght = CONTENT_STRING.Length / 2;
            //assert
            Assert.AreEqual(lenght,size);
            
        }

        /* Тестируем получение имени */
        [TestMethod]
        public void GetFilenameTest()
        {
            //arrange
            File file=new File(NAME_STRING, CONTENT_STRING);
            //act and assert
            Assert.AreEqual(NAME_STRING,file.GetFilename());
        }

    }
}
