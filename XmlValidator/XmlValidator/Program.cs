using System;
using System.Collections;
using System.Runtime.Serialization;


namespace XmlValidator
{
    public static class XmlString
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a String: ");
            string? userString = Console.ReadLine();
            DetermineXml(userString);
        }

        /// <summary>
        /// This method is the Driver code for the XmlValidator.
        /// The XmlValidator checks the validity of an XML string
        /// 
        /// <param name="xml"></param>: This is the string to be verified as valid XML syntax
        /// <returns></returns>
        /// </summary>
        public static bool DetermineXml(string xml)
        {
            var validator = new XmlValidator();
            List<String>? tokens = validator.Tokenize(xml);
            bool result = validator.DetermineNestOrder(tokens);
            Console.WriteLine(result);
            return result;
        }
    }
}

