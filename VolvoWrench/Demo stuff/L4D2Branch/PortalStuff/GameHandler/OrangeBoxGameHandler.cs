using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace PortalAdjust.Demo
{
	internal class OrangeBoxGameHandler : GameHandler
	{
		public override DemoProtocolVersion DemoVersion
		{
			get;
			protected set;
		}

		public OrangeBoxGameHandler()
		{
			this.DemoVersion = DemoProtocolVersion.ORANGEBOX;
		}

		public override DemoParseResult GetResult()
		{
			DemoParseResult demoParseResult = new DemoParseResult()
			{
				FileName = base.FileName,
                MapName = base.Map,
				PlayerName = base.PlayerName,
				GameDir = base.GameDir,
				TotalTicks = base.CurrentTick,
				StartAdjustmentType = base.MapStartAdjustType,
				EndAdjustmentType = base.MapEndAdjustType
			};
			return demoParseResult;
		}

		public override long HandleCommand(byte command, int tick, BinaryReader br)
		{
			if (tick < 0 || base.CurrentTick == 0 && tick > 66)
			{
				tick = 0;
			}
			if (tick > base.CurrentTick)
			{
				base.CurrentTick = tick;
			}
			Enum.IsDefined(typeof(OrangeBoxGameHandler.OrangeBoxDemoCommands), (OrangeBoxGameHandler.OrangeBoxDemoCommands)command);
			if (command == 1)
			{
				return (long)base.ProcessSignOn(br);
			}
			if (command == 2)
			{
				return this.ProcessPacket(br).Read;
			}
			if (command == 3)
			{
				return (long)0;
			}
			if (command == 4)
			{
				return this.ProcessConsoleCmd(br).Read;
			}
			if (command == 5)
			{
				return this.ProcessUserCmd(br);
			}
			if (command == 6)
			{
				throw new NotImplementedException();
			}
			if (command == 9)
			{
				return this.ProcessStringTables(br);
			}
			if (command != 8)
			{
				throw new Exception(string.Concat("Unknown command: 0x", command.ToString("x")));
			}
			return this.ProcessCustomData(br);
		}

		public override bool IsStop(byte command)
		{
			if (command == 7)
			{
				return true;
			}
			return false;
		}

		protected override ConsoleCmdResult ProcessConsoleCmd(BinaryReader br)
		{
			long position = br.BaseStream.Position;
			int num = br.ReadInt32();
			string str = Encoding.ASCII.GetString(br.ReadBytes(num)).TrimEnd(new char[1]);
			ConsoleCmdResult consoleCmdResult = new ConsoleCmdResult()
			{
				Read = br.BaseStream.Position - position,
				Command = str
			};
			return consoleCmdResult;
		}

		protected override long ProcessCustomData(BinaryReader br)
		{
			long position = br.BaseStream.Position;
			br.ReadInt32();
			int num = br.ReadInt32();
			br.BaseStream.Seek((long)num, SeekOrigin.Current);
			return br.BaseStream.Position - position;
		}

		protected override PacketResult ProcessPacket(BinaryReader br)
		{
			int num;
			long position = br.BaseStream.Position;
			br.BaseStream.Seek((long)4, SeekOrigin.Current);
			float single = br.ReadSingle();
			float single1 = br.ReadSingle();
			float single2 = br.ReadSingle();
			if (base.NetworkProtocol != 2001)
			{
				if (base.NetworkProtocol != 7108 && base.NetworkProtocol != 1028)
				{
					throw new Exception("unknown protocol");
				}
				num = 296;
			}
			else
			{
				num = 144;
			}
			br.BaseStream.Seek((long)num, SeekOrigin.Current);
			int num1 = br.ReadInt32();
			br.BaseStream.Seek((long)num1, SeekOrigin.Current);
			PacketResult packetResult = new PacketResult()
			{
				Read = br.BaseStream.Position - position,
				CurrentPosition = new Point3D(single, single1, single2)
			};
			return packetResult;
		}

		protected override long ProcessStringTables(BinaryReader br)
		{
			long position = br.BaseStream.Position;
			int num = br.ReadInt32();
			br.BaseStream.Seek((long)num, SeekOrigin.Current);
			return br.BaseStream.Position - position;
		}

		protected override long ProcessUserCmd(BinaryReader br)
		{
			long position = br.BaseStream.Position;
			br.BaseStream.Seek((long)4, SeekOrigin.Current);
			int num = br.ReadInt32();
			br.BaseStream.Seek((long)num, SeekOrigin.Current);
			return br.BaseStream.Position - position;
		}

		protected enum OrangeBoxDemoCommands
		{
			SignOn = 1,
			Packet = 2,
			SyncTick = 3,
			ConsoleCmd = 4,
			UserCmd = 5,
			DataTables = 6,
			Stop = 7,
			CustomData = 8,
			StringTables = 9
		}
	}
}