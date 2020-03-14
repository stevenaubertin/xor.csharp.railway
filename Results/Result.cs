using System;

namespace xor.csharp.railway.Results
{
    public readonly struct Result<TValue, TError>
    {
        public TValue Value { get; }
        public TError Error { get; }
        public bool IsSuccess { get; }

        private Result(TValue success, TError error, bool isSuccess)
        {
            Value = success;
            Error = error;
            IsSuccess = isSuccess;
        }

        public Result(TValue success)
            : this(success, default, true)
        {
        }

        public Result(TError error)
            : this(default, error, false)
        {
        }

        public static implicit operator bool(Result<TValue, TError> result)
            => result.IsSuccess;

        public static implicit operator Result<TValue, TError>(TValue success)
            => new Result<TValue, TError>(success);

        public static implicit operator Result<TValue, TError>(TError error)
            => new Result<TValue, TError>(error);

        public static explicit operator TValue(Result<TValue, TError> result)
            => result.IsSuccess ?
                result.Value :
                throw new InvalidCastException(
                    $"Current {typeof(Result<TValue, TError>)} express {nameof(Error)} and can't be use as a {typeof(TValue)}");

        public static explicit operator TError(Result<TValue, TError> result)
            => !result.IsSuccess ?
                result.Error :
                throw new InvalidCastException(
                    $"Current {typeof(Result<TValue, TError>)} express {nameof(Value)} and can't be use as a {typeof(TError)}");
    }
}
