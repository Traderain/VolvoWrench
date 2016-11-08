using System;
using System.Runtime.CompilerServices;

namespace PortalAdjust.Demo
{
	public class DemoParseResult : ICloneable
	{

        public DemoParseResult()
        {
            this.StartAdjustmentTick = -1;
            this.EndAdjustmentTick = -1;
        }

		public int AdjustedTicks
		{
			get
			{
				if (this.StartAdjustmentTick > -1 && this.EndAdjustmentTick > -1)
				{
					return this.EndAdjustmentTick - this.StartAdjustmentTick;
				}
				if (this.StartAdjustmentTick > -1)
				{
					return this.TotalTicks - this.StartAdjustmentTick;
				}
				if (this.EndAdjustmentTick > -1)
				{
					return this.EndAdjustmentTick;
				}
				return this.TotalTicks;
			}
		}

		public int EndAdjustmentTick
		{
			get;
			set;
		}

		public string EndAdjustmentType
		{
			get;
			set;
		}

        public string FileName
        {
            get;
            set;
        }

		public string GameDir
		{
			get;
			set;
		}

		public string MapName
		{
			get;
			set;
		}

		public string PlayerName
		{
			get;
			set;
		}

		public int StartAdjustmentTick
		{
			get;
			set;
		}

		public string StartAdjustmentType
		{
			get;
			set;
		}

		public int TotalTicks
		{
			get;
			set;
		}

        public object Clone()
        {
            DemoParseResult demoParseResult = new DemoParseResult()
            {
                FileName = this.FileName,
                MapName = this.MapName,
                PlayerName = this.PlayerName,
                GameDir = this.GameDir,
                TotalTicks = this.TotalTicks,
                StartAdjustmentTick = this.StartAdjustmentTick,
                StartAdjustmentType = this.StartAdjustmentType,
                EndAdjustmentTick = this.EndAdjustmentTick,
                EndAdjustmentType = this.EndAdjustmentType
            };
            return demoParseResult;
        }
	}
}