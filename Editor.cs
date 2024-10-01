// Copyright (c) 2024 RFull Development
// This source code is managed under the MIT license. See LICENSE in the project root.
using Microsoft.VisualBasic.FileIO;

namespace MatrixRotator
{
    public class Editor(string delimiter = ",", string? newline = null)
    {
        public string[,] Data { get; private set; } = { };

        private readonly string _delimiter = delimiter;
        private readonly string _newline = newline ?? Environment.NewLine;

        public void Load(StreamReader reader)
        {
            using TextFieldParser parser = new(reader)
            {
                TextFieldType = FieldType.Delimited
            };
            parser.SetDelimiters(_delimiter);
            List<string[]> list = [];
            while (!parser.EndOfData)
            {
                string[]? fields = parser.ReadFields();
                if (fields == null)
                {
                    continue;
                }
                list.Add(fields);
            }

            int rowCount = list.Count;
            int columnCount = list[0].Length;
            string[,] newData = new string[rowCount, columnCount];
            Parallel.For(0, rowCount, i =>
            {
                Parallel.For(0, columnCount, j =>
                {
                    newData[i, j] = list[i][j];
                });
            });
            Data = newData;
        }

        public void Save(StreamWriter writer)
        {
            int rowCount = Data.GetLength(0);
            int columnCount = Data.GetLength(1);
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var row = Enumerable.Range(0, columnCount)
                    .Select(columnIndex => Data[rowIndex, columnIndex]);
                string line = string.Join(_delimiter, row);
                writer.WriteLine(line);
            }
        }

        public void Rotate()
        {
            int rowCount = Data.GetLength(0);
            int columnCount = Data.GetLength(1);
            string[,] newData = new string[columnCount, rowCount];
            Parallel.For(0, rowCount, i =>
            {
                Parallel.For(0, columnCount, j =>
                {
                    newData[j, i] = Data[i, j];
                });
            });
            Data = newData;
        }
    }
}
