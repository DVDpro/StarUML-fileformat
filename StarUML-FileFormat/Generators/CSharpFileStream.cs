using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators
{
    public class CSharpFileStream : System.IO.StreamWriter
    {
        public CSharpFileStream(string fileName)
            : base(fileName, false, Encoding.UTF8)
        {
            IndentString = string.Empty;
        }

        public int IndentLevel { get; private set; }

        public string IndentString { get; private set; }

        public char IndentChar { get; set; } = ' ';

        public int IndentCharCount { get; set; } = 4;

        private class IndentScope : IDisposable
        {
            private bool AutoBraclet { get; }
            private CSharpFileStream Stream { get; }

            public IndentScope(bool autoBraclet, CSharpFileStream cSharpFileStream)
            {
                AutoBraclet = autoBraclet;
                Stream = cSharpFileStream;
            }

            public void Dispose()
            {
                Stream.Unindent(AutoBraclet);
            }
        }

        public IDisposable CreateIndentScope(bool autoBraclet = true)
        {
            if (autoBraclet)
                WriteCodeLine("{");
            Indent();
            return new IndentScope(autoBraclet, this);
        }

        public void Indent()
        {
            IndentLevel++;
            IndentString = new string(IndentChar, IndentCharCount * IndentLevel);
        }

        public void Unindent(bool autoBraclet = true)
        {
            if (IndentLevel < 1) throw new InvalidOperationException("Indent level is too low.");
            IndentLevel--;
            IndentString = new string(IndentChar, IndentCharCount * IndentLevel);

            if (autoBraclet)
                WriteCodeLine("}");
        }

        public void WriteCodeLine(string lineContent)
        {
            Write(IndentString);
            WriteLine(lineContent);
        }

        internal void WriteSummary(string documentationContent)
        {
            var trimmedContent = documentationContent?.Trim(); //.Trim('\r', '\n')
            if (string.IsNullOrEmpty(trimmedContent)) return;

            WriteLine($"{IndentString}/// <summary>");

            foreach (var line in trimmedContent.Split(new[] { '\n' }, StringSplitOptions.None))
            {
                WriteLine($"{IndentString}/// {line}");
            }

            WriteLine($"{IndentString}/// </summary>");
        }
    }
}
