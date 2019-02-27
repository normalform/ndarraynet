// <copyright file="FloatComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the NdArrayNet project.
// </copyright>

namespace NdArray.NdArrayImpl
{
    using NdArrayNet;

    internal sealed class FloatComparison : BaseComparison<float>
    {
        public FloatComparison(IStaticMethod staticMethod) : base(staticMethod)
        {
        }

        public override float One => 1.0f;

        public override float Zero => 0.0f;

        //public bool AlmostEqual(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence, float relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Equal(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillAlmostEqual(bool result, NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence, float relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillEqual(IFrontend<bool> result, IFrontend<float> lhs, IFrontend<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreater(NdArray<bool> result, NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreaterOrEqual(NdArray<bool> result, NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsClose(NdArray<bool> result, NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence, float relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsFinite(NdArray<bool> result, NdArray<float> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLess(NdArray<bool> result, NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLessOrEqual(NdArray<bool> result, NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillNotEqual(IFrontend<bool> result, IFrontend<float> lhs, IFrontend<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Greater(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> GreaterOrEqual(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsClose(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence, float relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithoutTolerence(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithTolerence(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence, float relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsFinite(NdArray<float> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Less(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> LessOrEqual(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> NotEqual(NdArray<float> lhs, NdArray<float> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}