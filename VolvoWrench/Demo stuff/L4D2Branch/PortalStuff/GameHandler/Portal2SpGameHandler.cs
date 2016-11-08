using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using VolvoWrench.Demo_stuff;

namespace PortalAdjust.Demo
{
	internal class Portal2SpGameHandler : OrangeBoxGameHandler
	{
        private const string _crosshairAppearAdjustType = "Crosshair Appear";

        private const string _crosshairDisappearAdjustType = "Crosshair Disappear";

		private string _startAdjustType;

		private string _endAdjustType;

        private string[] _maps = Category.Portal2Sp.Maps;

		private int _startTick = -1;

		private int _endTick = -1;

		private StringBuilder _debugBuffer;

        private Point3D INTRO_START_POS = new Point3D(-8709.20f, 1690.07f, 28.00f);
        private Point3D INTRO_START_TOL = new Point3D(0.02f, 0.02f, 0.5f);
        private int INTRO_START_TICK_OFFSET = 1;

        // best guess. you can move at ~2-3 units/tick, so don't check exactly.
        private Point3D FINALE_END_POS = new Point3D(54.1f, 159.2f, -201.4f);

        // how many ticks from last portal shot to being at the checkpoint.
        // experimentally determined, may be wrong.
        private int FINALE_END_TICK_OFFSET = -852;

        private bool onTheMoon(Point3D position) {
            // check if you're in a specific cylinder of volume and far enough below the floor.
            return Math.Pow(position.X - FINALE_END_POS.X, 2) + Math.Pow(position.Y - FINALE_END_POS.Y, 2) < Math.Pow(50, 2)
                && (position.Z < FINALE_END_POS.Z);
        }

        private bool atSpawn(Point3D position) {
            // check if at the spawn coordinate for sp_a1_intro1
            return !(Math.Abs(position.X - INTRO_START_POS.X) > INTRO_START_TOL.X)
                && !(Math.Abs(position.Y - INTRO_START_POS.Y) > INTRO_START_TOL.Y)
                && !(Math.Abs(position.Z - INTRO_START_POS.Z) > INTRO_START_TOL.Z);
        }

		public Portal2SpGameHandler()
		{
			base.Maps.AddRange(this._maps);
			this._debugBuffer = new StringBuilder();
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

			StringBuilder stringBuilder = this._debugBuffer;
			object[] currentTick = new object[] { base.CurrentTick, ": ", consoleCmdResult.Command, Environment.NewLine };
			stringBuilder.Append(string.Concat(currentTick));
			return consoleCmdResult;
		}

		protected override PacketResult ProcessPacket(BinaryReader br)
		{
			PacketResult packetResult = base.ProcessPacket(br);
			StringBuilder stringBuilder = this._debugBuffer;
			object[] currentTick = new object[] { base.CurrentTick, ": ", packetResult.CurrentPosition, Environment.NewLine };
			stringBuilder.Append(string.Concat(currentTick));
			if (this._startAdjustType == null && base.Map == "sp_a1_intro1" && atSpawn(packetResult.CurrentPosition))
			{
                this._startAdjustType = "Crosshair Appear";
                this._startTick = base.CurrentTick + INTRO_START_TICK_OFFSET;
			}
			else if (this._endAdjustType == null && base.Map == "sp_a4_finale4" && onTheMoon(packetResult.CurrentPosition))
			{
                this._endAdjustType = "Crosshair Disappear";
                this._endTick = base.CurrentTick + FINALE_END_TICK_OFFSET;
			}
			return packetResult;
		}
	}
}