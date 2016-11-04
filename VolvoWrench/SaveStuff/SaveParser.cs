using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

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
        #region DataDesc
        [Serializable]
        public class SaveFile
        {
            public string FileName { get; set; }
            public string Header { get; set; }
            public int SaveVersion { get; set; }
            public int TokenTableFileTableOffset { get; set; }
            public int TokenTableSize { get; set; }
            public int TokenCount { get; set; }
            public List<ValvFile> Files { get; set; }

        }

        public enum Hlfile
        {
            Hl1,
            Hl2,
            Hl3
        }
        public class ValvFile
        {
            public string MagicWord;
            public int Length;
            public byte[] Data;
            public string FileName;
            public Hlfile StateType;
        }

        public const int SAVEGAME_MAPNAME_LEN = 32;
        public const int SAVEGAME_COMMENT_LEN = 80;
        public const int SAVEGAME_ELAPSED_LEN = 32;
        public const int SECTION_MAGIC_NUMBER = 0x54541234;
        public const int SECTION_VERSION_NUMBER = 2;

        unsafe struct GAME_HEADER
        {

            fixed char mapName[32];
            fixed char comment[80];
            int mapCount;       // the number of map state files in the save file.  This is usually number of maps * 3 (.hl1, .hl2, .hl3 files)
            fixed char originMapName[32];
            fixed char landmark[256];
        };

        unsafe struct SaveGameDescription_t
        {
            fixed char szShortName[64];
            fixed char szFileName[128];
            fixed char szMapName[SAVEGAME_MAPNAME_LEN];
            fixed char szComment[SAVEGAME_COMMENT_LEN];
            fixed char szType[64];
            fixed char szElapsedTime[SAVEGAME_ELAPSED_LEN];
            fixed char szFileTime[32];
            int iTimestamp;
            int iSize;
        };

        unsafe struct SaveHeader
        {
            int saveId;
            int version;
            int skillLevel;
            int connectionCount;
            int lightStyleCount;
            int mapVersion;
            float time;
            fixed char mapName[32];
            fixed char skyName[32];
        };
        #endregion

        public static string Chaptername(int chapter)
        {
            #region MapSwitch
            switch (chapter)
            {
                case 0: return "Point Insertion";
                case 1: return "A Red Letter Day";
                case 2: return "Route Kanal";
                case 3: return "Water Hazard";
                case 4: return "Black Mesa East";
                case 5: return "We don't go to Ravenholm";
                case 6: return "Highway 17";
                case 7: return "Sandtraps";
                case 8: return "Nova Prospekt";
                case 9: return "Entanglement";
                case 10: return "Anticitizen One";
                case 11: return "Follow Freeman!";
                case 12: return "Our Benefactors";
                case 13: return "Dark Energy";
                default: return "Mod/Unknown";
            }
            #endregion
        }
        public static SaveFile ParseFile(string file)
        {
            var result = new SaveFile();
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                result.FileName = Path.GetFileName(file);
                result.Files = new List<ValvFile>();
                result.Header = (Encoding.ASCII.GetString(br.ReadBytes(sizeof (int))));
                result.SaveVersion = br.ReadInt32();
                result.TokenTableFileTableOffset = br.ReadInt32();
                result.TokenCount = br.ReadInt32();
                result.TokenTableSize = br.ReadInt32();
                br.BaseStream.Seek(result.TokenTableSize + result.TokenTableFileTableOffset, SeekOrigin.Current);
                var endoffile = false;
                var check = br.ReadBytes(4);
                br.BaseStream.Seek(-4,SeekOrigin.Current);
                if (!check.Any(b => b == 0))
                {
                    while (!endoffile && result.SaveVersion <= 116)
                    {
                        if (UnexpectedEof(br, 260))
                        {
                            var tempvalv = new ValvFile
                            {
                                Data = new byte[0],
                                FileName = new string(br.ReadChars(260))
                            };
                            if (UnexpectedEof(br, 8))
                            {
                                var filelength = br.ReadInt32();
                                tempvalv.MagicWord = Encoding.ASCII.GetString(br.ReadBytes(4))
                                    .Trim('\0')
                                    .Replace("\0", string.Empty);
                                if (UnexpectedEof(br, 8) && filelength > 0)
                                    tempvalv.Data = br.ReadBytes(filelength - 4);
                                else
                                    endoffile = true;
                            }
                            else
                                endoffile = true;
                            result.Files.Add(tempvalv);
                        }
                        else
                            endoffile = true;
                    }
                    return result;
                }
                else
                {
                    var filenum = br.ReadInt32();
                    while (!endoffile && result.SaveVersion <= 116)
                    {
                        if (UnexpectedEof(br, 260))
                        {
                            var tempvalv = new ValvFile
                            {
                                Data = new byte[0],
                                FileName = Encoding.ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0').Replace("\0","") //BUG: bunch of \0 in string
                            };
                            tempvalv.StateType = (Hlfile) tempvalv.FileName[tempvalv.Length];
                            if (UnexpectedEof(br, 8))
                            {
                                var filelength = br.ReadInt32();
                                tempvalv.MagicWord = Encoding.ASCII.GetString(br.ReadBytes(4))
                                    .Trim('\0')
                                    .Replace("\0", string.Empty);
                                if (UnexpectedEof(br, 8) && filelength > 0)
                                    tempvalv.Data = br.ReadBytes(filelength - 4);
                                else
                                    endoffile = true;
                            }
                            else
                                endoffile = true;
                            result.Files.Add(tempvalv);
                        }
                        else
                            endoffile = true;
                    }
                    return result;
                }
            }
        }

        public static bool UnexpectedEof(BinaryReader b, int lengthtocheck) => b.BaseStream.Position + lengthtocheck < b.BaseStream.Length;
    }

}