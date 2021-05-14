using System;
using System.IO;
using NUnit.Framework;

namespace Chess
{
    [TestFixture]
    public class ChessProblem_Test
    {
        [Test]
        public void RepeatedMethodCallDoNotChangeBehaviour()
        {
            var boardLines = new[]
            {
                "        ",
                "        ",
                "        ",
                "   q    ",
                "    K   ",
                " Q      ",
                "        ",
                "        ",
            };
            ChessProblem.LoadFrom(boardLines);
            var chessStatus = ChessProblem.CalculateChessStatus();
            Assert.AreEqual(ChessStatus.Check, chessStatus);

            // Now check that internal board modifications during the first call do not change answer
            chessStatus = ChessProblem.CalculateChessStatus();
            Assert.AreEqual(ChessStatus.Check, chessStatus);
        }

        [Test]
        public void AllTests()
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            var testsCount = 0;
            foreach (var filename in Directory.GetFiles(Path.Combine(dir, "ChessTests"), "*.in"))
            {
                TestOnFile(filename);
                testsCount++;
            }
            Console.WriteLine("Tests passed: " + testsCount);
        }

        private static void TestOnFile(string filename)
        {
            var boardLines = File.ReadAllLines(filename);
            ChessProblem.LoadFrom(boardLines);
            var expectedAnswer = File.ReadAllText(Path.ChangeExtension(filename, ".ans")).Trim();
            var chessStatus = ChessProblem.CalculateChessStatus();
            Assert.AreEqual(expectedAnswer, chessStatus.ToString().ToLower(), "Failed test " + filename);
        }
    }
}