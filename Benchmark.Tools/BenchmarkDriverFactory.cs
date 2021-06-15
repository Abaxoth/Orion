using Benchmark.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmark.Tools
{
    public class BenchmarkDriverFactory
    {
        public ICpuBenchmarkDriver CreateCpuDriver()
        {
            return new CpuBenchmarkDriver();
        }
    }
}
