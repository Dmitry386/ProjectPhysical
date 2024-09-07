using System.IO;
using System.IO.Compression;
using System.Text;

namespace DVUnityUtilities
{
    public static class ByteUtils
    {
        public static string ByteArrayToString(byte[] ba, bool decompress)
        {
            if (decompress) ba = Decompress(ba);
            return Encoding.UTF8.GetString(ba);
        }

        public static byte[] StringToByteArray(string s, bool compress)
        {
            byte[] r = Encoding.UTF8.GetBytes(s);
            if (compress) r = Compress(r);
            return r;
        }

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}