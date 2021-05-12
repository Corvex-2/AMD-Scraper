using System.IO;
using System.IO.Compression;
using System.Text;

namespace AMD_Scraper
{
    public static class GZipping
    {
        public static string Decompress(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static byte[] Compress(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Compress))
                {
                    gs.CopyTo(mso);
                }
                return mso.ToArray();
            }
        }
    }
}
