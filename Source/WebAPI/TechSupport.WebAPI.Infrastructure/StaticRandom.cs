using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechSupport.WebAPI.Infrastructure
{
    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }

    //How to use
    //static string GenerateRandomNumber(int count)
    //{
    //    StringBuilder builder = new StringBuilder();

    //    for (int i = 0; i < count; i++)
    //    {
    //        int number = StaticRandom.Instance.Next(10);
    //        builder.Append(number);
    //    }

    //    return builder.ToString();
    //}
}
