using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace VolvoWrench.Demo_stuff
{
    internal class BitBuffer
    {
        private static readonly byte[] Mtbl = {0, 1, 3, 7, 15, 31, 63, 127, 255};
        private readonly byte[] _buf;
        private uint _pos;

        public BitBuffer(byte[] data)
        {
            _buf = data;
        }

        public void Seek(uint bits) => _pos += bits;

        public uint ReadBits(uint bits)
        {
            uint ret = 0;
            var left = bits;

            while (left > 0)
            {
                var idx = _pos >> 3; 
                var bit = _pos & 7; 
                var toget = Math.Min(8 - bit, left);

                var nib = (uint) (_buf[idx] >> (int) bit & Mtbl[toget]);
                ret |= nib << (int) (bits - left);

                _pos += toget;
                left -= toget;
            }

            return ret;
        }

        public bool ReadBool()
        {
            return ReadBits(1) != 0;
        }

        public float ReadFloat() => new UIntFloat {intvalue = ReadBits(32)}.floatvalue;

        public string ReadString()
        {
            var temp = new List<byte>();
            while (true)
            {
                var c = (byte) ReadBits(8);
                if (c == 0)
                    break;
                temp.Add(c);
            }
            return Encoding.UTF8.GetString(temp.ToArray());
        }

        public float ReadCoord()
        {
            var hasint = ReadBool();
            var hasfract = ReadBool();
            float value = 0;

            if (hasint || hasfract)
            {
                var sign = ReadBool();
                if (hasint)
                    value += ReadBits(14) + 1;
                if (hasfract)
                    value += ReadBits(5)*(1/32f);
                if (sign)
                    value = -value;
            }

            return value;
        }

        public float[] ReadVecCoord()
        {
            var hasx = ReadBool();
            var hasy = ReadBool();
            var hasz = ReadBool();

            return new[]
            {
                hasx ? ReadCoord() : 0,
                hasy ? ReadCoord() : 0,
                hasz ? ReadCoord() : 0
            };
        }

        public uint BitsLeft()
        {
            // Protected from overflow
            if ((_buf.Length << 3) >= _pos)
                return (uint) (_buf.Length << 3) - _pos;
            return 0;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct UIntFloat
        {
            [FieldOffset(0)] public uint intvalue;
            [FieldOffset(0)] public readonly float floatvalue;
        }
    }
}