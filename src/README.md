## FlowSynx SHA  Hash Plugin – FlowSynx Platform Integration

The SHA Hash Plugin is a secure, plug-and-play component for the FlowSynx engine. It enables the computation of various SHA family hashes (including SHA-1, SHA-2, SHA-3, and SHAKE) from text or binary data, and is designed to seamlessly integrate into FlowSynx’s no-code/low-code automation workflows.

This plugin is automatically installed and managed by the FlowSynx engine when selected within the platform. It is not intended for standalone use or manual installation outside the FlowSynx environment.

---

## Purpose

This plugin allows FlowSynx users to generate cryptographic hash values using a range of SHA algorithms for input values as part of their workflow logic—without writing any code. Once activated, it becomes available as a utility function within the platform's workflow builder, supporting automation, validation, data integrity, and transformation scenarios.

---

## Supported Algorithms

The plugin supports the following hash algorithms:

- SHA-1
- SHA-224
- SHA-256
- SHA-384
- SHA-512
- SHA-512/224
- SHA-512/256
- SHA3-224
- SHA3-256
- SHA3-384
- SHA3-512
- SHAKE128 (configurable output length)
- SHAKE256 (configurable output length)

## Notes

- This plugin is supported exclusively within the FlowSynx platform.
- It is installed automatically by the FlowSynx engine.
- All hashing logic is securely executed within the FlowSynx runtime.
- Plugin input (e.g., text, bytes, output length) is configured through FlowSynx-managed specifications.
- SHAKE algorithms support customizable output lengths defined in the plugin specification.

---

## License

© FlowSynx. All rights reserved.