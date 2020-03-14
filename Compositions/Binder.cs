using System;
using System.Diagnostics.CodeAnalysis;
using xor.csharp.railway.Results;

namespace xor.csharp.railway.Compositions
{
    public static class Binder//ou duo-tang
    {
        public static Result<TValue, TError> Bind<TValue, TError>(
            Result<TValue, TError> input,
            [DisallowNull]Func<TValue, Result<TValue, TError>> switchFunction)
            => input.IsSuccess ?
                switchFunction(input.Value) : // Success we continue
                input.Error;                  // Error we end the flow there
    }
}
