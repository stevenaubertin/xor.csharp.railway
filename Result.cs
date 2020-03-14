using System;

namespace xor.csharp.railway
{
    /*public readonly struct Result<TSuccess, TError>
    {
        public TSuccess Success {get;}
        public TError Error { get; }
        public bool IsError { get; }

        private Result(TSuccess ok, TError error, bool isError)
        {
            Success = ok;
            Error = error;
            IsError = isError;
        }

        public Result(TSuccess ok)
            : this(ok, default, false)
        {
        }

        public Result(TError error)
            : this(default, error, true)
        {
        }

        public static implicit operator Result<TSuccess, TError>(DelayedOk<TSuccess> ok) =>
            new Result<TSuccess, TError>(ok.Value);

        public static implicit operator Result<TSuccess, TError>(DelayedError<TError> error) =>
            new Result<TSuccess, TError>(error.Value);
    }

    public readonly struct DelayedOk<T>
    {
        public T Value { get; }

        public DelayedOk(T value)
        {
            Value = value;
        }
    }

    public readonly struct DelayedError<T>
    {
        public T Value { get; }

        public DelayedError(T value)
        {
            Value = value;
        }
    }

    public static class Result
    {
        public static DelayedOk<TOk> Ok<TOk>(TOk ok) =>
            new DelayedOk<TOk>(ok);

        public static DelayedError<TError> Error<TError>(TError error) =>
            new DelayedError<TError>(error);
    }*/
}
