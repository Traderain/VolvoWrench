using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;


//TODO: use this goldmine: https://github.com/LestaD/SourceEngine2007/blob/43a5c90a5ada1e69ca044595383be67f40b33c61/src_main/gameui/BaseSaveGameDialog.cpp#L285
namespace VolvoWrench.SaveStuff
{
    public class Flag
    {
        public string Ticks { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }

        public Flag(int t, float s, string type)
        {
            Ticks = t.ToString();
            Time = s.ToString(CultureInfo.InvariantCulture) + "s";
            Type = type;
        }
    }

    public class Listsave
    {
        const int SavegameMapnameLen = 32;
        const int SavegameCommentLen = 80;
        const int SavegameElapsedLen = 32;
        const int SavegameVersion = 0x0073;
        const int SectionMagicNumber = 0x54541234;
        const int SectionVersionNumber = 2;

        public static string Chaptername(int chapter)
        {
            #region MapSwitch
            switch (chapter)
            {
                case 1:
                    return "A Red Letter Day";
                case 2:
                    return "Route Kanal";
                case 3:
                    return "Water Hazard";
                case 4:
                    return "Black Mesa East";
                case 5:
                    return "We don't go to Ravenholm";
                case 6:
                    return "Highway 17";
                case 7:
                    return "Sandtraps";
                case 8:
                    return "Nova Prospekt";
                case 9:
                    return "Entanglement";
                case 10:
                    return "Anticitizen One";
                case 11:
                    return "Follow Freeman!";
                case 12:
                    return "Our Benefactors";
                case 13:
                    return "Dark Energy";
                default:
                    return "Mod/Unknown";
            }

            #endregion
        }
        public static SaveFile ParseFile(string file)
        {
            var result = new SaveFile();
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                var identifier = Encoding.ASCII.GetString(br.ReadBytes(4)).TrimEnd('\0');
                if (identifier != "JSAV")
                    throw new Exception("Not a save");
                result.FileName = file;
                result.Header = identifier;
                result.SaveVersion = br.ReadInt32();
                result.Size = br.ReadInt32();
                result.TokenCount = br.ReadInt32();
                result.Tokensize = br.ReadInt32();
                result.Size += result.Tokensize;
                var pSaveData = br.ReadChars(result.Size).ToString();
                int NumberOfFields;
                int nFieldSize;
                var pData = pSaveData;
                var pTokenList = new List<string>(result.TokenCount);
                for (int i = 0; i < result.TokenCount; i++)
                {
                    do
                    {
                        pTokenList[i] = (pData != null) ? pData : null;
                    } while (pTokenList[i] == null);
                }
                return result;
            }
        }
    }

    [Serializable]
    public class SaveFile
    {
        public string FileName { get; set; }
        public string Header { get; set; }
        public int SaveVersion { get; set; }
        public int Size { get; set; }
        public int Tokensize { get; set; }
        public int TokenCount { get; set; }
    }

    public class Token
    {
        public string Name;
        public string Value;

        public Token(string n, string v)
        {
            this.Name = n;
            this.Value = v;
        }
    }
}