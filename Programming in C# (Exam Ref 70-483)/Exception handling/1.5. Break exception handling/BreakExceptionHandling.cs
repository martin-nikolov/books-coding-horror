namespace ProgrammingInCSharp.BreakExceptionHandling
{
    using System;

    public class BreakExceptionHandling
    {
        private static void Main()
        {
            var response = BreakExceptionHandling.GetParsedResponse();
            Console.WriteLine(response.Length);
        }

        private static string[] GetParsedResponse()
        {
            try
            {
                var response = BreakExceptionHandling.ParseResponse(BreakExceptionHandling.GetResponse());

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
                //throw new Exception();
            }
        }

        private static string[] ParseResponse(string response)
        {
            return response.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetResponse()
        {
            var httpResponse = BreakExceptionHandling.MakeHttpResponse();

            return httpResponse;
        }

        private static string MakeHttpResponse()
        {
            throw new InvalidOperationException();
        }
    }
}