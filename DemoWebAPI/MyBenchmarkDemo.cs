using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebAPI;

[MemoryDiagnoser]
public class MyBenchmarkDemo
{
    [GlobalSetup]
    public void GlobalSetup()
    {

    }

    [Benchmark]
    public void FirstBenchmarkMethods()
    { 
    }

    [Benchmark]
    public void SecondBenchmarkMethods()
    {

    }
}
