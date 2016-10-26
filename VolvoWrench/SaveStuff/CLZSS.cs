/* https://github.com/LestaD/SourceEngine2007/blob/43a5c90a5ada1e69ca044595383be67f40b33c61/src_main/public/saverestoretypes.h#L323
 * https://github.com/LestaD/SourceEngine2007/blob/43a5c90a5ada1e69ca044595383be67f40b33c61/src_main/engine/host_saverestore.cpp
 * https://github.com/LestaD/SourceEngine2007/blob/43a5c90a5ada1e69ca044595383be67f40b33c61/src_main/gameui/BaseSaveGameDialog.cpp
 * https://github.com/LestaD/SourceEngine2007/blob/43a5c90a5ada1e69ca044595383be67f40b33c61/src_main/tier1/lzss.cpp
 * TODO: Implement these
 * 
 * Decompress the file with CLZSS -> Read header -> Parse files.
 * 
 * 
 */

using System;

namespace VolvoWrench.SaveStuff
{
    class CLZSS
    {
        public static readonly int LZSS_LOOKSHIFT = 4;
        public static readonly int LZSS_LOOKAHEAD = (1 << LZSS_LOOKSHIFT);
        public static readonly int DEFAULT_LZSS_WINDOW_SIZE = 4096;


        //LZSS_ID(('S'<<24)|('S'<<16)|('Z'<<8)|('L'))
        struct lzss_header_t
        {
             int id;
             int actualSize;    // always little endian
        };
        // expected to be sixteen bytes
        unsafe struct lzss_node_t
        {
            char* pData;
            lzss_node_t* pPrev;
            lzss_node_t* pNext;
            fixed char empty[4];
        };

        struct lzss_list_t
        {
            lzss_node_t pStart;
            lzss_node_t pEnd;
        };

        public static bool IsCompressed(char id)
        {
            return id == ('S' << 24) | id == ('S' << 16) | id == ('Z' << 8) | id == ('L');
        }

        public static byte[] AntiClzss(byte[] compressedfile)
        {
            return new byte[8];//TODO: Implement this
        }

    }
}
