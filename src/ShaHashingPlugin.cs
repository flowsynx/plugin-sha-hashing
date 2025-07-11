using FlowSynx.PluginCore;
using FlowSynx.PluginCore.Extensions;
using FlowSynx.PluginCore.Helpers;
using FlowSynx.Plugins.ShaHashing.Models;
using System.Text;

namespace FlowSynx.Plugins.ShaHashing;

public class ShaHashingPlugin : IPlugin
{
    private IPluginLogger? _logger;
    private bool _isInitialized;

    public PluginMetadata Metadata => new PluginMetadata
    {
        Id = Guid.Parse("cc9f1294-f8e2-4253-acdf-2b0f15cefbc8"),
        Name = "Hahing.Sha",
        CompanyName = "FlowSynx",
        Description = Resources.PluginDescription,
        Version = new Version(1, 1, 0),
        Category = PluginCategory.Security,
        Authors = new List<string> { "FlowSynx" },
        Copyright = "© FlowSynx. All rights reserved.",
        Icon = "flowsynx.png",
        ReadMe = "README.md",
        RepositoryUrl = "https://github.com/flowsynx/plugin-sha-hashing",
        ProjectUrl = "https://flowsynx.io",
        Tags = new List<string>() { "flowSynx", "hashing", "sha-1", "sha-2", "sha-3", "shake", "security" },
        MinimumFlowSynxVersion = new Version(1, 1, 1),
    };

    public PluginSpecifications? Specifications { get; set; }

    public Type SpecificationsType => typeof(ShaHashingPluginSpecifications);

    private Dictionary<string, Func<InputParameter, CancellationToken, Task<object?>>> OperationMap => new(StringComparer.OrdinalIgnoreCase)
    {
        ["sha1"]       = HashUsing((bytes, _) => HashHelper.SHA1(bytes)),
        ["sha224"]     = HashUsing((bytes, _) => HashHelper.SHA224(bytes)),
        ["sha256"]     = HashUsing((bytes, _) => HashHelper.SHA256(bytes)),
        ["sha384"]     = HashUsing((bytes, _) => HashHelper.SHA384(bytes)),
        ["sha512"]     = HashUsing((bytes, _) => HashHelper.SHA512(bytes)),
        ["sha512/224"] = HashUsing((bytes, _) => HashHelper.SHA512_224(bytes)),
        ["sha512/256"] = HashUsing((bytes, _) => HashHelper.SHA512_256(bytes)),
        ["sha3-224"]   = HashUsing((bytes, _) => HashHelper.SHA3_224(bytes)),
        ["sha3-256"]   = HashUsing((bytes, _) => HashHelper.SHA3_256(bytes)),
        ["sha3-384"]   = HashUsing((bytes, _) => HashHelper.SHA3_384(bytes)),
        ["sha3-512"]   = HashUsing((bytes, _) => HashHelper.SHA3_512(bytes)),
        ["shake128"]   = HashUsing((bytes, spec) => HashHelper.SHAKE128(bytes, spec.OutputLength ?? 32)),
        ["shake256"]   = HashUsing((bytes, spec) => HashHelper.SHAKE256(bytes, spec.OutputLength ?? 64)),
    };

public IReadOnlyCollection<string> SupportedOperations => OperationMap.Keys;

    public Task Initialize(IPluginLogger logger)
    {
        if (ReflectionHelper.IsCalledViaReflection())
            throw new InvalidOperationException(Resources.ReflectionBasedAccessIsNotAllowed);

        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
        _isInitialized = true;
        return Task.CompletedTask;
    }

    public Task<object?> ExecuteAsync(PluginParameters parameters, CancellationToken cancellationToken)
    {
        if (ReflectionHelper.IsCalledViaReflection())
            throw new InvalidOperationException(Resources.ReflectionBasedAccessIsNotAllowed);

        if (!_isInitialized)
            throw new InvalidOperationException($"Plugin '{Metadata.Name}' v{Metadata.Version} is not initialized.");

        var inputParameter = parameters.ToObject<InputParameter>();
        var algorithm = inputParameter.Algorithm;

        if (OperationMap.TryGetValue(algorithm, out var handler))
        {
            return handler(inputParameter, cancellationToken);
        }

        throw new NotSupportedException($"Sha hashing plugin: Algorithms '{algorithm}' is not supported.");
    }

    private Func<InputParameter, CancellationToken, Task<object?>> HashUsing(Func<byte[], InputParameter, byte[]> hashFunc)
    {
        return (parameters, cancellationToken) =>
        {
            byte[] inputBytes = parameters.InputBytes
                ?? (parameters.InputText != null ? Encoding.UTF8.GetBytes(parameters.InputText) : null)
                ?? throw new ArgumentException("Either InputText or InputBytes must be provided.");

            byte[] hash = hashFunc(inputBytes, parameters);
            string hex = Convert.ToHexString(hash).ToLowerInvariant();

            return Task.FromResult<object?>(new PluginContext(Guid.NewGuid().ToString(), "Data") 
            { 
                Format = "Hashing", 
                Content = hex 
            });
        };
    }
}