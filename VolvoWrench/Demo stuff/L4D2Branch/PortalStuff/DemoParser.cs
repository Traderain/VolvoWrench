using System;
using System.IO;
using System.Text;

namespace PortalAdjust.Demo
{
	public class DemoParser
	{
		public static DemoParseResult ParseDemo(string file)
		{
			byte num;
			DemoParseResult result;
			using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (Encoding.ASCII.GetString(binaryReader.ReadBytes(8)).TrimEnd(new char[1]) != "HL2DEMO")
					{
						throw new Exception("Not a demo");
					}
					DemoProtocolVersion demoProtocolVersion = (DemoProtocolVersion)binaryReader.ReadInt32();
					if ((int)demoProtocolVersion == 2)
					{
						demoProtocolVersion = DemoProtocolVersion.HL2;
					}
					if (!Enum.IsDefined(typeof(DemoProtocolVersion), demoProtocolVersion))
					{
						throw new Exception(string.Concat("Unknown demo protocol version: 0x", demoProtocolVersion.ToString("x")));
					}
					int num1 = binaryReader.ReadInt32();
					binaryReader.BaseStream.Seek((long)260, SeekOrigin.Current);
					string str = Encoding.ASCII.GetString(binaryReader.ReadBytes(260)).TrimEnd(new char[1]);
					string str1 = Encoding.ASCII.GetString(binaryReader.ReadBytes(260)).TrimEnd(new char[1]);
					string str2 = Encoding.ASCII.GetString(binaryReader.ReadBytes(260)).TrimEnd(new char[1]);
					binaryReader.BaseStream.Seek((long)4, SeekOrigin.Current);
					binaryReader.BaseStream.Seek((long)4, SeekOrigin.Current);
					binaryReader.BaseStream.Seek((long)4, SeekOrigin.Current);
					int num2 = binaryReader.ReadInt32();
					GameHandler hL2GameHandler = null;
					try
					{
						hL2GameHandler = GameHandler.getGameHandler(str2, str1);
					}
					catch (Exception)
					{
						if (demoProtocolVersion == DemoProtocolVersion.HL2)
						{
							hL2GameHandler = new HL2GameHandler();
						}
						else if (demoProtocolVersion == DemoProtocolVersion.ORANGEBOX)
						{
							hL2GameHandler = new OrangeBoxGameHandler();
						}
					}

                    hL2GameHandler.FileName = file;
					hL2GameHandler.Map = str1;
					hL2GameHandler.GameDir = str2;
					hL2GameHandler.PlayerName = str;
					hL2GameHandler.SignOnLen = num2;
					hL2GameHandler.NetworkProtocol = num1;

                    do
					{
						num = binaryReader.ReadByte();
						if (hL2GameHandler.IsStop(num))
						{
							break;
						}
						int num3 = binaryReader.ReadInt32();
						if (hL2GameHandler.DemoVersion >= DemoProtocolVersion.ORANGEBOX)
						{
							binaryReader.ReadByte();
						}
						hL2GameHandler.HandleCommand(num, num3, binaryReader);
					}
					while (!hL2GameHandler.IsStop(num));
					result = hL2GameHandler.GetResult();
				}
			}
			return result;
		}
	}
}