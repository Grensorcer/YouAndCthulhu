using System;

namespace YouAndCthulhu
{
    public class Program
    {
        public static void CthulhuSlaver(string path)
        // Read a file, fills a function table and execute it, thus running a Cthulhu program.
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            FunctionTable ftable = new FunctionTable();
            foreach (string line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;
                Function f = LexerParser.LineToFunction(line, ftable);
                ftable.Add(f);
            }

            ftable.Execute();
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
                CthulhuSlaver(args[0]);
            else
                throw new ArgumentException("This program takes only 1" +
                                            " argument: the path to " +
                                            "a Cthulhu file.");
        }
    }
}
