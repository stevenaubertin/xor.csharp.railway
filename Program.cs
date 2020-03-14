using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace xor.csharp.railway
{
    #region other possible implementation
    //public abstract class Result<TSuccess, TFailure>
    //{
    //    public static Result<TSuccess, TFailure> Success(TSuccess input)
    //        => new Success<TSuccess, TFailure>(input);

    //    public static Result<TSuccess, TFailure> Error(TFailure input)
    //        => new Failure<TSuccess, TFailure>(input);
    //}

    //public class Success<TSuccess, TFailure> : Result<TSuccess, TFailure>
    //{
    //    public Success(TSuccess value)
    //        => Value = value;

    //    public TSuccess Value { get; }
    //}

    //public class Failure<TSuccess, TFailure> : Result<TSuccess, TFailure>
    //{
    //    public Failure(TFailure value)
    //        => Value = value;

    //    public TFailure Value { get; }
    //}
    //public class Result : Result<Request, string> { }
    #endregion

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

        public static explicit operator TValue (Result<TValue, TError> result)
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

    public static class Binder//ou duo-tang
    {
        public static Result<TValue, TError> Bind<TValue, TError>(
            Result<TValue, TError> input,
            [DisallowNull]Func<TValue, Result<TValue, TError>> switchFunction)
            => input.IsSuccess ?
                switchFunction(input.Value) : // Success we continue
                input.Error;                  // Error we end the flow there
    }

    public static class SwitchAdapter
    {
        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>([DisallowNull]this Action<TValue> func)
            => input => { func(input); return input; };

        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>([DisallowNull]this Func<TValue> func)
            => input => func();

        public static Func<TValue, Result<TValue, TError>> ToSwitch<TValue, TError>([DisallowNull]this Func<TValue, TValue> func)
            => input => func(input);
    }

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

    public class Request
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    class Program
    {
        public static Result<Request, string> ValidateName(Request input)
        {
            if (string.IsNullOrEmpty(input.Name)) return "Name must not be blank";
            return input;
        }

        public static Result<Request, string> ValidateLength(Request input)
        {
            if (input.Name.Length > 50) return "Name must not be over 50 chars";
            return input;
        }

        public static Result<Request, string> ValidateEmail(Request input)
        {
            if (string.IsNullOrEmpty(input.Email)) return "Email must not be blank";
            return input;
        }

        public static Request ToUpper(Request input)
            => new Request { Name = input.Name?.ToUpper(), Email = input.Email?.ToUpper() };

        static void Main(string[] args)
        {
            var request = new Request { Name = "Steven Aubertin", Email = "stevenaubertin@gmail.com" };
            //var request = new Request { Name = new string('1', 70), Email = "stevenaubertin@gmail.com" };

            //var result = ValidateName(request)
            //    .Bind(ValidateLength)
            //    .Bind(ValidateEmail);

            //var result = ValidateName(request)
            //    .Compose(ValidateLength, ValidateEmail);

            var result = request.Compose(
                input => input is null ? "Input value can't be null" : (Result<Request, string>)input,
                ValidateName,
                ValidateLength,
                ValidateEmail,
                input => new Request //Supports labmda
                {
                    Email = input.Email,
                    Name = string.Join("", input.Name.Select(x => x switch
                    {
                        'a' => '4',
                        'e' => '3',
                        't' => '7',
                        'l' => '1',
                        's' => '5',
                        _ => x
                    }).ToArray())
                },
                SwitchAdapter.ToSwitch<Request, string>(ToUpper)// Adapts non-switch functions
                //,input => "Error types is still supported uncomment to return an error"
                ,input => throw new ArgumentException("DANS MON TEMPS")
            );

            if (!result) Console.WriteLine(result.Error);
            else
            {
                var value = result.Value;
                Console.WriteLine(value.Name);
                Console.WriteLine(value.Email);
            }
        }
    }
}
