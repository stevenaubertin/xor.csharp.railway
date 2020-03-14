using System;

namespace xor.csharp.railway.Options
{
    public readonly struct Option<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        private Option(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public Option(T value)
            : this(value, true)
        {
        }

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) =>
            _hasValue ? some(_value) : none();

        public void Match(Action<T> some, Action none)
        {
            if (_hasValue)
                some(_value);
            else
                none();
        }

        public Option<TOut> Select<TOut>(Func<T, TOut> map) =>
            _hasValue ? new Option<TOut>(map(_value)) : new Option<TOut>();

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bind) =>
            _hasValue ? bind(_value) : new Option<TOut>();

        public static implicit operator Option<T>(NoneOption none) => new Option<T>();
    }

    public readonly struct NoneOption
    {
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value) => new Option<T>(value);

        public static NoneOption None { get; } = new NoneOption();
    }
}
