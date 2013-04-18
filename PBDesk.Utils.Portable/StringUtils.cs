/*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*
 *~*  www.PBDesk.com 2010/2011                                                      
 *~*  
 *~*  Project : PBDesk.Utils                        Namespace : PBDesk.Utils
 *~*  File : StringUtils.cs                             Class : StringUtils
 *~*  
 *~*  Author : Pinal Bhatt (pinal.bhatt@PBDesk.com)
 *~*  Version History
 *~*            Ver. 1.0     Date 12-Oct-2010       Pinal Bhatt
 *~*            Ver. 2.0     Date 21-Jun-2011       Pinal Bhatt
 *~*            Ver. 3.0     Date 05-Nov-2012       Pinal Bhatt - Converting to Portable Class Library
 *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PBDesk.Utils
{
    /// <summary>
    /// String Utilities.
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// Removes HTML tags from original string and returns it
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string StripHtml(string original)
        {
            return Regex.Replace(original, @"<(.|\n)*?>", string.Empty);
        }


        /// <summary>
        /// Removes multiple spaces between words
        /// </summary>
        /// <param name="input">The string to trim.</param>
        /// <returns>A string.</returns>
        public static string RemoveExtraSpaces(string input)
        {
            Regex regEx = new Regex(@"[\s]+");
            return regEx.Replace(input, " ");
        }

        /// <summary>
        /// Converts new line(\n) and carriage return(\r) symbols to
        /// HTML line breaks.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string.</returns>
        public static string NewLineToHtmlBreak(string input)
        {
            Regex regEx = new Regex(@"[\n|\r]+");
            return regEx.Replace(input, "<br />");
        }

        /// <summary>
        /// Converts all spaces to HTML non-breaking spaces
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string</returns>
        public static string SpaceToNbsp(string input)
        {
            string space = "&nbsp;";
            return input.Replace(" ", space);
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <returns>A string</returns>
        public static string RemoveLineBreaks(string input)
        {
            return RemoveLineBreaks(input, false);
        }

        /// <summary>
        /// SubstringFromLastIndexOf
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static string SubstringFromLastIndexOf(string inStr, string sep)
        {
            if (inStr.IsNotNullAndNotEmpty())
            {
                if (sep.IsNotNullAndNotEmpty() && inStr.LastIndexOf(sep) > 0)
                    return inStr.Substring(inStr.LastIndexOf(sep));
            }
            return inStr;
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <param name="addSpace">If true, adds a space (" ") for each newline and carriage
        /// return found.</param>
        /// <returns>A string</returns>
        public static string RemoveLineBreaks(string input, bool addSpace)
        {
            string replace = string.Empty;
            if (addSpace)
                replace = " ";

            string pattern = @"[\r|\n]";
            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return regEx.Replace(input, replace);
        }


        /// <summary>
        /// Returns an empty string if input string is null.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string IfNullThenEmpty(string inputString)
        {
            return inputString ?? string.Empty;
        }

        /// <summary>
        /// Returns true if invoking string object is in valid EMail Address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEMailID(string email)
        {
            return RegexIsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        /// <summary>
        /// Returns true if input url string is in valid http URL pattern.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {
            return RegexIsMatch(url, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(Uri url)
        {
            if (url != null)
                return IsValidUrl(url.AbsoluteUri);
            else
                return false;
        }

        /// <summary>
        /// Returns true if original string matches with regex pattern
        /// </summary>
        /// <param name="original"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool RegexIsMatch(string original, string regex)
        {
            return Regex.IsMatch(original, regex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string TrimTrailingDelimiter(string original, string delimiter)
        {
            string returnValue = original;
            if (original.IsNotNullAndNotEmpty() && delimiter.IsNotNullAndNotEmpty())
            {
                if (original.EndsWith(delimiter))
                {
                    returnValue = returnValue.Substring(0, returnValue.Length - delimiter.Length).Trim();
                }
            }
            return returnValue;
        }

        

        #region URL Encode/Decode Methods

        /// <summary>
        /// UrlEncodes a string without the requirement for System.Web
        /// </summary>
        /// <param name="text">The string to encode</param>
        /// <returns></returns>
        public static string UrlEncode(string text)
        {
            return System.Uri.EscapeDataString(text);
        }

        /// <summary>
        /// UrlDecodes a string without requiring System.Web
        /// </summary>
        /// <param name="text">String to decode.</param>
        /// <returns>decoded string</returns>
        public static string UrlDecode(string text)
        {
            // pre-process for + sign space formatting since System.Uri doesn't handle it
            // plus literals are encoded as %2b normally so this should be safe
            text = text.Replace("+", " ");
            return System.Uri.UnescapeDataString(text);
        }

        /// <summary>
        /// Retrieves a value by key from a UrlEncoded string.
        /// </summary>
        /// <param name="urlEncoded">UrlEncoded String</param>
        /// <param name="key">Key to retrieve value for</param>
        /// <returns>returns the value or "" if the key is not found or the value is blank</returns>
        public static string GetValueFromUrlEncodedByKey(string urlEncoded, string key)
        {
            urlEncoded = "&" + urlEncoded + "&";

            int Index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);
            if (Index < 0)
                return "";

            int lnStart = Index + 2 + key.Length;

            int Index2 = urlEncoded.IndexOf("&", lnStart);
            if (Index2 < 0)
                return "";

            return UrlDecode(urlEncoded.Substring(lnStart, Index2 - lnStart));
        }
        #endregion

        #region HTML Encode/Decode Methods.
        public static string HtmlEncode(string text)
        {
            return null;
        }

        public static string HtmlDecode(string text)
        {
            return null;
        }
        #endregion
        

        public static bool AreNullOrEmpty(params string[] strings)
        {
            bool result = false;
            if (strings != null && strings.Length > 0)
            {
                result = true;
                var s = (from string str in strings where str.IsNotNullAndNotEmpty() select str).ToArray<string>();
                if (s != null)
                    if (s.Length == strings.Length)
                    {
                        result = false;
                    }
            }
            return result;
        }

        public static bool AreNotNullAndNotEmpty(params string[] strings)
        {
            bool result = false;
            if (strings != null && strings.Length > 0)
            {
                result = !AreNullOrEmpty(strings);
            }
            return result;
        }

        public static string[] GetStringArrFromCSV(string csvString)
        {
            return csvString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitCSV(string csvString)
        {
            if (string.IsNullOrWhiteSpace(csvString))
            {
                csvString = string.Empty;
                return new List<string>();
            }
            else            
            {                
                return (from p in csvString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) select p.Trim());                                
            }
            
        }

        #region MD5 functionality

        /// <summary>
        /// MD5 encodes the passed string
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string MD5String(string input)
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Create a new instance of the MD5CryptoServiceProvider object.
            using (MD5 md5Hasher = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Verified a string against the passed MD5 hash.
        /// </summary>
        /// <param name="input">The string to compare.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>True if the input and the hash are the same, false otherwise.</returns>
        public static bool MD5VerifyString(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = MD5String(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Base64 Encode Decode

        /// <summary>
        /// Base64 encodes a string.
        /// </summary>
        /// <param name="input">A string</param>
        /// <returns>A base64 encoded string</returns>
        public static string Base64StringEncode(string input)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64 decodes a string.
        /// </summary>
        /// <param name="input">A base64 encoded string</param>
        /// <returns>A decoded string</returns>
        public static string Base64StringDecode(string input)
        {
            byte[] decbuff = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        #endregion

        public static Hashtable GetHashtableFromDelimatedString(string originalString, string pairDelimiter, string keyValueDelimiter)
        {
            Hashtable htable = null;
            if (StringUtils.AreNotNullAndNotEmpty(originalString, pairDelimiter, keyValueDelimiter))
            {
                string[] pairDelimiterArr = new string[1];
                pairDelimiterArr[0] = pairDelimiter;

                string[] keyValueDelimiterArr = new string[1];
                keyValueDelimiterArr[0] = keyValueDelimiter;

                string[] pairs = originalString.Split(pairDelimiterArr, StringSplitOptions.RemoveEmptyEntries);
                if (pairs != null && pairs.Length > 0)
                {
                    htable = new Hashtable();
                    foreach (string pair in pairs)
                    {
                        string[] keyValue = pair.Split(keyValueDelimiterArr, StringSplitOptions.RemoveEmptyEntries);
                        if (keyValue != null && keyValue.Length == 2)
                        {
                            htable.Add(keyValue[0], keyValue[1]);
                        }

                    }
                }


            }
            return htable;
        }
        

    }
}
