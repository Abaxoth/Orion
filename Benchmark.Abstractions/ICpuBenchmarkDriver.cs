using System.Threading.Tasks;
using Benchmark.Models;

namespace Benchmark.Abstractions
{
    public interface ICpuBenchmarkDriver
    {
        CpuBenchmarkResult Execute();
    }
}
