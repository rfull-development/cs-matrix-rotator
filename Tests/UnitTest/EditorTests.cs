using System.Diagnostics;
using System.Text;

namespace MatrixRotator.Tests
{
    [TestClass]
    [TestCategory("Unit")]
    public class EditorTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void Load()
        {
            int rowCount = 4;
            int columnCount = 3;
            string input = GenerateCsvString(rowCount, columnCount);
            byte[] rawInput = Encoding.UTF8.GetBytes(input);
            using var stream = new MemoryStream(rawInput);
            using var reader = new StreamReader(stream);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var editor = new Editor();
            editor.Load(reader);

            stopwatch.Stop();
            double processingTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Processing time: {processingTime} seconds.");

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    string expected = $"{columnCount * rowIndex + columnIndex + 1}";
                    Assert.AreEqual(expected, editor.Data[rowIndex, columnIndex]);
                }
            }
        }

        [TestMethod]
        public void Save()
        {
            int rowCount = 4;
            int columnCount = 3;
            string input = GenerateCsvString(rowCount, columnCount);
            byte[] rawInput = Encoding.UTF8.GetBytes(input);
            var editor = new Editor();
            using var inputStream = new MemoryStream(rawInput);
            using (var reader = new StreamReader(inputStream))
            {
                editor.Load(reader);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using var outputStream = new MemoryStream();
            using (var writer = new StreamWriter(outputStream))
            {
                editor.Save(writer);
                writer.Flush();

                stopwatch.Stop();
                double processingTime = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine($"Processing time: {processingTime} seconds.");

                outputStream.Position = 0;
                using var reader = new StreamReader(outputStream);
                string output = reader.ReadToEnd();
                Assert.AreEqual(input, output);
            }
        }

        [TestMethod]
        public void Rotate()
        {
            int rowCount = 4;
            int columnCount = 3;
            string input = GenerateCsvString(rowCount, columnCount);
            byte[] rawInput = Encoding.UTF8.GetBytes(input);
            using var stream = new MemoryStream(rawInput);
            using var reader = new StreamReader(stream);
            var editor = new Editor();
            editor.Load(reader);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            editor.Rotate();

            stopwatch.Stop();
            double processingTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Processing time: {processingTime} seconds.");

            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    string expected = $"{columnCount * rowIndex + columnIndex + 1}";
                    Assert.AreEqual(expected, editor.Data[columnIndex, rowIndex]);
                }
            }
        }

        private static string GenerateCsvString(int rowCount, int columnCount)
        {
            StringBuilder builder = new();
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                string[] row = new string[columnCount];
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    row[columnIndex] = $"{columnCount * rowIndex + columnIndex + 1}";

                }
                string line = string.Join(",", row);
                builder.AppendLine(line);
            }
            return builder.ToString();
        }
    }
}
