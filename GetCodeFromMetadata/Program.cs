using AcmeCorp.Training.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GetCodeFromMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            //string pattern = @"([a-zA-Z0-9 ]*)~([a-zA-Z0-9 ]*)_([a-zA-Z0-9 ]*)~([a-zA-Z0-9 ]*)_([a-zA-Z0-9 ]*)~([a-zA-Z0-9 \-]*)\.([a-zA-Z0-9 ]*)";
            //string text = "11977~Iron Pipe_F3M~EL_MQ9~7d115c0a-b1cc-4fba-ac86-8b8d86a4e17e.acm";

            //MatchCollection matches = Regex.Matches(text, pattern);
            //foreach (Match match in matches)
            //{
            //    Console.WriteLine(match.Groups[3].Value);
            //    Console.WriteLine();
            //}

            //Console.ReadKey();

            LegacyObjectMetadataProvider.V1 metadataProviderVersion1 = new LegacyObjectMetadataProvider.V1();
            string metadata = metadataProviderVersion1.ProvideMetadata();
            Console.WriteLine($"Getting product code from {metadata}");
            string code = GetCode(metadata);
            Console.WriteLine($"Recognized code as [{code}]");

            ObjectCodeValidator validator = new ObjectCodeValidator();
            validator.AssertCodeIsValid(code, metadata);

            Console.ReadKey();
        }

        private static string GetCode(string metadata)
        {
            string pattern = @"([a-zA-Z0-9 ]*)~([a-zA-Z0-9 ]*)_([a-zA-Z0-9 ]*)~([a-zA-Z0-9 ]*)_([a-zA-Z0-9 ]*)~([a-zA-Z0-9 \-]*)\.([a-zA-Z0-9 ]*)";

            List<string> _tempRes = new List<string>();

            MatchCollection matches = Regex.Matches(metadata, pattern);

            foreach (Match match in matches)
            {
                _tempRes.Add(match.Groups[3].Value);
            }

            return _tempRes[5];

        }
    }
}