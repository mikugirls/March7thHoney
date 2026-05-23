# WebServer Open Source Notes

## Scope

This module is trimmed for public release:

- Removed web admin console routes and pages.
- Removed explicit account registration and account recovery HTTP endpoints/pages.
- Kept username/password login and token verify endpoints required by client flow.

## Login Behavior

Account creation is now login-driven only:

1. Client submits a normal login request.
2. Server tries to resolve account by login identifier.
3. If account does not exist and `ServerOption.AutoCreateUser` is enabled, server auto-creates account.
4. Login continues with the created account and returns normal login payload.

No standalone register API is required.

## RSA and Non-RSA Login

Both modes are supported:

- RSA mode: enabled when `WebSecurity.EnableRsaLoginDecrypt` is true and request marks encrypted credentials.
- Plain mode: falls back to non-RSA credential handling for compatible clients.

This keeps mixed-client compatibility while preserving a minimal public auth surface.
