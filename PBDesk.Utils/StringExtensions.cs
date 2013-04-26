/*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*
 *~*  www.PBDesk.com 2010/2011                                                      
 *~*  
 *~*  Project : PBDesk.Utils                        Namespace : PBDesk.Utils
 *~*  File : StringExtensionMethods.cs                  Class : StringExtensionMethods
 *~*  
 *~*  Author : Pinal Bhatt (pinal.bhatt@PBDesk.com)
 *~*  Version History
 *~*            Ver. 1.0     Date 12-Oct-2010       Pinal Bhatt
 *~*            Ver. 2.0     Date 21-Jun-2011       Pinal Bhatt
 *~*            Ver. 3.0     Date 05-Nov-2012       Pinal Bhatt - Converting to Portable Class Library
 *~*            Ver. 4.0     Date 18-Apr-2013       Pinal Bhatt - Removed from Portable Class Library 
 *~*                                                                and upgraded to .Net 4.5      
 *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PBDesk.Utils
{
    /// <summary>
    /// Static Class containing Extension Methods for strings.
    /// Extending System.String functionalities
    /// </summary>
    public static class StringExtensions
    {
        #region Null Empty Extensions for String
        /// <summary>
        /// Indicates whether invoking string object is null or not.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>True, if string is null else returns false</returns>
        public static bool IsNull(this string inputString)
        {
            return (inputString == null);
        }

        /// <summary>
        /// Indicates whether invoking string object is null or not.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>True, if string is not null else returns false</returns>
        public static bool IsNotNull(this string inputString)
        {
            return (inputString != null);
        }

        /// <summary>
        /// Indicates whether the specified string is Null/Nothing or an Empty string.
        /// </summary>
        /// <param name="originalStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string inputstr)
        {
            return string.IsNullOrEmpty(inputstr);
        }

        /// <summary>
        /// Indicates whether invoking string object is not null and not an empty string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool IsNotNullAndNotEmpty(this string inputString)
        {
            return !string.IsNullOrEmpty(inputString);
        }

        /// <summary>
        /// Indicates whether the specified string is Null/Nothing or an Empty string or an white space only.
        /// </summary>
        /// <param name="originalStr"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string inputstr)
        {
            return string.IsNullOrWhiteSpace(inputstr);
        }

        /// <summary>
        /// Indicates whether the specified string is not Null/Nothing and is not an Empty string and is not an white space only.
        /// </summary>
        /// <param name="originalStr"></param>
        /// <returns></returns>
        public static bool IsNotNullAndNotWhiteSpace(this string inputstr)
        {
            return !string.IsNullOrWhiteSpace(inputstr);
        }

        #endregion

        #region Enum String Utils

        public static T ToEnum<T>(this string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(inString, ignoreCase, throwException);
        }
        public static T ToEnum<T>(this string inString, T defaultValue, bool ignoreCase = true, bool throwException = false) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(inString, defaultValue, ignoreCase, throwException);
        }

        #endregion

        #region Other Util Ext. Methods
        /// <summary>
        /// Get Boolean Equivalent string. 
        /// </summary>
        /// <param name="boolStrIn"></param>
        /// <returns>Returns True for {Y, True, 1, T, Yes} else returns False</returns>
        public static bool GetBoolEquivalent(this string boolStrIn)
        {
            bool returnVal = false;

            if (!boolStrIn.IsNullOrEmpty())
            {
                switch (boolStrIn.Trim().ToUpper())
                {
                    case "Y":
                    case "TRUE":
                    case "1":
                    case "T":
                    case "YES":
                        {
                            returnVal = true;
                            break;
                        }
                }
            }

            return returnVal;
        }

        /// <summary>
        /// Indicates whether the regular expression finds a match in the input string.
        /// </summary>
        /// <param name="originalStr"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsRegexMatch(this string originalStr, string pattern)
        {
            return Regex.IsMatch(originalStr, pattern);
        }

        #endregion

        #region "As" Conversion Methods

        /// <summary>
        /// Converts invoking string object to Type T and returns defaultValue if conversion fails
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T As<T>(this string strValue, T defaultValue)
        {
            if (defaultValue == null)
            {
                defaultValue = default(T);
            }
            T output = defaultValue;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    output = (T)converter.ConvertFromString(strValue);
                }
                catch
                {
                    output = defaultValue;
                }
            }
            return output;
        }

        /// <summary>
        /// Converts invoking string object to Type T and throws exception if conversion fails
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static T As<T>(this string strValue)
        {
            //if (typeof(T) is string)
            //{
            //}
            //if (strValue.IsNullOrWhiteSpace())
            //{
            //    throw new NotSupportedException("Error in String.As(). Conversion of null, empty or white space is not supported.");
            //}
            T output = default(T);
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    output = (T)converter.ConvertFromString(strValue);
                }
                catch (NotSupportedException ex1)
                {
                    throw ex1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in String.As() extension Method. Invalid Conversion.", ex);
                }
            }
            else
            {
                throw new NotSupportedException("Error in String.As(). Converter for specified type T is not available.");
            }
            return output;
        }

        /// <summary>
        /// Tries converting invoking string object to type T and returns true if success else returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse<T>(this string strValue, out T result)
        {
            bool success = false;
            result = default(T);
            try
            {
                result = strValue.As<T>();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        #endregion

    }
}
