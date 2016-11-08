using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace PortalAdjust.Demo
{
	public class PacketResult
	{
		public Point3D CurrentPosition
		{
			get;
			set;
		}

		public long Read
		{
			get;
			set;
		}

		public PacketResult()
		{
		}
	}
}