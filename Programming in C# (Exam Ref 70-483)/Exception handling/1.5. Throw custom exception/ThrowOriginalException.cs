namespace ProgrammingInCSharp.ThrowCustomException
{
    using System;

    public class ThrowOriginalException
    {
        private static void Main()
        {
            var response = ThrowOriginalException.GetParsedResponse();
            Console.WriteLine(response.Length);
        }

        private static string[] GetParsedResponse()
        {
            try
            {
                var response = ThrowOriginalException.ParseResponse(ThrowOriginalException.GetResponse());

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new CustomException("Custom exception was thrown.", ex);
            }
        }

        private static string[] ParseResponse(string response)
        {
            return response.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetResponse()
        {
            var httpResponse = ThrowOriginalException.MakeHttpResponse();

            return httpResponse;
        }

        private static string MakeHttpResponse()
        {
            throw new InvalidOperationException();
        }
    }

    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message)
            : this(message, null)
        {
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}