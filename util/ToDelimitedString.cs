using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.util
{
    /// <summary>
    /// Helper class for some extension methods to pretty print arrays or colelction
    /// </summary>
    public static class ToDelimitedStringClass
    {
        public static string ToDelimitedString( this object[] source, string delimiter )
        {
            StringBuilder sb = new StringBuilder();

            foreach ( var element in source )
            {
                if ( sb.Length != 0 )
                {
                    sb.Append( delimiter );
                }
                sb.Append( element.ToString() );
            }

            return sb.ToString();
        }
        public static string ToDelimitedString<T>( this ReadOnlyCollection<T> source, string delimiter )
        {
            StringBuilder sb = new StringBuilder();

            foreach ( var element in source )
            {
                if ( sb.Length != 0 )
                {
                    sb.Append( delimiter );
                }
                sb.Append( element.ToString() );
            }

            return sb.ToString();
        }
        public static string ToDelimitedString( this int[] source, string delimiter )
        {
            StringBuilder sb = new StringBuilder();

            foreach ( var element in source )
            {
                if ( sb.Length != 0 )
                {
                    sb.Append( delimiter );
                }
                sb.Append( element.ToString() );
            }

            return sb.ToString();
        }
    }
}
