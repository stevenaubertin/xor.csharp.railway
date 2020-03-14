using System;
using System.Linq;
using xor.csharp.railway.Compositions;
using xor.csharp.railway.Results;

namespace xor.csharp.railway
{
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
                //,input => throw new ArgumentException("DANS MON TEMPS")
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
