//Copyright(c) 2019, Jaeho Kim
//All rights reserved.

//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:

//1. Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//2. Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
//ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//The views and conclusions contained in the software and documentation are those
//of the authors and should not be interpreted as representing official policies,
//either expressed or implied, of the NdArrayNet project.

namespace NdArrayNet
{
    public class NumPy
    {
        private static readonly IDevice Device = HostDevice.Instance;

        public static NdArray<int> Arange(int stop)
        {
            return NdArray<int>.Arange(Device, 0, stop, 1);
        }

        public static NdArray<int> Arange(int start, int stop, int step)
        {
            return NdArray<int>.Arange(Device, start, stop, step);
        }

        public static NdArray<double> Arange(double stop)
        {
            return NdArray<double>.Arange(Device, 0.0, stop, 1.0);
        }

        public static NdArray<double> Arange(double start, double stop, double step)
        {
            return NdArray<double>.Arange(Device, start, stop, step);
        }

        public static NdArray<T> Ones<T>(int[] shape)
        {
            return NdArray<T>.Ones(Device, shape);
        }

        public static NdArray<T> OnesLike<T>(NdArray<T> template)
        {
            return NdArray<T>.Ones(template.Storage.Device, template.Shape);
        }

        public static NdArray<T> Zeros<T>(int[] shape)
        {
            return NdArray<T>.Zeros(Device, shape);
        }

        public static NdArray<T> ZerosLike<T>(NdArray<T> template)
        {
            return NdArray<T>.Zeros(template.Storage.Device, template.Shape);
        }
    }
}