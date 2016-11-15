using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnitTestProject1
{
    using System;
    using System.Threading.Tasks;

    using Firebase.Database;
    using Firebase.Database.Query;

    public class test
    {
        public static void Main()
        {
            new test().Run().Wait();
        }

        private async Task Run()
        {
            var firebase = new FirebaseClient("https://best-quotes-24f45.firebaseio.com/");

            var dinos = await firebase
              .Child("Writer")
              .OnceAsync<List<Writer>>();

            foreach (var dino in dinos)
            {
                Console.WriteLine($"{dino.Key} is {dino.Object.Count}m high.");
            }

            //var firebase = new FirebaseClient("https://best-quotes-24f45.firebaseio.com/");

            //// add new item to list of data and let the client generate new key for you (done offline)
            //var category = await firebase
            //  .Child("Category")
            //  .PostAsync(new List<CategoryItem>()
            //  {
            //      new CategoryItem()
            //      {
            //          PkCategoryId = 1,
            //          CategoryName = "Age",
            //          IsDaily = false,
            //          WriterName = "Bob"
            //      },
            //      new CategoryItem()
            //      {
            //          PkCategoryId = 2,
            //          CategoryName = "Mothers Day",
            //          IsDaily = false,
            //          WriterName = "amet"
            //      },
            //      new CategoryItem()
            //      {
            //          PkCategoryId = 3,
            //          CategoryName = "Fathers Day",
            //          IsDaily = false,
            //          WriterName = "anil"
            //      },
            //      new CategoryItem()
            //      {
            //          PkCategoryId = 4,
            //          CategoryName = "Animals Day",
            //          IsDaily = false,
            //          WriterName = "ahmet"
            //      }
            //  });
        }


        public class WriterItem
        {
            public string Id { get; set; }

            public string FkCategoryId { get; set; }

            public string WriterName { get; set; }

            public string Content { get; set; }

            public string PkWriterId { get; set; }

            public string CategoryName { get; set; }

            public string IsDaily { get; set; }
        }

        public class CategoryItem
        {
            public int PkCategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool IsDaily { get; set; }
            public string WriterName { get; set; }
        }

        public class Writer
        {
            public int PkWriterId { get; set; }
            public string WriterName { get; set; }
            public string DailyContent { get; set; }
            public bool IsDaily { get; set; }
        }

    }
}
