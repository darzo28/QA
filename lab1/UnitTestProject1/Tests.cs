using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace lab1.tests
{
    [TestClass]
    public class lab1Tests
    {
        const string outputFilePath = "../../../output.txt", inputFilePath = "../../../input.txt";

        [TestMethod]
        public void TestProgram()
        {
            File.WriteAllText(outputFilePath, string.Empty);
            using StreamWriter output = new StreamWriter(outputFilePath, true);

            using (var inputFile = new StreamReader(inputFilePath))
            {
                int testNumber = 1;
                string argsLine;

                while ((argsLine = inputFile.ReadLine()) != null)
                {
                    string[] testArgs = Regex.Replace(argsLine, @"\s+", " ").Split();
                    string expectedResult = inputFile.ReadLine();

                    var strWriter = new StringWriter();
                    Console.SetOut(strWriter);
                    Console.SetError(strWriter);
                    Program.Main(testArgs);
                    string result = strWriter.ToString();

                    if (result == expectedResult)
                    {
                        output.WriteLine($"Test {testNumber} \"{argsLine} {expectedResult}\" > success;");
                    }
                    else
                    {
                        output.WriteLine($"Test {testNumber} \"{argsLine} {expectedResult}\" > error;");
                    }

                    Assert.AreEqual(result, expectedResult);

                    testNumber++;
                }
            }
        }
    }
}
