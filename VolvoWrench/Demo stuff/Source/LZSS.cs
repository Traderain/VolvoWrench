using System;

namespace netdecode
{
    public class LZSS
    {
        private const int LZSS_LOOKSHIFT = 4;
        private const int LZSS_LOOKAHEAD = (1 << LZSS_LOOKSHIFT);
        // let me know if you figure out what algorithm this is
        public static byte[] Decompress(byte[] bufIn, uint outSize)
        {
            uint totalBytes = 0;
            var cmdByte = 0;
            var getCmdByte = 0;

            var pInput = 0;
            var pOutput = 0;
            var bufOut = new byte[outSize];

            for (;;)
            {
                if (getCmdByte == 0)
                {
                    cmdByte = bufIn[pInput];
                    pInput++;
                }
                getCmdByte = (getCmdByte + 1) & 0x07;

                if ((cmdByte & 0x01) != 0)
                {
                    var position = bufIn[pInput] << LZSS_LOOKSHIFT;
                    pInput++;
                    position |= (bufIn[pInput] >> LZSS_LOOKSHIFT);
                    var count = (bufIn[pInput] & 0x0F) + 1;
                    pInput++;
                    Console.Write(count + " ");
                    if (count == 1)
                    {
                        break;
                    }
                    var pSource = pOutput - position - 1;
                    for (var i = 0; i < count; i++)
                    {
                        bufOut[pOutput] = bufIn[pSource];
                        pOutput++;
                        pSource++;
                    }
                    totalBytes += (uint) count;
                }
                else
                {
                    bufOut[pOutput] = bufIn[pInput];
                    pOutput++;
                    pInput++;
                    totalBytes++;
                }
                cmdByte = cmdByte >> 1;
            }
            Console.WriteLine();
            throw new Exception();
            //return bufOut;
        }
    }
}