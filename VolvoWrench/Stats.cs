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

namespace VolvoWrench
{
    public partial class Statisctics : Form
    {
        public List<string> cmds = new List<string>()
        {
            "+jump",
            "+attack"
        }; 
        public Statisctics()
        {
            Random r = new Random();
            InitializeComponent();
            dataGridView1.DataSource = cmds.Select(x => new {Command = x, Count = 1}).ToArray();
            dataGridView1.AutoGenerateColumns = true;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            chart1.Series.Clear();
            chart1.Series.Add(new Series("ConsoleCMDs"));
            cmds.ForEach(x => chart1.Series.First().Points.Add(r.Next(1000)).Label = x); //TODO: Add acctuall value
            chart1.Series.First().LabelForeColor = Color.White;
        }
    }
}
