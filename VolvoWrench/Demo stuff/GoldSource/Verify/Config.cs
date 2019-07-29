using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace VolvoWrench.Demo_stuff.GoldSource.Verify
{
    class Config
    {
        public string bxt_version = "";

        public List<Category> categories;

        public Config(string file)
        {
            categories = new List<Category>();
            JObject jsonfile = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(file));
            jsonfile["bxt_version"] = bxt_version;
            var cats = (JArray)jsonfile["categories"];
            foreach(var category in cats)
            {
                Category c = new Category();
                c.name = category["name"].ToString();
                categories.Add(c);
            }
        }
    }
}
