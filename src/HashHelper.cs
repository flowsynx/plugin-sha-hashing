using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;

namespace FlowSynx.Plugins.ShaHashing;

internal static class HashHelper
{
    public static byte[] SHA1(byte[] input) => System.Security.Cryptography.SHA1.HashData(input);
    public static byte[] SHA256(byte[] input) => System.Security.Cryptography.SHA256.HashData(input);
    public static byte[] SHA384(byte[] input) => System.Security.Cryptography.SHA384.HashData(input);
    public static byte[] SHA512(byte[] input) => System.Security.Cryptography.SHA512.HashData(input);

    public static byte[] SHA3_256(byte[] input) => System.Security.Cryptography.SHA3_256.HashData(input);
    public static byte[] SHA3_384(byte[] input) => System.Security.Cryptography.SHA3_384.HashData(input);
    public static byte[] SHA3_512(byte[] input) => System.Security.Cryptography.SHA3_512.HashData(input);

    public static byte[] SHA224(byte[] input)
    {
        var digest = new Sha224Digest();
        return ComputeDigest(digest, input);
    }

    public static byte[] SHA512_224(byte[] input)
    {
        var digest = new Sha512tDigest(224);
        return ComputeDigest(digest, input);
    }

    public static byte[] SHA512_256(byte[] input)
    {
        var digest = new Sha512tDigest(256);
        return ComputeDigest(digest, input);
    }

    public static byte[] SHA3_224(byte[] input)
    {
        var digest = new Sha3Digest(224);
        return ComputeDigest(digest, input);
    }

    public static byte[] SHAKE128(byte[] input, int outputLength)
    {
        var digest = new ShakeDigest(128);
        return ComputeXofDigest(digest, input, outputLength);
    }

    public static byte[] SHAKE256(byte[] input, int outputLength)
    {
        var digest = new ShakeDigest(256);
        return ComputeXofDigest(digest, input, outputLength);
    }

    private static byte[] ComputeDigest(IDigest digest, byte[] input)
    {
        digest.BlockUpdate(input, 0, input.Length);
        var result = new byte[digest.GetDigestSize()];
        digest.DoFinal(result, 0);
        return result;
    }

    private static byte[] ComputeXofDigest(IXof xof, byte[] input, int outputLength)
    {
        xof.BlockUpdate(input, 0, input.Length);
        var result = new byte[outputLength];
        xof.OutputFinal(result, 0, outputLength);
        return result;
    }
}