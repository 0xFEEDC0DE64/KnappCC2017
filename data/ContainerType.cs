using com.knapp.KCC2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.data
{
    /// <summary>
    /// Represents the base type for the type of a container type
    /// The Types can be 
    /// ContainerType.Full
    /// ContainerType.Quarter
    /// ContainerType.Half
    /// 
    /// This is a .NET implementation of enums with data as they exist in Jaba
    /// </summary>
    public class ContainerType : EnumBase<ContainerType>
    {

        public static readonly ContainerType Full = new ContainerType( "Full" , 1 );
        public static readonly ContainerType Half = new ContainerType("Half", 2);
        public static readonly ContainerType Quarter = new ContainerType("Quarter", 4);

        /// <summary>
        /// The strign identifying this 'enum'
        /// </summary>
        public string Token { get; protected set; }

        /// <summary>
        /// Gets the ordinal of the enum
        /// The order in which they have been declared in the deriving class
        /// </summary>
        public int Ordinal { get; protected set; }

        /// <summary>
        /// The number of slots available in a container of this type
        /// </summary>
        public int NumberOfSlots { get; protected set; }

        public override string ToString()
        {
            return Token;
        }
        public override int GetHashCode()
        {
            return Token.GetHashCode();
        }

        /// <summary>
        /// Get the number of different types that have been declared
        /// </summary>
        public static int Count
        {
            get
            {
                return values.Count;
            }
        }

        /// <summary>
        /// Get all declared types
        /// </summary>
        public static IEnumerable<ContainerType> Values
        {
            get
            {
                return values;
            }
        }

        public static ContainerType Get( string token )
        {
            return values.FirstOrDefault( r => r.Token.Equals( token, StringComparison.InvariantCulture ) );
        }



        /// <summary>
        /// Equality operation
        /// Equals when the other ContainerType is not null and the token (Full, Half, Quarter) matches
        /// </summary>
        /// <param name="obj">object to compare with</param>
        /// <returns>true when obj is a Product and has the same code, false in any other case</returns>
        public override bool Equals(object obj)
        {
            ContainerType other = obj as ContainerType;

            return other != null
                && this.Token == other.Token;
        }



        private ContainerType( string token, int numberOfSlots )
        {
            Token = token;
            NumberOfSlots = numberOfSlots;

            Add(this);
            Ordinal = values.Count;
        }

        /// <summary>
        /// Add a concrete type to the List
        /// </summary>
        /// <param name="value"></param>
        private static void Add( ContainerType value )
        {
            values.Add( value );
        }
    }

    public abstract class EnumBase<T>
    {
        /// <summary>
        /// store all declared derived types of T
        /// (all declared 'enums')
        /// </summary>
        protected readonly static List<T> values = new List<T>();
    }
}
