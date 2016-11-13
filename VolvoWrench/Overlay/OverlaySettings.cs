using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace VolvoWrench.Overlay
{
    public partial class OverlaySettings : Form
    {
        public OverlaySettings()
        {
            InitializeComponent();
            var parser = new FileIniDataParser();
            var cvt = new FontConverter();
            IniData iniD = parser.ReadFile(Main.SettingsPath);
            F = cvt.ConvertFromString(iniD["SETTINGS"]["overlay_font"]) as Font;
            var colorstring = iniD["SETTINGS"]["overlay_color"].Split(':');
            C = Color.FromArgb(
                Convert.ToInt32(colorstring[0]),
                Convert.ToInt32(colorstring[1]),
                Convert.ToInt32(colorstring[2]),
                Convert.ToInt32(colorstring[3]));
            var tf = new Font(FontFamily.GenericMonospace, 14, FontStyle.Regular, GraphicsUnit.Point);
            if (F == null)
                F = tf; 
            label1.Font = F;
            button2.BackColor = C;
        }

        public Font F;
        public Color C;

        private void OverlaySettings_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Color pick
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = cd.Color;
                C = cd.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                label1.Font = fd.Font;
                F = fd.Font;
            }
        }

        private void button3_Click(object sender, EventArgs e) //OK
        {
            var parser = new FileIniDataParser();
            var cvt = new FontConverter();
            IniData iniD = parser.ReadFile(Main.SettingsPath);
            iniD["SETTINGS"]["overlay_font"] = cvt.ConvertToString(F);
            iniD["SETTINGS"]["overlay_color"] = C.A + ":" + C.R + ":" + C.B + ":" + C.G;
            parser.WriteFile(Main.SettingsPath, iniD);
            var parentT = (Main)Owner;
            parentT.UpdateOverLaySettings(F,C);
            Close();
        }

        private void button4_Click(object sender, EventArgs e) //CANCEL
        {
            Close();
        }
    }
}
