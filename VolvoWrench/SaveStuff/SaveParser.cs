using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VolvoWrench.ExtensionMethods;

namespace VolvoWrench.SaveStuff
{
    public class Flag
    {
        public Flag(int t, float s, string type)
        {
            Ticks = t.ToString();
            Time = s.ToString(CultureInfo.InvariantCulture) + "s";
            Type = type;
        }

        public string Ticks { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
    }

    public class Listsave
    {
        [Serializable]
        public enum Hlfile
        {
            Hl1,
            Hl2,
            Hl3
        }

        public static string Chaptername(int chapter)
        {
            #region MapSwitch

            switch (chapter)
            {
                case 0:
                    return "Point Insertion";
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

        public static SaveFile ParseSaveFile(string file)
        {
            var result = new SaveFile();
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                result.FileName = Path.GetFileName(file);
                result.Files = new List<StateFileInfo>();
                result.Header = (Encoding.ASCII.GetString(br.ReadBytes(sizeof (int))));
                result.SaveVersion = br.ReadInt32();
                result.TokenTableFileTableOffset = br.ReadInt32();
                result.TokenCount = br.ReadInt32();
                result.TokenTableSize = br.ReadInt32();
                br.BaseStream.Seek(result.TokenTableSize + result.TokenTableFileTableOffset, SeekOrigin.Current);
                var endoffile = false;
                var check = br.ReadBytes(4);
                br.BaseStream.Seek(-4, SeekOrigin.Current);
                if (check.Any(b => b == 0))
                {
                    var filenum = br.ReadInt32();
                }
                while (!endoffile && result.SaveVersion <= 116)
                {
                    if (UnexpectedEof(br, 260))
                    {
                        var tempvalv = new StateFileInfo
                        {
                            Data = new byte[0],
                            FileName = Encoding.ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0').Replace("\0", "")
                            //BUG: bunch of \0 in string
                        };
                        tempvalv.StateType = (Hlfile) tempvalv.FileName[tempvalv.Length];
                        if (UnexpectedEof(br, 8))
                        {
                            var filelength = br.ReadInt32();
                            tempvalv.MagicWord = Encoding.ASCII.GetString(br.ReadBytes(4))
                                .Trim('\0')
                                .Replace("\0", string.Empty);
                            br.BaseStream.Seek(-4, SeekOrigin.Current);
                            if (UnexpectedEof(br, 8) && filelength > 0)
                                tempvalv.Data = br.ReadBytes(filelength);
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
                foreach (var f in result.Files)
                {
                 //  f.StateFile =  ParseStateFile(f);
                }
                return result;
            }
        }

        public static ValvFile ParseStateFile(StateFileInfo stateFile)
        {
            var vf = new ValvFile();
            using (var br = new BinaryReader(new MemoryStream(stateFile.Data)))
            {
                SaveFileSectionsInfo_t si = new SaveFileSectionsInfo_t();
                vf.MagicWord = br.ReadString(4);
                vf.Version = br.ReadInt32();
                si.nBytesSymbols = br.ReadInt32();
                si.nSymbols = br.ReadInt32();
                si.nBytesDataHeaders = br.ReadInt32();
                si.nBytesData = br.ReadInt32();
                vf.pSymbols = br.ReadBytes(si.nSymbols);
                vf.pDataHeaders = br.ReadBytes(si.nBytesDataHeaders);
                vf.pData = br.ReadBytes(si.nBytesData);
                var pos = br.BaseStream.Position;
                var length = br.BaseStream.Length;
            }
            return new ValvFile();
        }

        public static uint rotr(uint val, int shift)
        {
            var num = val;    /* number to rotate */
            shift &= 0x1f;                  /* modulo 32 -- this will also make
										   negative shifts work */
            while (Convert.ToBoolean(shift--))
            {
                var lobit = num & 1;        /* non-zero means lo bit set */
                num >>= 1;              /* shift right one bit */
                if (Convert.ToBoolean(lobit))
                    num |= 0x80000000;  /* set hi bit if lo bit was set */
            }

            return num;
        }

        public static bool UnexpectedEof(BinaryReader b, int lengthtocheck)
            => b.BaseStream.Position + lengthtocheck < b.BaseStream.Length;

        [Serializable]
        public class SaveFile
        {
            public string FileName { get; set; }
            public string Header { get; set; }
            public int SaveVersion { get; set; }
            public int TokenTableFileTableOffset { get; set; }
            public int TokenTableSize { get; set; }
            public int TokenCount { get; set; }
            public List<StateFileInfo> Files { get; set; }
        }

        [Serializable]
        public class ValvFile
        {
            public string MagicWord;
            public int Version;
            public SaveFileSectionsInfo_t SectionsInfo;
            public byte[] pData;
            public byte[] pDataHeaders;
            public byte[] pSymbols;
        }

        [Serializable]
        public class StateFileInfo
        {
            public string FileName;
            public string MagicWord;
            public byte[] Data;
            public Hlfile StateType;
            public int Length;
            public ValvFile StateFile;
        }

        #region DataDesc

        public const int SAVEGAME_MAPNAME_LEN = 32;
        public const int SAVEGAME_COMMENT_LEN = 80;
        public const int SAVEGAME_ELAPSED_LEN = 32;
        public const int SECTION_MAGIC_NUMBER = 0x54541234;
        public const int SECTION_VERSION_NUMBER = 2;
        public const int MAX_MAP_NAME = 32;

        private unsafe struct GAME_HEADER
        {
            private fixed char comment [80];
            private fixed char landmark [256];

            private int mapCount;
                // the number of map state files in the save file.  This is usually number of maps * 3 (.hl1, .hl2, .hl3 files)

            private fixed char mapName [32];
            private fixed char originMapName [32];
        };

        private unsafe struct SaveGameDescription_t
        {
            private int iSize;
            private int iTimestamp;
            private fixed char szComment [SAVEGAME_COMMENT_LEN];
            private fixed char szElapsedTime [SAVEGAME_ELAPSED_LEN];
            private fixed char szFileName [128];
            private fixed char szFileTime [32];
            private fixed char szMapName [SAVEGAME_MAPNAME_LEN];
            private fixed char szShortName [64];
            private fixed char szType [64];
        };

        private unsafe struct SaveHeader
        {
            private int connectionCount;
            private int lightStyleCount;
            private fixed char mapName [32];
            private int mapVersion;
            private int saveId;
            private int skillLevel;
            private fixed char skyName [32];
            private float time;
            private int version;
        };

        public class SaveFileSectionsInfo_t
        {
            public int nBytesData;
            public int nBytesDataHeaders;
            public int nBytesSymbols;
            public int nSymbols;

            public int SumBytes()
            {
                return (nBytesSymbols + nBytesDataHeaders + nBytesData);
            }
        }

        public class saverestorelevelinfo_t
        {
            public int connectionCount; // Number of elements in the levelList[]
            public levellist_t[] levelList = new levellist_t[16]; // List of connections from this level

            // smooth transition
            public int fUseLandmark;
            public char[] szLandmarkName = new char[20]; // landmark we'll spawn near in next level
            public Vector vecLandmarkOffset; // for landmark transitions
            public float time;
            public char[] szCurrentMapName = new char[MAX_MAP_NAME]; // To check global entities
            public int mapVersion;

        }

        public class levellist_t
        {
            public char[] mapName = new char[MAX_MAP_NAME];
            public char[] landmarkName = new char[MAX_MAP_NAME];
            //edict_t* pentLandmark;
            public Vector vecLandmarkOrigin;
        }

        struct entitytable_t
        {
            void Clear()
            {
                id = -1;
                edictindex = -1;
                saveentityindex = -1;
                restoreentityindex = -1;
                location = 0;
                size = 0;
                flags = 0;
                classname = "";
                globalname = "";
                landmarkModelSpace = new Vector();
                modelname = "";
            }

            int id;             // Ordinal ID of this entity (used for entity <--> pointer conversions)
            int edictindex;     // saved for if the entity requires a certain edict number when restored (players, world)
            int saveentityindex; // the entity index the entity had at save time ( for fixing up client side entities )
            int restoreentityindex; // the entity index given to this entity at restore time
            int location;       // Offset from the base data of this entity
            int size;           // Byte size of this entity's data
            int flags;          // This could be a short -- bit mask of transitions that this entity is in the PVS of
            string classname;     // entity class name
            string globalname;        // entity global name
            Vector landmarkModelSpace;  // a fixed position in model space for comparison
                                        // NOTE: Brush models can be built in different coordiante systems
                                        //		in different levels, so this fixes up local quantities to match
                                        //		those differences.
            string modelname;
        }

        #endregion
    }
}