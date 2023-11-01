using BenchmarkDotNet.Running;
using Chess2.Benchs;
using Chess2.Tests;

//BenchmarkRunner.Run<BenchTests>();

// Если все тесты проходят, то консоль пустая.
TestClass.TestKills1();
TestClassJulia.TestKills1();
TestClassJulia.TestKills2();
TestClassJulia.TestKills3();
TestClass.TestKills4();
TestClassJulia.TestKills4();

