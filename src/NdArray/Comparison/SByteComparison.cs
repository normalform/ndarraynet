// <copyright file="SByteComparison.cs" company="NdArrayNet">
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

    internal sealed class SByteComparison : BaseComparison<sbyte>
    {
        public SByteComparison(IStaticMethod staticMethod) : base(staticMethod)
        {
        }

        public override sbyte One => 1;

        public override sbyte Zero => 0;

        //public bool AlmostEqual(NdArray<sbyte> lhs, NdArray<sbyte> rhs, sbyte absoluteTolerence, sbyte relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Equal(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillAlmostEqual(bool result, NdArray<sbyte> lhs, NdArray<sbyte> rhs, sbyte absoluteTolerence, sbyte relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillEqual(IFrontend<bool> result, IFrontend<sbyte> lhs, IFrontend<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreater(NdArray<bool> result, NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreaterOrEqual(NdArray<bool> result, NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsClose(NdArray<bool> result, NdArray<sbyte> lhs, NdArray<sbyte> rhs, sbyte absoluteTolerence, sbyte relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsFinite(NdArray<bool> result, NdArray<sbyte> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLess(NdArray<bool> result, NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLessOrEqual(NdArray<bool> result, NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillNotEqual(IFrontend<bool> result, IFrontend<sbyte> lhs, IFrontend<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Greater(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> GreaterOrEqual(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsClose(NdArray<sbyte> lhs, NdArray<sbyte> rhs, sbyte absoluteTolerence, sbyte relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithoutTolerence(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithTolerence(NdArray<sbyte> lhs, NdArray<sbyte> rhs, sbyte absoluteTolerence, sbyte relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsFinite(NdArray<sbyte> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Less(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> LessOrEqual(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> NotEqual(NdArray<sbyte> lhs, NdArray<sbyte> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}