namespace NUnitTestMTP
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAlphabetCheck_ValidInput()
        {
            string input = "abcdefghijklmnopqrstuvwxyz";
            bool result = ProcessingController.AlphabetCheck(input);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestAlphabetCheck_InvalidInput()
        {
            string input = "1234567890";
            bool result = ProcessingController.AlphabetCheck(input);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestTreeSort_Sorting()
        {
            char[] arr = "dcba".ToCharArray();
            ProcessingController.TreeSort(arr, 0, arr.Length - 1);
            string sorted = new string(arr);
            Assert.AreEqual("abcd", sorted);
        }

        [Test]
        public void TestQuickSort_Sorting()
        {
            char[] arr = "dcba".ToCharArray();
            ProcessingController.QuickSort(arr, 0, arr.Length - 1);
            string sorted = new string(arr);
            Assert.AreEqual("abcd", sorted);
        }

        [Test]
        public void TestPartition_Partitioning()
        {
            char[] arr = "dcba".ToCharArray();
            int pivotIndex = ProcessingController.Partition(arr, 0, arr.Length - 1);
            Assert.AreEqual(2, pivotIndex);
        } 
    }
}