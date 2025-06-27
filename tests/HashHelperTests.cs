using System.Text;

namespace FlowSynx.Plugins.ShaHashing.UnitTests;

public class HashHelperTests
{
    private static readonly byte[] TestInput = Encoding.UTF8.GetBytes("hello world");

    [Theory]
    [InlineData("hello world", "2aae6c35c94fcfb415dbe95f408b9ce91ee846ed")] // SHA1
    public void SHA1_ShouldMatchExpectedHash(string input, string expectedHex)
    {
        var actual = HashHelper.SHA1(Encoding.UTF8.GetBytes(input));
        Assert.Equal(expectedHex, ConvertToHex(actual));
    }

    [Theory]
    [InlineData("hello world", "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9")] // SHA256
    public void SHA256_ShouldMatchExpectedHash(string input, string expectedHex)
    {
        var actual = HashHelper.SHA256(Encoding.UTF8.GetBytes(input));
        Assert.Equal(expectedHex, ConvertToHex(actual));
    }

    [Theory]
    [InlineData("hello world", "fdbd8e75a67f29f701a4e040385e2e23986303ea10239211af907fcbb83578b3e417cb71ce646efd0819dd8c088de1bd")] // SHA384
    public void SHA384_ShouldMatchExpectedHash(string input, string expectedHex)
    {
        var actual = HashHelper.SHA384(Encoding.UTF8.GetBytes(input));
        Assert.Equal(expectedHex, ConvertToHex(actual));
    }

    [Theory]
    [InlineData("hello world", "309ecc489c12d6eb4cc40f50c902f2b4d0ed77ee511a7c7a9bcd3ca86d4cd86f989dd35bc5ff499670da34255b45b0cfd830e81f605dcf7dc5542e93ae9cd76f")] // SHA512
    public void SHA512_ShouldMatchExpectedHash(string input, string expectedHex)
    {
        var actual = HashHelper.SHA512(Encoding.UTF8.GetBytes(input));
        Assert.Equal(expectedHex, ConvertToHex(actual));
    }

    [Fact]
    public void SHA3_256_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA3_256(TestInput);
        Assert.Equal(32, hash.Length);
    }

    [Fact]
    public void SHA3_384_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA3_384(TestInput);
        Assert.Equal(48, hash.Length);
    }

    [Fact]
    public void SHA3_512_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA3_512(TestInput);
        Assert.Equal(64, hash.Length);
    }

    [Fact]
    public void SHA224_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA224(TestInput);
        Assert.Equal(28, hash.Length);
    }

    [Fact]
    public void SHA512_224_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA512_224(TestInput);
        Assert.Equal(28, hash.Length);
    }

    [Fact]
    public void SHA512_256_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA512_256(TestInput);
        Assert.Equal(32, hash.Length);
    }

    [Fact]
    public void SHA3_224_ShouldProduceCorrectLength()
    {
        var hash = HashHelper.SHA3_224(TestInput);
        Assert.Equal(28, hash.Length);
    }

    [Fact]
    public void SHAKE128_ShouldProduceExactLength()
    {
        int len = 64;
        var hash = HashHelper.SHAKE128(TestInput, len);
        Assert.Equal(len, hash.Length);
    }

    [Fact]
    public void SHAKE256_ShouldProduceExactLength()
    {
        int len = 64;
        var hash = HashHelper.SHAKE256(TestInput, len);
        Assert.Equal(len, hash.Length);
    }

    private static string ConvertToHex(byte[] data) =>
        BitConverter.ToString(data).Replace("-", "").ToLowerInvariant();
}