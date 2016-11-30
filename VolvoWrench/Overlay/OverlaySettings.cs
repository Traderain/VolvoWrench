using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;

namespace VolvoWrench.Overlay
{
    public partial class OverlaySettings : Form
    {
        public Color C;
        public Font F;

        public OverlaySettings()
        {
            InitializeComponent();
            var parser = new FileIniDataParser();
            var cvt = new FontConverter();
            try
            {
                var iniD = parser.ReadFile(Main.SettingsPath);
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
                iniD["SETTINGS"]["overlay_font"] = cvt.ConvertToString(F);
                iniD["SETTINGS"]["overlay_color"] = C.A + ":" + C.R + ":" + C.B + ":" + C.G;
                //(iniD\[\"\w*\"\]\[\"\w*\"\])  = (checkBox\d\d.Checked) //TODO: Learn regex kek

                //SOURCE OVERLAY_SOURCE
                checkBox1.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["demo_protocol"]));
                checkBox2.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["net_protocol"]));
                checkBox3.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["server_name"]));
                checkBox5.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["client_name"]));
                checkBox6.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["map_name"]));
                checkBox7.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["game_directory"]));
                checkBox8.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_time"]));
                checkBox9.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_ticks"]));
                checkBox10.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["save_flag"]));
                checkBox11.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["autosave_flag"]));
                //HLSOOE OVERLAY_HLSOOE
                checkBox22.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["demo_protocol"]));
                checkBox21.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["net_protocol"]));
                checkBox20.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["server_name"]));
                checkBox18.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["client_name"]));
                checkBox17.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["map_name"]));
                checkBox16.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["game_directory"]));
                checkBox15.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_time"]));
                checkBox14.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_ticks"]));
                checkBox13.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["save_flag"]));
                checkBox12.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["autosave_flag"]));
                //L4D2 Branch OVERLAY_L4D2BRANCH
                checkBox33.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["demo_protocol"]));
                checkBox32.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["net_protocol"]));
                checkBox31.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["server_name"]));
                checkBox29.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["client_name"]));
                checkBox28.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["map_name"]));
                checkBox27.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["game_directory"]));
                checkBox26.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_time"]));
                checkBox25.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_ticks"]));
                checkBox24.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["save_flag"]));
                checkBox23.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["autosave_flag"]));
                checkBox34.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_time"]));
                checkBox35.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_ticks"]));
                //GOLDSOURCE OVERLAY_GOLDSOURCE
                checkBox44.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["demo_protocol"]));
                checkBox43.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["net_protocol"]));
                checkBox42.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["server_name"]));
                checkBox40.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["client_name"]));
                checkBox39.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["map_name"]));
                checkBox38.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["game_directory"]));
                checkBox37.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_time"]));
                checkBox36.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_ticks"]));
                checkBox48.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_fps"]));
                checkBox47.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_fps"]));
                checkBox4.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_fps"]));
                checkBox46.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_msec"]));
                checkBox45.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_msec"]));
                checkBox49.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_msec"]));
                parser.WriteFile(Main.SettingsPath, iniD);
            }
            catch (Exception e)
            {
                MessageBox.Show(@"The settings file has been reset to avoid a crash due to: " + e.Message);
                Main.SettingsManager(true);
            }
            finally
            {
                var iniD = parser.ReadFile(Main.SettingsPath);
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
                iniD["SETTINGS"]["overlay_font"] = cvt.ConvertToString(F);
                iniD["SETTINGS"]["overlay_color"] = C.A + ":" + C.R + ":" + C.B + ":" + C.G;
                //(iniD\[\"\w*\"\]\[\"\w*\"\])  = (checkBox\d\d.Checked) //TODO: Learn regex kek

                //SOURCE OVERLAY_SOURCE
                checkBox1.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["demo_protocol"]));
                checkBox2.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["net_protocol"]));
                checkBox3.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["server_name"]));
                checkBox5.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["client_name"]));
                checkBox6.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["map_name"]));
                checkBox7.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["game_directory"]));
                checkBox8.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_time"]));
                checkBox9.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_ticks"]));
                checkBox10.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["save_flag"]));
                checkBox11.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["autosave_flag"]));
                //HLSOOE OVERLAY_HLSOOE
                checkBox22.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["demo_protocol"]));
                checkBox21.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["net_protocol"]));
                checkBox20.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["server_name"]));
                checkBox18.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["client_name"]));
                checkBox17.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["map_name"]));
                checkBox16.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["game_directory"]));
                checkBox15.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_time"]));
                checkBox14.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_ticks"]));
                checkBox13.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["save_flag"]));
                checkBox12.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["autosave_flag"]));
                //L4D2 Branch OVERLAY_L4D2BRANCH
                checkBox33.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["demo_protocol"]));
                checkBox32.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["net_protocol"]));
                checkBox31.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["server_name"]));
                checkBox29.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["client_name"]));
                checkBox28.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["map_name"]));
                checkBox27.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["game_directory"]));
                checkBox26.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_time"]));
                checkBox25.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_ticks"]));
                checkBox24.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["save_flag"]));
                checkBox23.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["autosave_flag"]));
                checkBox34.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_time"]));
                checkBox35.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_ticks"]));
                //GOLDSOURCE OVERLAY_GOLDSOURCE
                checkBox44.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["demo_protocol"]));
                checkBox43.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["net_protocol"]));
                checkBox42.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["server_name"]));
                checkBox40.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["client_name"]));
                checkBox39.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["map_name"]));
                checkBox38.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["game_directory"]));
                checkBox37.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_time"]));
                checkBox36.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_ticks"]));
                checkBox48.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_fps"]));
                checkBox47.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_fps"]));
                checkBox4.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_fps"]));
                checkBox46.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_msec"]));
                checkBox45.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_msec"]));
                checkBox49.Checked = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_msec"]));
                parser.WriteFile(Main.SettingsPath, iniD);
            }
        }

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
            try
            {
                var parser = new FileIniDataParser();
                var cvt = new FontConverter();
                var iniD = parser.ReadFile(Main.SettingsPath);
                iniD["SETTINGS"]["overlay_font"] = cvt.ConvertToString(F);
                iniD["SETTINGS"]["overlay_color"] = C.A + ":" + C.R + ":" + C.B + ":" + C.G;
                //SOURCE OVERLAY_SOURCE
                iniD["OVERLAY_SOURCE"]["demo_protocol"] = Convert.ToInt32(checkBox1.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["net_protocol"] = Convert.ToInt32(checkBox2.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["server_name"] = Convert.ToInt32(checkBox3.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["client_name"] = Convert.ToInt32(checkBox5.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["map_name"] = Convert.ToInt32(checkBox6.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["game_directory"] = Convert.ToInt32(checkBox7.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["measured_time"] = Convert.ToInt32(checkBox8.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["measured_ticks"] = Convert.ToInt32(checkBox9.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["save_flag"] = Convert.ToInt32(checkBox10.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["autosave_flag"] = Convert.ToInt32(checkBox11.Checked).ToString();
                //HLSOOE OVERLAY_HLSOOE
                iniD["OVERLAY_HLSOOE"]["demo_protocol"] = Convert.ToInt32(checkBox22.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["net_protocol"] = Convert.ToInt32(checkBox21.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["server_name"] = Convert.ToInt32(checkBox20.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["client_name"] = Convert.ToInt32(checkBox18.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["map_name"] = Convert.ToInt32(checkBox17.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["game_directory"] = Convert.ToInt32(checkBox16.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["measured_time"] = Convert.ToInt32(checkBox15.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["measured_ticks"] = Convert.ToInt32(checkBox14.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["save_flag"] = Convert.ToInt32(checkBox13.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["autosave_flag"] = Convert.ToInt32(checkBox12.Checked).ToString();
                //L4D2 Branch OVERLAY_L4D2BRANCH
                iniD["OVERLAY_L4D2BRANCH"]["demo_protocol"] = Convert.ToInt32(checkBox33.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["net_protocol"] = Convert.ToInt32(checkBox32.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["server_name"] = Convert.ToInt32(checkBox31.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["client_name"] = Convert.ToInt32(checkBox29.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["map_name"] = Convert.ToInt32(checkBox28.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["game_directory"] = Convert.ToInt32(checkBox27.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["measured_time"] = Convert.ToInt32(checkBox26.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["measured_ticks"] = Convert.ToInt32(checkBox25.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["save_flag"] = Convert.ToInt32(checkBox24.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["autosave_flag"] = Convert.ToInt32(checkBox23.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["adjusted_time"] = Convert.ToInt32(checkBox34.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["adjusted_ticks"] = Convert.ToInt32(checkBox35.Checked).ToString();
                //GOLDSOURCE OVERLAY_GOLDSOURCE
                iniD["OVERLAY_GOLDSOURCE"]["demo_protocol"] = Convert.ToInt32(checkBox44.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["net_protocol"] = Convert.ToInt32(checkBox43.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["server_name"] = Convert.ToInt32(checkBox42.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["client_name"] = Convert.ToInt32(checkBox40.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["map_name"] = Convert.ToInt32(checkBox39.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["game_directory"] = Convert.ToInt32(checkBox38.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["measured_time"] = Convert.ToInt32(checkBox37.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["measured_ticks"] = Convert.ToInt32(checkBox36.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["highest_fps"] = Convert.ToInt32(checkBox48.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["lowest_fps"] = Convert.ToInt32(checkBox47.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["average_fps"] = Convert.ToInt32(checkBox4.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["lowest_msec"] = Convert.ToInt32(checkBox46.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["highest_msec"] = Convert.ToInt32(checkBox45.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["average_msec"] = Convert.ToInt32(checkBox49.Checked).ToString();
                parser.WriteFile(Main.SettingsPath, iniD);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Missconfigured settings file! Resetting it to avoid a crash!");
                Main.SettingsManager(true);
            }
            finally
            {
                var parser = new FileIniDataParser();
                var cvt = new FontConverter();
                var iniD = parser.ReadFile(Main.SettingsPath);
                iniD["SETTINGS"]["overlay_font"] = cvt.ConvertToString(F);
                iniD["SETTINGS"]["overlay_color"] = C.A + ":" + C.R + ":" + C.B + ":" + C.G;
                //SOURCE OVERLAY_SOURCE
                iniD["OVERLAY_SOURCE"]["demo_protocol"] = Convert.ToInt32(checkBox1.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["net_protocol"] = Convert.ToInt32(checkBox2.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["server_name"] = Convert.ToInt32(checkBox3.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["client_name"] = Convert.ToInt32(checkBox5.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["map_name"] = Convert.ToInt32(checkBox6.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["game_directory"] = Convert.ToInt32(checkBox7.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["measured_time"] = Convert.ToInt32(checkBox8.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["measured_ticks"] = Convert.ToInt32(checkBox9.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["save_flag"] = Convert.ToInt32(checkBox10.Checked).ToString();
                iniD["OVERLAY_SOURCE"]["autosave_flag"] = Convert.ToInt32(checkBox11.Checked).ToString();
                //HLSOOE OVERLAY_HLSOOE
                iniD["OVERLAY_HLSOOE"]["demo_protocol"] = Convert.ToInt32(checkBox22.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["net_protocol"] = Convert.ToInt32(checkBox21.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["server_name"] = Convert.ToInt32(checkBox20.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["client_name"] = Convert.ToInt32(checkBox18.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["map_name"] = Convert.ToInt32(checkBox17.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["game_directory"] = Convert.ToInt32(checkBox16.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["measured_time"] = Convert.ToInt32(checkBox15.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["measured_ticks"] = Convert.ToInt32(checkBox14.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["save_flag"] = Convert.ToInt32(checkBox13.Checked).ToString();
                iniD["OVERLAY_HLSOOE"]["autosave_flag"] = Convert.ToInt32(checkBox12.Checked).ToString();
                //L4D2 Branch OVERLAY_L4D2BRANCH
                iniD["OVERLAY_L4D2BRANCH"]["demo_protocol"] = Convert.ToInt32(checkBox33.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["net_protocol"] = Convert.ToInt32(checkBox32.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["server_name"] = Convert.ToInt32(checkBox31.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["client_name"] = Convert.ToInt32(checkBox29.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["map_name"] = Convert.ToInt32(checkBox28.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["game_directory"] = Convert.ToInt32(checkBox27.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["measured_time"] = Convert.ToInt32(checkBox26.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["measured_ticks"] = Convert.ToInt32(checkBox25.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["save_flag"] = Convert.ToInt32(checkBox24.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["autosave_flag"] = Convert.ToInt32(checkBox23.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["adjusted_time"] = Convert.ToInt32(checkBox34.Checked).ToString();
                iniD["OVERLAY_L4D2BRANCH"]["adjusted_ticks"] = Convert.ToInt32(checkBox35.Checked).ToString();
                //GOLDSOURCE OVERLAY_GOLDSOURCE
                iniD["OVERLAY_GOLDSOURCE"]["demo_protocol"] = Convert.ToInt32(checkBox44.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["net_protocol"] = Convert.ToInt32(checkBox43.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["server_name"] = Convert.ToInt32(checkBox42.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["client_name"] = Convert.ToInt32(checkBox40.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["map_name"] = Convert.ToInt32(checkBox39.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["game_directory"] = Convert.ToInt32(checkBox38.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["measured_time"] = Convert.ToInt32(checkBox37.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["measured_ticks"] = Convert.ToInt32(checkBox36.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["highest_fps"] = Convert.ToInt32(checkBox48.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["lowest_fps"] = Convert.ToInt32(checkBox47.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["average_fps"] = Convert.ToInt32(checkBox4.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["lowest_msec"] = Convert.ToInt32(checkBox46.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["highest_msec"] = Convert.ToInt32(checkBox45.Checked).ToString();
                iniD["OVERLAY_GOLDSOURCE"]["average_msec"] = Convert.ToInt32(checkBox49.Checked).ToString();
                parser.WriteFile(Main.SettingsPath, iniD);
            }
            Close();
        }

        private void button4_Click(object sender, EventArgs e) //CANCEL
        {
            Close();
        }
    }
}