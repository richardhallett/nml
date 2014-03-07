using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace nml.benchmarks
{
    /// <summary>
    /// Attribute that specifys whether a method is a benchmark test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BenchmarkAttribute : Attribute
    {
        /// <summary>
        /// Name of the benchmark to use, if not supplied the method name will be used.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional amount of iterations the method will be run for.
        /// </summary>
        public int Iterations { get; set; }
    }

    class Program
    {
        static private Dictionary<string, double> _results = new Dictionary<string, double>();

        static void Profile(string name, int iterations, Action func)
        {
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Lets do a little warm up
            for (int k = 0; k < 10; k++)
            {
                func();
            }

            var repeatResults = new List<double>();

            // Run through our tests and get a sample of results
            for (int i = 0; i < 10; i++)
            {
                var stopWatch = Stopwatch.StartNew();

                for (int j = 0; j < iterations; j++)
                {
                    func();
                }

                stopWatch.Stop();

                double msPerOp = stopWatch.Elapsed.TotalMilliseconds / iterations;
                double nsPerOp = 1e6 * msPerOp;

                repeatResults.Add(nsPerOp);
            }

            // Calculate the average time per op
            double avgNsPerOp = repeatResults.Average();

            _results.Add(name, avgNsPerOp);
        }

        static void ExecuteBenchmarks()
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                {
                    // Only interested in methods defined as benchmarks.
                    if (!System.Attribute.IsDefined(method, typeof(BenchmarkAttribute)))
                    {
                        continue;
                    }

                    // Parameterless methods only.
                    if (method.GetParameters().Length != 0)
                    {
                        continue;
                    }

                    // Does not return a value.
                    if (method.ReturnType != typeof(void))
                    {
                        continue;
                    }

                    var classInstance = Activator.CreateInstance(type);

                    // As we know that all benchmarks have to be parameterless and return void, we can actually get the signature at compile time
                    // We can however support static and non static methods.
                    // This is speedier than letting reflection doing it, otherwise it throws off the benchmarking results.   
                    Action func;
                    if (method.IsStatic)
                    {
                        func = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    }
                    else
                    {
                        func = (Action)Delegate.CreateDelegate(typeof(Action), classInstance, method, true);
                    }

                    BenchmarkAttribute benchmarkAttr = (BenchmarkAttribute)method.GetCustomAttributes(typeof(BenchmarkAttribute), true)[0];

                    // If we've specified a name to use, use that otherwise use the methods own name
                    string name = method.Name;
                    if (!String.IsNullOrEmpty(benchmarkAttr.Name))
                    {
                        name = benchmarkAttr.Name;
                    }

                    // If we've specified an iteration count to use, use that, otherwise use default value.
                    int iterations = 100000;
                    if (benchmarkAttr.Iterations != 0)
                    {
                        iterations = benchmarkAttr.Iterations;
                    }

                    // Go ahead and profile.
                    Profile(name, iterations, func);
                }
            }
        }

        static void Main(string[] args)
        {
            ExecuteBenchmarks();

            foreach (var result in _results)
            {
                Console.WriteLine("{0}: {1} ns", result.Key, result.Value);
            }
        }
    }
}
