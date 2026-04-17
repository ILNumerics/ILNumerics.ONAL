using System;
using System.Diagnostics;

namespace ILNumerics.Core.Misc {
    internal class NRandom {
        private double m_spareRand = 0; 
        private bool m_hasSpareRand = false; 
        private Random m_rand; 

        public NRandom (int seed) {
            m_rand = new Random(seed); 
        }
        public NRandom() {
            m_rand = new Random((int)Stopwatch.GetTimestamp()); 
        }

        public double NextDouble() {
            if (m_hasSpareRand) {
                m_hasSpareRand = false; 
                return m_spareRand; 
            } else {
                double v1 = 0.0, v2, rsq = 0.0, fac = 0.0;  
                do {
                    v1 = 2.0 * m_rand.NextDouble() -1.0; 
                    v2 = 2.0 * m_rand.NextDouble() -1.0; 
                    rsq = v1 * v1 + v2 * v2; 
                } while (rsq >= 1.0 || rsq == 0.0); 
                fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq); 
                m_spareRand = v1 * fac; 
                m_hasSpareRand = true; 
                return v2 * fac; 
            }
        }
        
    }
}