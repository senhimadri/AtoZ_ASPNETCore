using BenchmarkDotNet.Running;
using CodeBenchmarking;

var summary = BenchmarkRunner.Run(typeof(BenchmarkLINQPerformance));

