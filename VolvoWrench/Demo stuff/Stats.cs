using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VolvoWrench.Demo_stuff;

namespace VolvoWrench
{
    public partial class Statisctics : Form
    {
        public class CMDFilter
        {
            public string CMD;
            public int count;

            public CMDFilter(){}

            public CMDFilter(string command, int count)
            {
                
            }
        }
        public List<string> cmds = new List<string>()
        {
            "+jump",
            "+attack"
        }; 
        public Statisctics(SourceDemoInfo di)
        {
            InitializeComponent();
            dataGridView1.DataSource = cmds.Select(x => new {Command = x, Count = di.Messages
                 .Count(y => y.Type == SourceParser.MessageType.ConsoleCmd)
            }).ToArray();
            dataGridView1.AutoGenerateColumns = true;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            chart1.Series.Clear();
            chart1.Series.Add(new Series("ConsoleCMDs"));
            cmds.ForEach(x => chart1.Series
                 .First().Points.Add(di.Messages
                 .Count( y => y.Type == SourceParser.MessageType.ConsoleCmd)*1000)
                 .Label = x);
            chart1.Series.First().LabelForeColor = Color.White;
        }
    }
}
