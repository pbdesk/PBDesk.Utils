/*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*
 *~*  www.PBDesk.com 2010/2011                                                      
 *~*  
 *~*  Project : PBDesk.Utils                        Namespace : PBDesk.Utils
 *~*  File : EnumUtils.cs                               Class : EnumUtils
 *~*  
 *~*  Author : Pinal Bhatt (pinal.bhatt@PBDesk.com)
 *~*  Version History
  *~*            Ver. 1.0     Date 21-Jun-2011       Pinal Bhatt
 *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/

using System;

namespace PBDesk.Utils
{
    public static class EnumUtils
    {
        #region Enum Methods

        #region String to Enum
        public static T ParseEnum<T>(string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(inString, default(T), ignoreCase, throwException);
        }

        public static T ParseEnum<T>(string inString, T defaultValue,
                               bool ignoreCase = true, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;

            if (!typeof(T).IsEnum || inString.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("Invalid Enum Type or Input String 'inStr'. " + typeof(T).ToString() + "  must be an Enum");
            }

            try
            {
                //bool success = Enum.TryParse<T>(inString, ignoreCase, out returnEnum);
                bool success = false;
                try
                {
                    returnEnum = (T)Enum.Parse(typeof(T), inString, ignoreCase);
                    success = true;
                }
                catch
                {
                    success = false;
                }


                if (!success && throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid Cast", ex);
            }

            return returnEnum;
        }
        #endregion

        #region Int to Enum
        public static T ParseEnum<T>(int input, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(input, default(T), throwException);
        }
        public static T ParseEnum<T>(int input, T defaultValue, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Invalid Enum Type. " + typeof(T).ToString() + "  must be an Enum");
            }
            if (Enum.IsDefined(typeof(T), input))
            {
                returnEnum = (T)Enum.ToObject(typeof(T), input);
            }
            else
            {
                if (throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }

            return returnEnum;

        }
        #endregion        

        #endregion

        #region Int Extension Methods for Enum Parsing
        public static T ToEnum<T>(this int input, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(input, default(T), throwException);
        }

        public static T ToEnum<T>(this int input, T defaultValue, bool throwException = false) where T : struct
        {
            return (T)ParseEnum<T>(input, defaultValue, throwException);
        }
        #endregion
    }
}
