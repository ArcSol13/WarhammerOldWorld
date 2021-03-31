using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerOldWorld.Utility
{
    public static class TowMath
    {
        private static Random rng = new Random();
        private static readonly object syncLock = new object();

        /// <summary>
        /// Get a random double between min and max, inclusive.
        /// </summary>
        /// <param name="min">The lower bound of the returned double</param>
        /// <param name="max">The upper bound of the returned double</param>
        /// <returns>A random double between min and max, inclusive</returns>
        public static double GetRandomDouble(double min, double max)
        {
            lock(syncLock)
            {
                return (rng.NextDouble() * max) + min;
            }
        }
    }
}
