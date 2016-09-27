using System;
using System.Windows.Forms;

namespace VolvoWrench.Netdec
{
    class DataTables
    {
        enum SendPropType : uint
        {
            Int = 0,
            Float,
            Vector,
            VectorXy,
            String,
            Array,
            DataTable,
            Int64
        }

        [Flags]
        enum SendPropFlags : uint
        {
            Unsigned = 1,
            Coord = 2,
            Noscale = 4,
            Rounddown = 8,
            Roundup = 16,
            Normal = 32,
            Exclude = 64,
            Xyze = 128,
            Insidearray = 256,
            ProxyAlwaysYes = 512,
            ChangesOften = 1024,
            IsAVectorElem = 2048,
            Collapsible = 4096,
            CoordMp = 8192,
            CoordMpLowprecision = 16384,
            CoordMpIntegral = 32768
        }

        static void ParseTables(BitBuffer bb, TreeNode node)
        {
            while (bb.ReadBool())
            {
                bool needsdecoder = bb.ReadBool();
                var dtnode = node.Nodes.Add(bb.ReadString());
                if (needsdecoder) dtnode.Text += "*";

                var numprops = bb.ReadBits(10);
                dtnode.Text += " (" + numprops + " props)";

                for (int i = 0; i < numprops; i++)
                {
                    var type = (SendPropType)bb.ReadBits(5);
                    var propnode = dtnode.Nodes.Add("DPT_" + type + " " + bb.ReadString());
                    var flags = (SendPropFlags)bb.ReadBits(16);

                    if (type == SendPropType.DataTable || (flags & SendPropFlags.Exclude) != 0)
                        propnode.Text += " : " + bb.ReadString();
                    else
                    {
                        if (type == SendPropType.Array)
                            propnode.Text += "[" + bb.ReadBits(10) + "]";
                        else
                        {
                            bb.Seek(64);
                            propnode.Text += " (" + bb.ReadBits(7) + " bits)";
                        }
                    }
                }
            }
        }

        static void ParseClassInfo(BitBuffer bb, TreeNode node)
        {
            var classes = bb.ReadBits(16);

            for (int i = 0; i < classes; i++)
                node.Nodes.Add("[" + bb.ReadBits(16) + "] " + bb.ReadString() + " (" + bb.ReadString() + ")");
        }

        public static void Parse(byte[] data, TreeNode node)
        {
            var bb = new BitBuffer(data);
            ParseTables(bb, node.Nodes.Add("Send tables"));
            ParseClassInfo(bb, node.Nodes.Add("Class info"));
        }
    }
}
