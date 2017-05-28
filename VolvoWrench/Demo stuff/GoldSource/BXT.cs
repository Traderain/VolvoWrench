using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    class BXT
    {
        public enum RuntimeDataType
        {
            CVAR_VALUES = 1,
			TIME,
			BOUND_COMMAND,
			ALIAS_EXPANSION,
			SCRIPT_EXECUTION,
		}

        struct Time
        {
            UInt32 hours;
            byte minutes;
            byte seconds;
            double remainder;
        }

        // Map from CVar name to value.
        List<KeyValuePair<string, string>> CVarValues;

	    struct BoundCommand
        {
            string key;
            string command;
        }

        struct AliasExpansion
        {
            string name;
            string command;
        }

        struct ScriptExecution
        {
            string filename;
            string contents;
        }
    }
}
