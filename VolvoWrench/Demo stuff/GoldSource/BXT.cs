using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace VolvoWrench.Demo_stuff.GoldSource
{
	/// <summary>
	/// Types from BunnymodXT's runtime serialization.
	/// </summary>
	public static class Bxt
	{
		public enum RuntimeDataType
		{
			VERSION_INFO = 1,
			CVAR_VALUES,
			TIME,
			BOUND_COMMAND,
			ALIAS_EXPANSION,
			SCRIPT_EXECUTION,
			COMMAND_EXECUTION,
			GAME_END_MARKER,
			LOADED_MODULES,
			CUSTOM_TRIGGER_COMMAND,
		}

		[Serializable]
		public class VersionInfo : BXTData
		{
			public int build_number;
			public string bxt_version;


			public override void Read(BinaryReader br)
			{
				build_number = br.ReadInt32();
				bxt_version = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		[Serializable]
		public class Time : BXTData
		{
			public UInt32 hours;
			public byte minutes;
			public byte seconds;
			public double remainder;

			public override string ToString()
			{
				return hours + ":" + minutes + ":" + (seconds + remainder).ToString("F1");
			}


			public override void Read(BinaryReader br)
			{
				hours = br.ReadUInt32();
				minutes = br.ReadByte();
				seconds = br.ReadByte();
				remainder = br.ReadDouble();
			}
		}

		[Serializable]
		public class BoundCommand : BXTData
		{
			public string command;

			public override void Read(BinaryReader br)
			{
				command = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		[Serializable]
		public class AliasExpansion : BXTData
		{
			public string name;
			public string command;

			public override void Read(BinaryReader br)
			{
				name = new string(br.ReadChars(br.ReadInt32()));
				command = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		public class ScriptExecution : BXTData
		{
			public string filename;
			public string contents;

			public override void Read(BinaryReader br)
			{
				filename = new string(br.ReadChars(br.ReadInt32()));
				contents = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		[Serializable]
		public class CommandExecution : BXTData
		{
			public string command;

			public override void Read(BinaryReader br)
			{
				command = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		[Serializable]
		public class LoadedModules : BXTData
		{
			public List<string> filenames;

			public override void Read(BinaryReader br)
			{
				filenames = new List<string>();
				var count = br.ReadUInt32();
				for (int  i= 0; i < count; i++)
				{
					filenames.Add(new string(br.ReadChars(br.ReadInt32())));
				}
			}
		}

		[Serializable]
		public class CustomTriggerCommand : BXTData
		{
			public Point3D corner_min;
			public Point3D corner_max;
			public string command;

			public override void Read(BinaryReader br)
			{
				corner_min = new Point3D(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
				corner_max = new Point3D(br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
				command = new string(br.ReadChars(br.ReadInt32()));
			}
		}

		public class GameEndMarker : BXTData
		{
			public override void Read(BinaryReader br) { }
		}

		[Serializable]
		public class CVarValues : BXTData
		{
			public List<KeyValuePair<string, string>> CVars;


			public override void Read(BinaryReader br)
			{
				CVars = new List<KeyValuePair<string, string>>();
				var cvarnum = br.ReadUInt32();
				for (var i = 0; i < cvarnum; i++)
				{
					var fsl = br.ReadInt32();
					var fs = new string(br.ReadChars(fsl));
					var ssl = br.ReadInt32();
					var ss = new string(br.ReadChars(ssl));
					CVars.Add(new KeyValuePair<string, string>(fs,ss));
				}
			}
		}

		public abstract class BXTData
		{
			/// <summary>
			/// Read the data.
			/// </summary>
			public abstract void Read(BinaryReader br);
		}
	}
}
