using Benchmark.Abstractions;
using Benchmark.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmark.Tools
{
    internal class CpuBenchmarkDriver : ICpuBenchmarkDriver
    {
        private const int Seed = 1745929123;

        public CpuBenchmarkResult Execute()
        {
            var singleScore = GetSingleCoreScore();
            var multipleScore = GetMultipleCoreScore();
            var result = new CpuBenchmarkResult()
            {
                SingleCoreScore = singleScore,
                MultipleCoreScore = multipleScore,
            };

            return result;
        }

        private long GetSingleCoreScore()
        {
            var ts = Stopwatch.StartNew();
            SortArrayTask();
            ts.Stop();

            var score = (1000000 / ts.ElapsedMilliseconds + 1);
            return (long)score;
        }

        private long GetMultipleCoreScore()
        {
            var ts = Stopwatch.StartNew();

            var threads = new List<Thread>();

            for (var i = 0; i < 10; i++)
                threads.Add(new Thread(SortArrayTask));

            foreach (var thread in threads)
                thread.Start();

            while (threads.Any(x => x.IsAlive))
            {
                continue;
            }

            ts.Stop();

            var score = (1000000 / ts.ElapsedMilliseconds + 1);
            return (long)score * 10;
        }

        private void SortArrayTask()
        {
            for (var i = 0; i < 10000; i++)
            {
                var array = GenerateArray();
                var sorted = array.OrderBy(x => x);
            }
        }

        private IEnumerable<int> GenerateArray()
        {
            var array = new int[10000];
            var rnd = new Random(Seed);

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next();
            }

            return array;
        }
    }
}
