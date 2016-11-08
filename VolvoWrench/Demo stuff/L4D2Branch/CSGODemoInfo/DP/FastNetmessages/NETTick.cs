using System;
using System.IO;
using VolvoWrench.Demo_stuff.L4D2Branch.BitStreamUtil;

namespace VolvoWrench.Demo_stuff.L4D2Branch.CSGODemoInfo.DP.FastNetmessages
{
	public struct NETTick
	{
		public UInt32 Tick;
		public UInt32 HostComputationTime;
		public UInt32 HostComputationTimeStdDeviation;
		public UInt32 HostFramestartTimeStdDeviation;

		public void Parse(IBitStream bitstream, DemoParser parser)
		{
			while (!bitstream.ChunkFinished) {
				var desc = bitstream.ReadProtobufVarInt();
				var wireType = desc & 7;
				var fieldnum = desc >> 3;
				if (wireType != 0)
					throw new InvalidDataException();

				var val = (uint)bitstream.ReadProtobufVarInt();

				switch (fieldnum) {
				case 1:
					Tick = val;
					break;
				case 4:
					HostComputationTime = val;
					break;
				case 5:
					HostComputationTimeStdDeviation = val;
					break;
				case 6:
					HostFramestartTimeStdDeviation = val;
					break;
				default:
					// silently drop
					break;
				}
			}
		}
	}
}

