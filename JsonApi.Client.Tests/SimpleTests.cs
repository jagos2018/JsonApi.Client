using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JsonApi.Client.Tests
{
    [TestClass]
    public class SimpleTests
    {
        readonly string _bucketName = "TestBucket";
        readonly string _bucketData = "{\"data\": true}";

        public SimpleTests() {

        }

        [TestMethod]
        public void CreateObject() {
            string dataId = Common.Client.CreateObject(_bucketName, _bucketData).GetAwaiter().GetResult();
            Assert.IsNotNull(dataId);
        }

        [TestMethod]
        public void GetObjects() {
            var jsonObjs = Common.Client.GetObjects(_bucketName).GetAwaiter().GetResult();
            var jsonObj = Common.Client.GetObject(_bucketName, jsonObjs.First().Id).GetAwaiter().GetResult();
            Assert.IsNotNull(jsonObj);
            Assert.IsNotNull(jsonObj.Id);
            Assert.IsNotNull(jsonObj.Data);
        }

        [TestMethod]
        public void UpdateObject() {
            var jsonObjs = Common.Client.GetObjects(_bucketName)
                .GetAwaiter().GetResult();
            var jsonObj = Common.Client.GetObject(_bucketName, jsonObjs.First().Id)
                .GetAwaiter().GetResult();
            bool updated = Common.Client.UpdateObject(_bucketName, jsonObj.Id, jsonObj.Data + "new_data_added")
                .GetAwaiter().GetResult();

            Assert.IsNotNull(jsonObj);
            Assert.IsNotNull(jsonObj.Id);
            Assert.IsNotNull(jsonObj.Data);
            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void DeleteObject() {
            var jsonObjs = Common.Client.GetObjects(_bucketName)
                .GetAwaiter().GetResult();
            var jsonObj = Common.Client.GetObject(_bucketName, jsonObjs.First().Id)
                .GetAwaiter().GetResult();
            bool deleted = Common.Client.DeleteObject(_bucketName, jsonObj.Id)
                .GetAwaiter().GetResult();

            Assert.IsNotNull(jsonObj);
            Assert.IsNotNull(jsonObj.Id);
            Assert.IsNotNull(jsonObj.Data);
            Assert.IsTrue(deleted);
        }

        [TestInitialize]
        public void Init() {
            this.CreateObject();
        }

        [TestCleanup]
        public void Cleanup() {
            var jsonObjs = Common.Client.GetObjects(_bucketName)
                .GetAwaiter().GetResult();

            jsonObjs.ForEach((json) => {
                bool deleted = Common.Client.DeleteObject(_bucketName, json.Id)
                    .GetAwaiter().GetResult();
            });
        }
    }
}
