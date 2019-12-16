using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators
{
    /// <summary>
    /// Stream writer with support csharp code file indent and some helpful methods for generate consistent csharp code.
    /// </summary>
    public class CSharpWriter : System.IO.StreamWriter
    {
        public CSharpWriter(string fileName)
            : base(fileName, false, Encoding.UTF8)
        {
            IndentString = string.Empty;
        }

        public CSharpWriter(System.IO.Stream baseStream)
            : base(baseStream, Encoding.UTF8)
        {
            IndentString = string.Empty;
        }

        /// <summary>
        /// Current indent level. <see cref="CreateIndentScope(bool)"/> or <see cref="Indent"/> and <see cref="Unindent(bool)"/>
        /// </summary>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Current indent string. <see cref="CreateIndentScope(bool)"/> or <see cref="Indent"/> and <see cref="Unindent(bool)"/>
        /// </summary>
        public string IndentString { get; private set; }

        public char IndentChar { get; private set; } = ' ';

        public int IndentCharCount { get; private set; } = 4;

        private class IndentScope : IDisposable
        {
            private bool AutoBracket { get; }
            private CSharpWriter Stream { get; }

            public IndentScope(bool autoBracket, CSharpWriter cSharpFileStream)
            {
                AutoBracket = autoBracket;
                Stream = cSharpFileStream;
            }

            public void Dispose()
            {
                Stream.Unindent(AutoBracket);
            }
        }

        /// <summary>
        /// Increment indent level and create <see cref="IDisposable"/> object for this indent.
        /// </summary>
        /// <param name="autoBracket"></param>
        /// <returns></returns>
        public IDisposable CreateIndentScope(bool autoBracket = true)
        {
            Indent(autoBracket);
            return new IndentScope(autoBracket, this);
        }

        /// <summary>
        /// Increment indent level.
        /// </summary>
        /// <param name="autoBracket"></param>
        public void Indent(bool autoBracket = true)
        {
            if (autoBracket)
                WriteCodeLine("{");
            IndentLevel++;
            IndentString = new string(IndentChar, IndentCharCount * IndentLevel);
        }

        /// <summary>
        /// Decrement indent level.
        /// </summary>
        /// <param name="autoBracket"></param>
        public void Unindent(bool autoBracket = true)
        {
            if (IndentLevel < 1) throw new InvalidOperationException("Indent level is too low.");
            IndentLevel--;
            IndentString = new string(IndentChar, IndentCharCount * IndentLevel);

            if (autoBracket)
                WriteCodeLine("}");
        }

        /// <summary>
        /// Write single indented code line.
        /// </summary>
        /// <param name="lineContent"></param>
        public void WriteCodeLine(string lineContent)
        {
            Write(IndentString);
            WriteLine(lineContent);
        }

        private HashSet<string> _usingDefinitions = new HashSet<string>();

        /// <summary>
        /// Write namespace declaration and return <see cref="IDisposable"/> indent for namespace
        /// </summary>
        /// <param name="namespaceParts">All parts from namespace</param>
        /// <returns></returns>
        public IDisposable WriteNamespaceStart(params string[] namespaceParts)
        {
            WriteLine("namespace {0}",string.Join(".", namespaceParts));
            return CreateIndentScope();
        }

        /// <summary>
        /// Write using statement. You can declare alias for namespace.
        /// </summary>
        /// <param name="fullNamespace"></param>
        /// <param name="alias"></param>
        public void WriteUsing(string fullNamespace, string alias = null)
        {
            if (_usingDefinitions.Contains(fullNamespace)) return;

            if (alias == null)
                WriteCodeLine($"using {fullNamespace};");
            else
                WriteCodeLine($"using {alias} = {fullNamespace};");

            _usingDefinitions.Add(fullNamespace);
        }

        /// <summary>
        /// Write documentation block :summmary:
        /// </summary>
        /// <param name="documentationContent"></param>
        public void WriteSummary(string documentationContent)
        {
            var trimmedContent = documentationContent?.Trim();
            if (string.IsNullOrEmpty(trimmedContent)) return;

            var lines = trimmedContent.Split(new[] { '\n' }, StringSplitOptions.None);
            if (lines.Length == 1)
            {
                WriteLine($"{IndentString}/// <summary>{lines[0]}</summary>");
            }
            else
            {
                WriteLine($"{IndentString}/// <summary>");
                foreach (var line in trimmedContent.Split(new[] { '\n' }, StringSplitOptions.None))
                {
                    WriteLine($"{IndentString}/// {line}");
                }
                WriteLine($"{IndentString}/// </summary>");
            }
        }

        /// <summary>
        /// Write documentation block :param:
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="parameterDescription"></param>
        public void WriteParameterDoc(string paramName, string parameterDescription)
        {

            var trimmedContent = parameterDescription?.Trim();
            if (string.IsNullOrEmpty(trimmedContent)) return;

            Write($"{IndentString}/// <param name=\"{paramName}\">");
            var lines = trimmedContent.Split(new[] { '\n' }, StringSplitOptions.None);
            if (lines.Length == 1)
            {
                Write(lines[0]);
            }
            else
            {
                foreach (var line in lines)
                {
                    WriteLine($"{IndentString}/// {line}");
                }
            }            
            WriteLine("</param>");
        }
    }
}
