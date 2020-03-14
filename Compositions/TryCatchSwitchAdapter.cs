using System;
using System.Diagnostics.CodeAnalysis;
using xor.csharp.railway.Results;

namespace xor.csharp.railway.Compositions
{
    public static class TryCatchSwitchAdapter
    {
        public static Func<TValue, Result<TValue, TError>> TryCatch<TValue, TError>(
            [DisallowNull]this Action<TValue> func,
            [DisallowNull]Func<Exception, TError> exceptionHandler = null)
            => input => { func(input); return input; };

        public static Func<TValue, Result<TValue, TError>> TryCatch<TValue, TError>(
            [DisallowNull]this Func<TValue> func,
            [DisallowNull]Func<Exception, TError> exceptionHandler = null)
            => input => func();

        public static Func<TValue, Result<TValue, TError>> TryCatch<TValue, TError>(
            [DisallowNull]this Func<TValue, TValue> func,
            [DisallowNull]Func<Exception, TError> exceptionHandler = null)
            => input => func(input);
    }
}
