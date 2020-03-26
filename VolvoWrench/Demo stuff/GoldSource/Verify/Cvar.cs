using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolvoWrench.Demo_stuff.GoldSource.Verify
{
    public class Cvar
    {
        enum CVartype
        {
            CONSTANT,
            RANGE,
            LIST
        }

        private string id;

        private double lowerbound;
        private double upperbound;

        private List<string> sequencevalues;

        private string stringconst = "";

        CVartype type;

        public Cvar(JObject obj)
        {
            sequencevalues = new List<string>();
            ParseJsonSetting(obj);
        }

        public string GetCommandID()
        {
            return id;
        }

        public void ParseJsonSetting(JObject obj)
        {
            if (obj == null)
                throw new Exception("Mallformed settings json! (NULL value)");
            else
            {
                string name = obj["cvar"].ToString().Trim();
                string value = obj["value"].ToString().Trim();
                if (value.Contains("from"))
                {
                    //From-to value
                    type = CVartype.RANGE;
                    var vals = new System.Text.RegularExpressions.Regex("[0-9.,]+").Matches(value);

                    if (vals.Count != 2)
                        throw new Exception($"Mallformed json value: {value}");
                    lowerbound = double.Parse(vals[0].Value);
                    upperbound = double.Parse(vals[1].Value);
                }
                else if(value.Contains(";") && value.EndsWith(")") && value.StartsWith("("))
                {
                    //List of values
                    type = CVartype.LIST;
                    sequencevalues.AddRange(value.Substring(1, value.Length-2).Split(';'));
                }
                else
                {
                    type = CVartype.CONSTANT;
                    stringconst = value;
                }
            }
        }

        public bool Verify(string value)
        {
            value = value.Trim();
            switch(type)
            {
                case CVartype.CONSTANT:
                {
                    return value.Trim() == stringconst.Trim();
                }
                case CVartype.LIST:
                {
                   return sequencevalues.Any(x => x.Trim() == value);
                }
                case CVartype.RANGE:
                {
                   var val = double.Parse(value);
                   return val >= lowerbound && val <= upperbound;
                }
            }
            return false;
        }
    }
}
