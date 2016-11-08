using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Media3D;
using VolvoWrench.Demo_stuff;

namespace PortalAdjust.Demo
{
	internal class PortalGameHandler : HL2GameHandler
	{
		private const string _crosshairAppearAdjustType = "Crosshair Appear";

		private const string _crosshairDisappearAdjustType = "Crosshair Disappear";

		private string _startAdjustType;

		private string _endAdjustType;

		private string[] _maps = Category.Portal.Maps;

		private int _startTick = -1;

		private int _endTick = -1;

		public PortalGameHandler()
		{
			base.Maps.AddRange(this._maps);
		}

		public override DemoParseResult GetResult()
		{
			DemoParseResult result = base.GetResult();
			if (this._startAdjustType != null)
			{
				result.StartAdjustmentType = this._startAdjustType;
				result.StartAdjustmentTick = this._startTick;
			}
			if (this._endAdjustType != null)
			{
				result.EndAdjustmentType = this._endAdjustType;
				result.EndAdjustmentTick = this._endTick;
			}
			return result;
		}

		protected override ConsoleCmdResult ProcessConsoleCmd(BinaryReader br)
		{
			ConsoleCmdResult consoleCmdResult = base.ProcessConsoleCmd(br);

			if (this._endAdjustType == null && base.Map == "escape_02" && consoleCmdResult.Command == "startneurotoxins 99999")
			{
				this._endAdjustType = "Crosshair Disappear";
				this._endTick = base.CurrentTick + 1;
			}
			return consoleCmdResult;
		}

		protected override PacketResult ProcessPacket(BinaryReader br)
		{
			PacketResult packetResult = base.ProcessPacket(br);

			if (this._startAdjustType == null && base.Map == "testchmb_a_00" && packetResult.CurrentPosition.Equals(new Point3D(-544f, -368.75f, 160f)))
			{
				this._startAdjustType = "Crosshair Appear";
				this._startTick = base.CurrentTick + 1;
			}
			return packetResult;
		}
	}
}