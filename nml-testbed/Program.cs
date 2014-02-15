using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nml;

namespace nml_testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Matrix4x4(new float[] { 1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f});

            var r = m * m;

            var vecA = new Vector2(0, 5);
            var vecB = new Vector2(5, 0);

            var vecC = vecA - vecB;

            Console.WriteLine(Vector2.Distance(vecA, vecB));
            Console.WriteLine(vecC);
            Console.WriteLine(vecC.Length);
        }
    }
}
