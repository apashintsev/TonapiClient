using System.Text;

namespace TonapiClient.Tests;

public static class HexEncodingExtensions
{
    // string → hex
    public static string ToHex(this string str)
    {
        var bytes = Encoding.Unicode.GetBytes(str);
        return bytes.ToHex();
    }

    // byte[] → hex
    public static string ToHex(this byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    // hex → string
    public static string FromHexToString(this string hexString)
    {
        var bytes = hexString.HexToBytes();
        return Encoding.Unicode.GetString(bytes);
    }

    // hex → byte[]
    public static byte[] HexToBytes(this string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new ArgumentException("Hex string length must be even.", nameof(hexString));

        var bytes = new byte[hexString.Length / 2];

        for (int i = 0; i < bytes.Length; i++)
            bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

        return bytes;
    }
}
