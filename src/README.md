## FlowSynx SHA Hashing Plugin

The **SHA Hashing Plugin** is a pre-packaged, plug-and-play integration component for the FlowSynx engine. It provides a wide range of secure hashing algorithms, including SHA-1, SHA-2, SHA-3, and SHAKE variants, enabling workflows to compute cryptographic hashes of input data. This plugin supports both text and binary data and allows configurable output lengths for extendable-output functions (XOF) like SHAKE128 and SHAKE256.

This plugin is automatically installed by the FlowSynx engine when selected within the platform. It is not intended for manual installation or standalone developer use outside the FlowSynx environment.

---

## Purpose

The SHA Hashing Plugin allows FlowSynx users to:

- Compute secure hash digests for input text or binary data.
- Support workflows involving data integrity checks, digital signatures, or secure token generation.
- Use configurable hashing algorithms without writing code.
- Generate hashes with fixed or variable-length outputs (for SHAKE algorithms).

---

## Supported Operations

The plugin supports the following hash algorithms:

- **SHA-1**
- **SHA-224**
- **SHA-256**
- **SHA-384**
- **SHA-512**
- **SHA-512/224**
- **SHA-512/256**
- **SHA3-224**
- **SHA3-256**
- **SHA3-384**
- **SHA3-512**
- **SHAKE128** (configurable output length)
- **SHAKE256** (configurable output length)

---

## Plugin Specifications

This plugin does not require external configuration. It is ready to use within FlowSynx workflows.  

---

## Input Parameters

| Parameter       | Type     | Required | Description                                                                                     |
|------------------|----------|----------|-------------------------------------------------------------------------------------------------|
| `Algorithm`      | string   | Yes      | The hashing algorithm to use (e.g., `sha256`, `sha3-512`, `shake128`).                          |
| `InputText`      | string   | No       | The input text to hash (UTF-8 encoded). Either `InputText` or `InputBytes` **must** be provided.|
| `InputBytes`     | byte[]   | No       | The raw binary data to hash. Either `InputText` or `InputBytes` **must** be provided.           |
| `OutputLength`   | int      | No       | The desired output length in bytes for SHAKE algorithms. Ignored for fixed-length algorithms.   |

---

### Example Input (SHA-256)

```json
{
  "Operation": "sha256",
  "InputText": "Hello, FlowSynx!"
}
```

### Example Input (SHAKE128 with custom output length)

```json
{
  "Operation": "shake128",
  "InputText": "Custom output hash",
  "OutputLength": 64
}
```

---

## Debugging Tips

- Ensure the `Algorithm` name matches one of the supported operations (case-insensitive).
- Provide either `InputText` or `InputBytes`, but not both simultaneously.
- For SHAKE algorithms, specify `OutputLength` to define the size of the output hash. If omitted, a default length may be applied.
- Binary outputs are Base64 encoded in FlowSynx for easy handling.

---

## Security Notes

- All hash computations are performed in memory and are not persisted beyond the workflow execution.
- No sensitive input data is stored unless explicitly configured in the workflow.
- The plugin uses secure, well-tested cryptographic libraries provided by the FlowSynx runtime.

---

## License

© FlowSynx. All rights reserved.