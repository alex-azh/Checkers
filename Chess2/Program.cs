// Если все тесты проходит, то консоль пустая.

using BenchmarkDotNet.Running;
using Chess2.Benchs;

BenchmarkRunner.Run<BenchTests>();
