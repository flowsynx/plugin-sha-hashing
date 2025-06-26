namespace FlowSynx.Plugins.ShaHashing.Models;

internal class InputParameter
{
    public string Algorithm { get; set; } = "sha256";
    public string? InputText { get; set; }
    public byte[]? InputBytes { get; set; }
    public int? OutputLength { get; set; } // Only for SHAKE
}