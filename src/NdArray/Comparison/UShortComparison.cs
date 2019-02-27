// <copyright file="UShortComparison.cs" company="NdArrayNet">
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

    internal sealed class UShortComparison : BaseComparison<ushort>
    {
        public UShortComparison(IStaticMethod staticMethod) : base(staticMethod)
        {
        }

        public override ushort One => 1;

        public override ushort Zero => 0;

        //public bool AlmostEqual(NdArray<ushort> lhs, NdArray<ushort> rhs, ushort absoluteTolerence, ushort relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Equal(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillAlmostEqual(bool result, NdArray<ushort> lhs, NdArray<ushort> rhs, ushort absoluteTolerence, ushort relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillEqual(IFrontend<bool> result, IFrontend<ushort> lhs, IFrontend<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreater(NdArray<bool> result, NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillGreaterOrEqual(NdArray<bool> result, NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsClose(NdArray<bool> result, NdArray<ushort> lhs, NdArray<ushort> rhs, ushort absoluteTolerence, ushort relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillIsFinite(NdArray<bool> result, NdArray<ushort> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLess(NdArray<bool> result, NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillLessOrEqual(NdArray<bool> result, NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void FillNotEqual(IFrontend<bool> result, IFrontend<ushort> lhs, IFrontend<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Greater(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> GreaterOrEqual(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsClose(NdArray<ushort> lhs, NdArray<ushort> rhs, ushort absoluteTolerence, ushort relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithoutTolerence(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsCloseWithTolerence(NdArray<ushort> lhs, NdArray<ushort> rhs, ushort absoluteTolerence, ushort relativeTolerence)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> IsFinite(NdArray<ushort> source)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> Less(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> LessOrEqual(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public NdArray<bool> NotEqual(NdArray<ushort> lhs, NdArray<ushort> rhs)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}