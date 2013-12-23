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
            var m = new Matrix4(new float[] { 1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f,
                                              1.0f, 1.0f, 2.0f, 0.0f});            
            m[0, 1] = 1.0f;
            m[3, 2] = 3.0f;

            Console.WriteLine(m);
        }
    }
}
