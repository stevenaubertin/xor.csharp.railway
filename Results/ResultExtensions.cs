using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xor.csharp.railway.Compositions;

namespace xor.csharp.railway.Results
{
    public static class ResultExtensions
    {
        public static Result<TValue, TError> Bind<TValue, TError>(
            this Result<TValue, TError> input,
            [DisallowNull]Func<TValue, Result<TValue, TError>> switchFunction)
            => Binder.Bind(input, switchFunction);

        public static Result<TValue, TError> Compose<TValue, TError>(
            this TValue input,
            [DisallowNull]params Func<TValue, Result<TValue, TError>>[] functionsToCompose)
                => Compose(new Result<TValue, TError>(input), functionsToCompose);

        public static Result<TValue, TError> Compose<TValue, TError>(
            this Result<TValue, TError> input,
            [DisallowNull]params Func<TValue, Result<TValue, TError>>[] functionsToCompose)
                => functionsToCompose.Aggregate(input, (i, f) => i.Bind(f));
    }
}
