using System.Collections.Generic;
using System.IO;
using Ader.Text;

namespace netdecode
{
    class GameStringTable
    {
        IDictionary<string, string> Strings;
        StringTokenizer Tok;

        public GameStringTable(TextReader reader)
        {
            Tok = new StringTokenizer(reader);
            Tok.IgnoreWhiteSpace = true;
            Tok.SymbolChars = new char[] { ',', '{', '}' };

            Strings = new Dictionary<string, string>();
        }

        public bool LookupString(string key, List<string> args, out string str)
        {
            if (!Strings.ContainsKey(key))
            {
                str = string.Empty;
                return false;
            }

            str = Strings[key];

            int i = 0;
            foreach (var arg in args)
            {
                i++;
                str = str.Replace("%s" + i, arg); 
            }

            return true;
        }

        public bool Parse()
        {
            Token token;
            do
            {
                token = NextToken();
                
                if (token.Value.Equals("Tokens"))
                {
                    token = NextToken(); // Skip unneeded tokens
                    
                    do
                    {
                        token = NextToken();

                        if (token.Kind == TokenKind.Comment)
                            continue;
                        
                        var key = token;
                        var value = NextToken();

                        Strings[key.Value] = value.Value;
                    } while (!token.Value.Contains("}"));
                }
            } while (token.Kind != TokenKind.EOF);

            return true;
        }

        Token NextToken()
        {
            Token token;
            do 
            {
                token = Tok.Next();
            } while (token.Kind == TokenKind.EOL);
            return token;
        }
    }
}
