/********************************************************8
 *	Author: Andrew Deren
 *	Date: July, 2004
 *	http://www.adersoftware.com
 * 
 *	StringTokenizer class. You can use this class in any way you want
 * as long as this header remains in this file.
 * 
 **********************************************************/

using System;
using System.Diagnostics;
using System.IO;

namespace Ader.Text
{
    public enum TokenKind
    {
        Unknown,
        Word,
        Number,
        QuotedString,
        WhiteSpace,
        Symbol,
        Comment,
        EOL,
        EOF
    }

    [DebuggerDisplay("Kind = {Kind}, Value = {Value}")]
    public class Token
    {
        public Token(TokenKind kind, string value, int line, int column)
        {
            Kind = kind;
            Value = value;
            Line = line;
            Column = column;
        }

        public int Column { get; }
        public TokenKind Kind { get; }
        public int Line { get; }
        public string Value { get; }
    }

    /// <summary>
    ///     StringTokenizer tokenized string (or stream) into tokens.
    /// </summary>
    public class StringTokenizer
    {
        private const char EOF = (char) 0;
        private readonly string data;
        private int column;
        private int line;
        private int pos; // position within data
        private int saveCol;
        private int saveLine;
        private int savePos;

        public StringTokenizer(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            data = reader.ReadToEnd();

            Reset();
        }

        public StringTokenizer(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            this.data = data;

            Reset();
        }

        /// <summary>
        ///     gets or sets which characters are part of TokenKind.Symbol
        /// </summary>
        public char[] SymbolChars { get; set; }

        /// <summary>
        ///     if set to true, white space characters will be ignored,
        ///     but EOL and whitespace inside of string will still be tokenized
        /// </summary>
        public bool IgnoreWhiteSpace { get; set; }

        private void Reset()
        {
            IgnoreWhiteSpace = false;
            SymbolChars = new[]
            {
                '=', '+', '-', '/', ',', '.', '*', '~', '!', '@', '#', '$', '%', '^', '&', '(', ')', '{', '}', '[', ']',
                ':', ';', '<', '>', '?', '|', '\\'
            };

            line = 1;
            column = 1;
            pos = 0;
        }

        protected char LA(int count)
        {
            if (pos + count >= data.Length)
                return EOF;
            return data[pos + count];
        }

        protected char Consume()
        {
            var ret = data[pos];
            pos++;
            column++;

            return ret;
        }

        protected Token CreateToken(TokenKind kind, string value)
        {
            return new Token(kind, value, line, column);
        }

        protected Token CreateToken(TokenKind kind)
        {
            var tokenData = data.Substring(savePos, pos - savePos);
            if (kind == TokenKind.QuotedString)
                tokenData = tokenData.Substring(1, tokenData.Length - 2);
            return new Token(kind, tokenData, saveLine, saveCol);
        }

        public Token Next()
        {
            ReadToken:

            var ch = LA(0);
            switch (ch)
            {
                case EOF:
                    return CreateToken(TokenKind.EOF, string.Empty);

                case ' ':
                case '\t':
                {
                    if (IgnoreWhiteSpace)
                    {
                        Consume();
                        goto ReadToken;
                    }
                    return ReadWhitespace();
                }
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadNumber();

                case '\r':
                {
                    StartRead();
                    Consume();
                    if (LA(0) == '\n')
                        Consume(); // on DOS/Windows we have \r\n for new line

                    line++;
                    column = 1;

                    return CreateToken(TokenKind.EOL);
                }
                case '\n':
                {
                    StartRead();
                    Consume();
                    line++;
                    column = 1;

                    return CreateToken(TokenKind.EOL);
                }

                case '"':
                {
                    return ReadString();
                }

                case '/':
                {
                    return ReadComment();
                }

                default:
                {
                    if (char.IsLetter(ch) || ch == '_')
                        return ReadWord();
                    if (IsSymbol(ch))
                    {
                        StartRead();
                        Consume();
                        return CreateToken(TokenKind.Symbol);
                    }
                    StartRead();
                    Consume();
                    return CreateToken(TokenKind.Unknown);
                }
            }
        }

        /// <summary>
        ///     save read point positions so that CreateToken can use those
        /// </summary>
        private void StartRead()
        {
            saveLine = line;
            saveCol = column;
            savePos = pos;
        }

        /// <summary>
        ///     reads all whitespace characters (does not include newline)
        /// </summary>
        /// <returns></returns>
        protected Token ReadWhitespace()
        {
            StartRead();

            Consume(); // consume the looked-ahead whitespace char

            while (true)
            {
                var ch = LA(0);
                if (ch == '\t' || ch == ' ')
                    Consume();
                else
                    break;
            }

            return CreateToken(TokenKind.WhiteSpace);
        }

        /// <summary>
        ///     reads all comment characters until newline
        /// </summary>
        /// <returns></returns>
        protected Token ReadComment()
        {
            StartRead();

            Consume(); // consume the looked-ahead comment chars
            Consume();

            while (true)
            {
                var ch = LA(0);
                if (ch != '\n')
                    Consume();
                else
                    break;
            }

            Consume();

            return CreateToken(TokenKind.Comment);
        }

        /// <summary>
        ///     reads number. Number is: DIGIT+ ("." DIGIT*)?
        /// </summary>
        /// <returns></returns>
        protected Token ReadNumber()
        {
            StartRead();

            var hadDot = false;

            Consume(); // read first digit

            while (true)
            {
                var ch = LA(0);
                if (char.IsDigit(ch))
                    Consume();
                else if (ch == '.' && !hadDot)
                {
                    hadDot = true;
                    Consume();
                }
                else
                    break;
            }

            return CreateToken(TokenKind.Number);
        }

        /// <summary>
        ///     reads word. Word contains any alpha character or _
        /// </summary>
        protected Token ReadWord()
        {
            StartRead();

            Consume(); // consume first character of the word

            while (true)
            {
                var ch = LA(0);
                if (char.IsLetter(ch) || ch == '_')
                    Consume();
                else
                    break;
            }

            return CreateToken(TokenKind.Word);
        }

        /// <summary>
        ///     reads all characters until next " is found.
        ///     If "" (2 quotes) are found, then they are consumed as
        ///     part of the string
        /// </summary>
        /// <returns></returns>
        protected Token ReadString()
        {
            StartRead();

            Consume(); // read "

            while (true)
            {
                var ch = LA(0);
                if (ch == EOF)
                    break;
                if (ch == '\r') // handle CR in strings
                {
                    Consume();
                    if (LA(0) == '\n') // for DOS & windows
                        Consume();

                    line++;
                    column = 1;
                }
                else if (ch == '\n') // new line in quoted string
                {
                    Consume();

                    line++;
                    column = 1;
                }
                else if (ch == '"')
                {
                    Consume();
                    if (LA(0) != '"')
                        break; // done reading, and this quotes does not have escape character
                    Consume(); // consume second ", because first was just an escape
                }
                else
                    Consume();
            }

            return CreateToken(TokenKind.QuotedString);
        }

        /// <summary>
        ///     checks whether c is a symbol character.
        /// </summary>
        protected bool IsSymbol(char c)
        {
            for (var i = 0; i < SymbolChars.Length; i++)
                if (SymbolChars[i] == c)
                    return true;

            return false;
        }
    }
}