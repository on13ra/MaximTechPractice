using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using WebAPI;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAlphabetCheck_ValidInput()
        {
            // Arrange
            string input = "abcdefg";

            // Act
            bool result = ProcessingController.AlphabetCheck(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAlphabetCheck_InvalidInput()
        {
            // Arrange
            string input = "abc123";

            // Act
            bool result = ProcessingController.AlphabetCheck(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPartition()
        {
            // Arrange
            char[] arr = { 'd', 'a', 'b', 'c' };
            int low = 0;
            int high = 3;

            // Act
            int pivotIndex = ProcessingController.Partition(arr, low, high);

            // Assert
            // Добавьте утверждения, чтобы проверить, что pivotIndex и элементы в массиве соответствуют ожиданиям.
            Assert.AreEqual(1, pivotIndex);
        }

        [TestMethod]
        public void TestProcessString()
        {
            // Arrange
            string input = "abcdefg";
            var requestLimiter = new RequestLimiter();
            var configuration = new Configuration();

            var controller = new ProcessingController(requestLimiter, configuration);

            // Act
            var result = controller.ProcessString(input);

            // Assert
            // Добавьте утверждения для проверки результата метода ProcessString.
            // Здесь важно убедиться, что код контроллера ведет себя как ожидалось.
        }
    }
}
