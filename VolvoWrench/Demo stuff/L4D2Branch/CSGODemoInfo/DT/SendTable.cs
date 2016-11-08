using System.Collections.Generic;
using VolvoWrench.Demo_stuff.L4D2Branch.BitStreamUtil;

namespace VolvoWrench.Demo_stuff.L4D2Branch.CSGODemoInfo.DT
{
	class SendTable
	{
        List<SendTableProperty> properties = new List<SendTableProperty>();
        public List<SendTableProperty> Properties
        {
            get { return properties; }
        }

		public string Name { get; set; }
		public bool IsEnd { get; set; }

		public SendTable(IBitStream bitstream) {
			DP.FastNetmessages.SendTable dataTable = new DP.FastNetmessages.SendTable();

			foreach (var prop in dataTable.Parse(bitstream)) {
				SendTableProperty property = new SendTableProperty () {
					DataTableName = prop.DtName,
					HighValue = prop.HighValue,
					LowValue = prop.LowValue,
					Name = prop.VarName,
					NumberOfBits = prop.NumBits,
					NumberOfElements = prop.NumElements,
					Priority = prop.Priority,
					RawFlags = prop.Flags,
					RawType = prop.Type
				};

				properties.Add (property);
			}

			this.Name = dataTable.NetTableName;
			this.IsEnd = dataTable.IsEnd;
		}
	}
}

