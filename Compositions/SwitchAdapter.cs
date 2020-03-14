using System;
using System.Diagnostics.CodeAnalysis;
using xor.csharp.railway.Results;

namespace xor.csharp.railway.Compositions
{
    public static class SwitchAdapter
    {
        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>(
            [DisallowNull]this Action<TValue> func)
            => input => { func(input); return input; };

        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>(
            [DisallowNull]this Func<TValue> func)
            => input => func();

        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>(
            [DisallowNull]this Func<TValue, TValue> func)
            => input => func(input);
    }
}
