# ILNumerics.ONAL  
### Open Numerical Algorithm Language (ONAL) for .NET

[![NuGet](https://img.shields.io/nuget/v/ILNumerics.ONAL.svg)](https://www.nuget.org/packages/ILNumerics.ONAL)
[![Build](https://img.shields.io/github/actions/workflow/status/ILNumerics/ILNumerics.ONAL/build.yml)](...)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

### Write serious numerical algorithms in .NET: NumPy/MATLAB semantic, proven industrial reliability, and no vendor lock-in.

Open-sourced from the proven, mature core of [ILNumerics Computing](https://ilnumerics.net).

#### Your algorithms.   Your IP.   Your language.

---
## Why ILNumerics.ONAL ? 
Because serious numerical software needs more than wrapping NumPy:

| Requirement | How ILNumerics.ONAL addresses it |
|---|---|
| ✓ Expressive numerical language | Dense n-dimensional arrays, logicals, cells and complex numbers with MATLAB and NumPy compatible semantics let algorithms be expressed as high-level  mathematics rather than low-level optimization code. |
| ✓ Complete numerical foundation | Broad support for elementary functions, linear algebra, transforms and the core building blocks expected from a modern numerical language. |
| ✓ Maintainable algorithm IP | Open-source language core enables long-term maintainability and protects algorithm IP from vendor dependency. |
| ✓ Compatibility and portability | State-of-the-art numerical kernels such as LAPACK are made available through automated .NET translations — enabling true *write once, run everywhere* across the .NET ecosystem. |
| ✓ Production-grade maturity | Grown and hardened over 15 years alongside .NET, the codebase has been proven in industrial and academic applications since 2011. |
| ✓ Tooling and debuggability | Visual Studio integration, array visualizers, large team support and standard workflows for professional development, maintenance and deployment. |
| ✓ Competitive execution speed | Standard .NET execution delivers practical performance for serious numerical workloads. |
| ✓ Optional path to additional performance | When needed, the same algorithms can optionally run on ILNumerics Computing for [runtime optimizations](https://ilnumerics.net/ilnumerics-autonomous-computing.html), without changing the algorithm code. |


ILNumerics.ONAL is the open-sourced language core of [ILNumerics](https://ilnumerics.net),
providing multidimensional array programming and the fundamental tools
for building maintainable numerical algorithm IP in .NET. Write serious numerical algorithms in .NET — with Matlab/NumPy-like semantics, industrial robustness, and no vendor lock-in.



## Prototype like MATLAB. Deploy like HPC software.

ILNumerics.ONAL combines:

- N-dimensional arrays
- Mathematical array programming
- Numerical algorithms and transforms
- Developer tooling and visual debugging
- Drop-in migration path to ILNumerics Computing Engine

Inspired by:

- MATLAB
- NumPy
- Julia

but built for .NET.

---

# Features

## N-Dimensional Arrays

```csharp
using ILNumerics;
using static ILNumerics.ILMath;

var A = rand(4,4);
var B = sin(A) + 2*A;

Console.WriteLine(B);
```

Supports:

- Scalars
- Vectors
- Matrices
- Tensors
- Logical arrays
- Cell arrays

MATLAB / NumPy-like semantics included.

---

## Array Expressions

Write mathematics naturally:

```csharp
var x = linspace(0,10,1000);

var y =
    exp(-x/5) *
    sin(4*x)
    + cos(x);
```

Broadcasting, indexing, slicing and vectorized expressions included.

---

## Linear Algebra

```csharp
var A = rand(500,500);

Array<double> U = 0.0, V = 0.0; 
var S = svd(A, U, V);
// work with S, U, V...


```

Includes:

- LU
- QR
- SVD
- Eigendecomposition
- Solvers
- Matrix products

---

## Fast Fourier Transform

```csharp
var signal = sin(20*t) + .4*sin(90*t);

var spectrum = fft(signal);
```

Built-in signal processing foundations.

---

## Debugging Visualizers

Numerical code should be inspectable.

Features include:

- Visual Studio tooltips  
- Graphical array watch window  
- Interactive debugging support

![visualizer](https://ilnumerics.net/media/photos/VisualStudioDebugging_L.jpg)

---

# Performance Philosophy

ONAL prioritizes:

- Correct semantics  
- Stable APIs  
- Expressive algorithms

while still delivering competitive speed among .NET numerical DSLs.

---

# Installation

```bash
dotnet add package ILNumerics.ONAL
```

NuGet:

https://www.nuget.org/packages/ILNumerics.ONAL

---

# Quick Start

## Matrix Example

```csharp
using ILNumerics;

var A = rand(3,3);

var B = A.T * A;

```

---

## Solve Linear System

```csharp
var A = rand(100,100);
var b = rand(100,1);

var x = linsolve(A,b);
```

---

## Signal Processing Example

```csharp
var fs = 1000;

var t =
    linspace(
      0,
      1,
      fs);

var signal =
      sin(2*pi*50*t)
    + .5*sin(2*pi*120*t);

var spectrum = fft(signal);
```

---

# Contributing

Contributions welcome.

```bash
git clone https://github.com/ILNumerics/ILNumerics.ONAL
```

Open an issue or PR.

---

# License

MIT

---

## Citation

If you use ONAL in research:

```bibtex
@software{ilnumerics_onal,
 title={ILNumerics.ONAL},
 author={ILNumerics GmbH},
 year={2026},
 url={https://github.com/ILNumerics/ILNumerics.ONAL}
}
```