using System;

namespace netdecode
{
    public class LZSS
    {
        const int LZSS_LOOKSHIFT = 4;
        const int LZSS_LOOKAHEAD = (1 << LZSS_LOOKSHIFT);

        // let me know if you figure out what algorithm this is
        public static byte[] Decompress(byte[] bufIn, uint outSize)
        {
            uint totalBytes = 0;
            int cmdByte = 0;
            int getCmdByte = 0;

            var pInput = 0;
            var pOutput = 0;
            var bufOut = new byte[outSize];

            for ( ;; )
            {
                if ( getCmdByte == 0 ) 
                {
                    cmdByte = bufIn[pInput];
                    pInput++;
                }
                getCmdByte = ( getCmdByte + 1 ) & 0x07;
                
                if ( (cmdByte & 0x01) != 0 )
                {
                    int position = bufIn[pInput] << LZSS_LOOKSHIFT;
                    pInput++;
                    position |= ( bufIn[pInput] >> LZSS_LOOKSHIFT );
                    int count = ( bufIn[pInput] & 0x0F ) + 1;
                    pInput++;
                    Console.Write(count + " ");
                    if ( count == 1 ) 
                    {
                        break;
                    }
                    int pSource = pOutput - position - 1;
                    for ( int i=0; i<count; i++ )
                    {
                        bufOut[pOutput] = bufIn[pSource];
                        pOutput++;
                        pSource++;
                    }
                    totalBytes += (uint)count;
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
            return bufOut;
        }
    }
}
