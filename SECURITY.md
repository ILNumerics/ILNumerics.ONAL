# Security Policy

The ILNumerics.ONAL project takes security issues seriously and appreciates responsible disclosure by researchers, users, and contributors.

## Supported Versions

Security updates are provided for the current `main` branch and for the latest stable release published by the project. Older releases may receive security fixes at the maintainers' discretion, depending on severity, affected users, and maintenance effort.

| Version | Supported |
| ------- | --------- |
| Latest stable release | Yes |
| `main` branch | Yes |
| Older releases | Best effort |

## Reporting a Vulnerability

Please do not report security vulnerabilities through public GitHub issues, pull requests, discussions, or other public channels.

To report a vulnerability, use GitHub's private vulnerability reporting feature for this repository:

1. Open the repository on GitHub.
2. Go to **Security**.
3. Select **Report a vulnerability**.
4. Include enough information for the maintainers to understand, reproduce, and assess the issue.

If private vulnerability reporting is not available, contact the maintainers through the security contact listed in the repository metadata or project website.

A useful report should include, where possible:

- A clear description of the vulnerability and its potential impact.
- Affected versions, packages, APIs, or platforms.
- Steps to reproduce the issue or a minimal proof of concept.
- Any relevant configuration, input data, build options, or runtime environment details.
- Whether the issue is already publicly known or has been reported elsewhere.

## Response Process

After receiving a report, the maintainers will make a best-effort attempt to:

1. Acknowledge receipt of the report.
2. Validate and assess the vulnerability.
3. Determine affected versions and severity.
4. Develop and review a fix, mitigation, or advisory.
5. Coordinate disclosure with the reporter when appropriate.
6. Publish a security advisory or release notes once a fix or mitigation is available.

Please allow the maintainers reasonable time to investigate and address the issue before public disclosure.

## Disclosure Guidelines

We ask reporters to follow responsible disclosure practices:

- Do not publicly disclose the vulnerability before the maintainers have had an opportunity to investigate and remediate it.
- Do not access, modify, or delete data that does not belong to you.
- Do not attempt denial-of-service attacks, social engineering, spam, or physical attacks.
- Do not use the vulnerability beyond what is necessary to demonstrate the issue.

Reports made in good faith help improve the security and reliability of ILNumerics.ONAL and the wider .NET ecosystem.

## Security Scope

Security-relevant issues may include, but are not limited to:

- Memory safety, unsafe code, or native interop issues.
- Malicious or malformed input causing unintended behavior.
- Build, packaging, or dependency-chain vulnerabilities.
- Vulnerabilities in numerical routines that may lead to unsafe execution, data corruption, or denial of service.
- Issues in test, benchmark, or sample code that could affect users who copy or execute it.

General bugs, feature requests, documentation improvements, and non-sensitive defects should be reported through the public GitHub issue tracker.

## Dependencies and Supply Chain

The project aims to maintain secure development practices for dependencies, build scripts, packages, and release artifacts. Reports about vulnerable dependencies, package integrity, build reproducibility, or compromised release assets are considered security-relevant and should be reported privately.

## .NET Foundation Code of Conduct

This project is expected to follow the .NET Foundation Code of Conduct. Security discussions, like all project interactions, should be respectful, constructive, and focused on improving the project.
