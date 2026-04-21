// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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