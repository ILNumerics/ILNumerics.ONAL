# ILNumerics.ONAL

## Numerical computing done right in .NET

ILNumerics.ONAL is a numerical computing library for .NET that brings full MATLAB and NumPy semantics into C#, Visual Basic, and F#.

It is not a “NumPy-like” API.
It is a **complete numerical language embedded in .NET**.

---

## Write MATLAB / NumPy code. In .NET. Exactly.

ILNumerics.ONAL provides:

* n-dimensional arrays with **100% MATLAB and NumPy semantics**
* identical indexing, slicing, broadcasting behavior
* consistent and predictable numerical results

No approximations. No surprises. No re-learning.

---

## Complete, tested numerical foundation

All linear algebra functionality is based on Netlib LAPACK:

* full LAPACK coverage
* translated from original FORTRAN using ILNumerics compiler technology
* **millions of lines of Netlib tests — all passing**

This results in a **fully managed, production-grade LAPACK implementation for .NET**.

Optional native LAPACK backends are available via NuGet if required.

---

## Numerical DSL for .NET

ILNumerics.ONAL is a domain-specific language embedded directly into .NET:

* natural expression of numerical algorithms in C#
* usable from Visual Basic and F#
* no external runtimes or language bridges

You write algorithms — not wrappers.

---

## Debug numbers like code

Numerical development becomes interactive with built-in Visual Studio integration:

* graphical Array Visualizer
* inspect arrays during debugging
* IntelliSense and tooltips showing array contents

You don’t print numbers.
You **see your data**.

---

## Write the algorithm. Skip the optimization nightmare.

Traditional numerical optimization requires:

* low-level code
* manual parallelization
* hardware-specific tuning
* significant maintenance effort

ILNumerics.ONAL removes this complexity:

* write clear, high-level array code
* ignore memory layout, threading, and hardware

---

## Faster than manual optimization

With the optional ILNumerics Accelerator:

* automatic analysis and transformation of array code
* advanced parallelization and optimization
* no code changes required

Result:

> execution speeds that exceed hand-optimized implementations

Even expert-level manual optimization cannot reliably achieve the same results without sacrificing maintainability.

---

## Managed by default. Native when needed.

* fully managed implementation out of the box
* optional native LAPACK integration via NuGet

Choose portability or native integration — without changing your code.

---

## Thread-safe by design

* safe for concurrent execution
* integrates into custom execution models
* no hidden global state

---

## Replace fragile in-house math libraries

Many teams maintain internal numerical codebases that are:

* incomplete
* inconsistent
* slow
* difficult to validate

ILNumerics.ONAL provides a stable, proven alternative:

* complete numerical functionality
* validated algorithms
* long-term maintainability

Your algorithms are your IP — not the math infrastructure underneath.

---

## Why this exists

Because too many numerical systems are built on fragile foundations.

We have spent over a decade building and refining a complete numerical computing engine for .NET.

We have also seen how easily such foundations are:

* reimplemented poorly
* copied incompletely
* or locked away in proprietary systems

The result is always the same:

* inconsistent behavior
* slow execution
* and code that becomes impossible to maintain

We decided to change that.

ILNumerics.ONAL is released as a **fully functional, production-grade numerical foundation** — so you don’t have to build your own.

Your algorithms are your IP.

The math underneath should not be your problem.

---

## Summary

ILNumerics.ONAL provides:

* a complete numerical language for .NET
* full MATLAB and NumPy semantics
* a fully managed LAPACK implementation
* deep Visual Studio integration
* optional high-performance optimization

It allows you to write numerical algorithms once — correctly, clearly, and future-proof.

---

## Links

* Array Visualizer: https://ilnumerics.net/array-visualizer-c.html
* Performance / Accelerator: https://ilnumerics.net/faster-array-codes.html

---

## License
MIT
