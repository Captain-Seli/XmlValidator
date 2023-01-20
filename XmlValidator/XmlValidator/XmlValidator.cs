using System.Runtime.CompilerServices;

namespace XmlValidator
{
    public class XmlValidator
    {
        /// <summary>
        /// Behavior of this method creates tokens from an XML String for later comparison in DetermineNestOrder()
        /// If something is not tokenized by this method, then the string is not valid XML. Returns null
        /// </summary>
        /// <param name="xml"></param>: The string to be validated as proper XML syntax
        /// <returns></returns>
        public List<string>? Tokenize(string xml)
        {
            List<string> tokens = new List<string>();
            int startIndex = 0;
            int endIndex = 0;

            // Trim off leading and trailing whitespace
            xml = xml.TrimStart(' ');
            xml = xml.TrimEnd(' ');
            // If the first char is not a < then this is not a valid xml string
            if (xml[0] != '<')
            {
                return null;
            }
            // Checks for the proper tag syntax
            while (startIndex < xml.Length)
            {
                // Check that there's an open <
                startIndex = xml.IndexOf("<", endIndex);
                if (startIndex == -1)
                {
                    return null;
                }
                // If there's an open <, check for a matching closed >
                endIndex = xml.IndexOf(">", startIndex);
                if (endIndex == -1) { return null; }
                if (endIndex - startIndex == 1) { return null; }
                // If there are both open < and closed > add the tokens to the list
                tokens.Add(xml.Substring(startIndex, endIndex - startIndex + 1));
                startIndex = endIndex + 1;
            }
            return tokens;
        }

        /// <summary>
        /// This method takes the list of tags from Tokenize() and matches an opening tag to a closed tag.
        /// </summary>
        /// <param name="tokens"></param> The list of strings that will be compared
        /// Each string represents either an open tag, a closed tag, or an invalid tag
        /// <returns></returns>
        public bool DetermineNestOrder(List<string> tokens)
        {
            Stack<string> tagStack = new Stack<string>();
            // If tokens is empty, then there's no valid xml string
            if (tokens != null)
            {
                if (tokens.Count == 0)
                {
                    return false;
                }
                // If there's one tag in the list, check to see that its a self closing tag
                if (tokens.Count == 1) { return SelfClosingCheck(tokens[0]); }

                string lastToken = tokens.Last().Replace("/", "");
                string firstToken = tokens.First();

                // Check for Root tags
                if (firstToken == lastToken)
                {
                    foreach (string token in tokens)
                    {
                        // Push the tokenized strings to the stack only if they are opening tags.
                        if (!(token.Contains("</")) && !(SelfClosingCheck(token)))
                        {
                            tagStack.Push(token);
                        }
                        // When the next element in the list is a closing tag compare it to the last element in the stack. Pop if they match
                        if (token.StartsWith("</"))
                        {
                            // Closing tag but nothing left on Stack
                            if (tagStack.Count() == 0)
                            {
                                return false;
                            }
                            string currentTag = token;
                            currentTag = currentTag.Replace("/", "");
                            Console.WriteLine("currentTag: " + currentTag);
                            Console.WriteLine("Popped: " + tagStack.Peek()); 
                            // If the tags do not match, invalid XML
                            if (currentTag != tagStack.Pop())
                            {
                                return false;
                            }
                        }
                    }
                    if (tagStack.Count > 0) { return false; }
                    else { return true; }
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// This method simply checks if a tag meets the requirements of being a self closing tag such as <img/>
        /// </summary>
        /// <param name="token"></param>: A string that represents a tag
        /// <returns></returns>
        private static bool SelfClosingCheck(string token)
        {
            if (token.StartsWith("<") && token.EndsWith("/>"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
