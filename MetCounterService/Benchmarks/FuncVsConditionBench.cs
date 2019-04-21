using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    [RPlotExporter, RankColumn]
    public class FuncVsConditionBench
    {

        [Params(true, false)]
        public bool TrueFalse;

        [Params(10, 50)]
        public int A;

        [Params(10, 66)]
        public int B;

        Func<bool> FuncTrue = () => true;
        Func<bool> FuncFalse => () => false;

        [Benchmark]
        public int WithCondition()
        {
            if (TrueFalse)
            {
                return A + B;
            }
            else
            {
                return A - B;
            }
        }

        [Benchmark]
        public int WithFuncTrue()
        {
            if (FuncTrue.Invoke())
                return A + B;
            else
                return A - B;
        }

        [Benchmark]
        public int WithFuncFalse()
        {
            if (FuncFalse.Invoke())
                return A + B;
            else
                return A - B;
        }


    }
    
    [RPlotExporter, RankColumn]
    public class Md5VsSha256
    {
        private SHA256 sha256 = SHA256.Create();
        private MD5 md5 = MD5.Create();
        private byte[] data;

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }
}
