using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using quotation;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            new UnitTest1().Run();

            System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void TestMethod2()
        {

            var test = new test();

            test.Main();
        }


        private async Task Run()
        {

            var firebase = new FirebaseClient("https://best-quotes-24f45.firebaseio.com/");
            var writerName = await firebase
                .Child("WriterItem")
                .OrderByKey()
                .StartAt("PkWriterId")
                .LimitToFirst(2)
                .OnceAsync<IList<WriterItem>>();
            Run().Start();
        }

    }

    public class WriterItem
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "fkCategoryId")]
        public string FkCategoryId { get; set; }
        [JsonProperty(PropertyName = "writerName")]
        public string WriterName { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "PkWriterId")]
        public string PkWriterId { get; set; }
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty(PropertyName = "IsDaily")]
        public string IsDaily { get; set; }
    }


}
