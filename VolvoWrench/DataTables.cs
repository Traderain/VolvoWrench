using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace netdecode
{
    class DataTables
    {
        enum SendPropType : uint
        {
            Int = 0,
            Float,
            Vector,
            VectorXY,
            String,
            Array,
            DataTable,
            Int64
        }

        [Flags]
        enum SendPropFlags : uint
        {
            UNSIGNED = 1,
            COORD = 2,
            NOSCALE = 4,
            ROUNDDOWN = 8,
            ROUNDUP = 16,
            NORMAL = 32,
            EXCLUDE = 64,
            XYZE = 128,
            INSIDEARRAY = 256,
            PROXY_ALWAYS_YES = 512,
            CHANGES_OFTEN = 1024,
            IS_A_VECTOR_ELEM = 2048,
            COLLAPSIBLE = 4096,
            COORD_MP = 8192,
            COORD_MP_LOWPRECISION = 16384,
            COORD_MP_INTEGRAL = 32768
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

                    if (type == SendPropType.DataTable || (flags & SendPropFlags.EXCLUDE) != 0)
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
