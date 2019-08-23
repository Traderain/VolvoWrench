using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolvoWrench.Demo_stuff.GoldSource.Verify
{
    class Category
    {
        public string name;
        public List<Tuple<String,Commandtype>> CommandRules;
        public List<Tuple<String,String>> CvarRules;

        public Category()
        {
            CommandRules = new List<Tuple<string, Commandtype>>();
            CvarRules = new List<Tuple<string, String>>();
        }
    }
}
