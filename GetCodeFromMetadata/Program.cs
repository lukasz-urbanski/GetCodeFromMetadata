using AcmeCorp.Training.Services;
using System;
using System.Text.RegularExpressions;

namespace GetCodeFromMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            LegacyObjectMetadataProvider.V1 metadataProviderVersion1 = new LegacyObjectMetadataProvider.V1();
            string metadata1 = metadataProviderVersion1.ProvideMetadata();

            LegacyObjectMetadataProvider.V2 metadataProviderVersion2 = new LegacyObjectMetadataProvider.V2();
            string metadata2 = metadataProviderVersion2.ProvideMetadata();

            LegacyObjectMetadataProvider.V3 metadataProviderVersion3 = new LegacyObjectMetadataProvider.V3();
            string metadata3 = metadataProviderVersion3.ProvideMetadata();

            LegacyObjectMetadataProvider.V4 metadataProviderVersion4 = new LegacyObjectMetadataProvider.V4();
            string metadata4 = metadataProviderVersion4.ProvideMetadata();

            LegacyObjectMetadataProvider.V5 metadataProviderVersion5 = new LegacyObjectMetadataProvider.V5();
            string metadata5 = metadataProviderVersion5.ProvideMetadata();

            LegacyObjectMetadataProvider.V6 metadataProviderVersion6 = new LegacyObjectMetadataProvider.V6();
            string metadata6 = metadataProviderVersion6.ProvideMetadata();

            LegacyObjectMetadataProvider.V7 metadataProviderVersion7 = new LegacyObjectMetadataProvider.V7();
            string metadata7 = metadataProviderVersion7.ProvideMetadata();

            string[] allStrings = new string[] {
                metadata1, metadata2, metadata3, metadata4, metadata5, metadata6, metadata7
            };

            for (int i = 0; i < 7; i++)
            {
                string metadata = allStrings[i];
                Console.WriteLine($"Getting product code from:{Environment.NewLine}{metadata}");
                string code = GetCode(allStrings[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Recognized code as [{code}]");
                Console.ForegroundColor = ConsoleColor.Gray;

                ObjectCodeValidator validator = new ObjectCodeValidator();
                validator.AssertCodeIsValid(code, metadata);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;            
            Console.WriteLine("Everything works. Cool!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Press any button to exit...");
            Console.ReadKey();
        }
        private static string GetCode(string metadata)
        {
            if (!(metadata.StartsWith("<") || metadata.StartsWith("<")))
            {
                string[] arrayToBeChecked = metadata.Split(new char[] { '~', '_' });
                switch (arrayToBeChecked.Length)
                {
                    case (6):
                        return arrayToBeChecked[4];
                    case (7):
                        return arrayToBeChecked[5];
                    case (9):
                        if (arrayToBeChecked[5].Substring(0, 1).Equals("v"))
                        {
                            return arrayToBeChecked[7];
                        }
                        else
                        {
                            return arrayToBeChecked[5];
                        }
                    case (10):
                        return arrayToBeChecked[6];
                }
            }
            if (metadata.StartsWith("<Object>"))
            {
                if (metadata.Contains("<Code>"))
                {
                    //ReturnCodeFromMetadataBasedOnPattern(metadata, @"(.*)(<Code>)(.*)(</Code>)(.*)");
                    string patternCode = @"(.*)(<Code>)(.*)(<\/Code>)(.*)";
                    string patternMarket = @"(.*)(<Market>)(.*)(<\/Market>)(.*)";
                    MatchCollection matchesCode = Regex.Matches(metadata, patternCode);
                    MatchCollection matchesMarket = Regex.Matches(metadata, patternMarket);
                    foreach (Match matchM in matchesMarket)
                    {
                        if (matchM.Groups[3].Value.Equals("PL") || matchM.Groups[3].Value.Equals("BG") || matchM.Groups[3].Value.Equals("EL"))
                        {
                            foreach (Match matchC in matchesCode)
                            {
                                return matchC.Groups[3].Value.Substring(0, matchC.Groups[3].Value.Length - 2);
                            }
                        }
                        else
                        {
                            foreach (Match matchC in matchesCode)
                            {
                                return matchC.Groups[3].Value;
                            }
                        }
                    }
                }
                else
                {
                    //ReturnCodeFromMetadataBasedOnPattern(metadata, @"(.*)(_v\d~)(.*?(?=~))(.*)");
                    string pattern = @"(.*)(_v\d~)(.*?(?=~))(.*)";
                    MatchCollection matches = Regex.Matches(metadata, pattern);
                    foreach (Match match in matches)
                    {
                        return match.Groups[3].Value;
                    }
                }
            }
            else
            {
                //ReturnCodeFromMetadataBasedOnPattern(metadata, @"(.*)(_v\d~)(.*?(?=~))(.*)");
                string pattern = @"(.*)(_v\d~)(.*?(?=~))(.*)";
                MatchCollection matches = Regex.Matches(metadata, pattern);
                foreach (Match match in matches)
                {
                    return match.Groups[3].Value;
                }
            }
            return "ERROR";
        }
        //Have tried to implement method below to follow DRY rule but it didn't work...
        private static string ReturnCodeFromMetadataBasedOnPattern(string metadata, string pattern)
        {
            MatchCollection matches = Regex.Matches(metadata, pattern);
            foreach (Match match in matches)
            {
                return match.Groups[3].Value;
            }
            return "";
        }
    }
}