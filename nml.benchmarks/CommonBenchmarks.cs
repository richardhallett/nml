
namespace Nml.Benchmarks
{
    #pragma warning disable 219
    public class CommonBenchmarks
    {
        [Benchmark(Name = "Common Lerp float")]
        public void Lerp()
        {
            var r = Common.Lerp(0.0f, 5.0f, 0.5f);
        }

        [Benchmark(Name = "Common Lerp double")]
        public void LerpDouble()
        {
            double a = 0.5E+7;
            double b = 1.0E+7; 
            var r = Common.Lerp(a, b, 0.5f);
        }

        [Benchmark(Name = "Common SinCos")]
        public void SinCos()
        {
            float sin;
            float cos;
            Common.SinCos(Common.Pi, out sin, out cos);
        }
    }
}