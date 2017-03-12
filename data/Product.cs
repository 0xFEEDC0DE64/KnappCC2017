using com.knapp.KCC2017.util;
using com.knapp.KCC2017.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.knapp.KCC2017.entities
{
    /// <summary>
    /// A product that is handled in a warehouse
    /// </summary>
    public class Product
    {
        /// <summary>
        /// store for the max quanitites for each slot type
        /// </summary>
        private readonly Dictionary<ContainerType, int> maxQuantities = new Dictionary<ContainerType, int>();

        
        /// <summary>
        /// Unique code of this product
        /// </summary>
        public string Code { get; private set; }


        /// <summary>
        /// Get stringified representation of this instance
        /// </summary>
        /// <returns>strign with infos for this instance</returns>
        public override string ToString( )
        {
            return string.Format("Product[ code={0}, maxQuantities={1}]"
                                , Code
                                , CollapseMaxQuantites()
                                ); ;
        }

        /// <summary>
        /// Create a product from the given data
        /// </summary>
        /// <param name="dataAsArray"></param>
        public Product( string[] dataAsArray )
        {
            KContract.Requires( dataAsArray != null, "dataAsArray mandatory but is null" );
            KContract.Requires( dataAsArray.Length >= 3, "dataAsArray must contain at least 3 elements  :" + dataAsArray[0]   );
            KContract.Requires<ArgumentException>( !string.IsNullOrWhiteSpace( dataAsArray[ 0 ] ), "Code must not be null @ offset 0" );

            Code = dataAsArray[ 0 ].Trim( );

            foreach( ContainerType t in ContainerType.Values )
            {
                maxQuantities.Add( t, 0 );
            }

            for ( int i = 1; i < dataAsArray.Length; i  += 2 )
            {
                ContainerType t = ContainerType.Get(dataAsArray[i]);
                if ( default(ContainerType) == t  )
                {
                    throw new ArgumentException( string.Format( "Unknonwn ContainerType '{0}' for product {1}", dataAsArray[i], Code ) );
                }

                maxQuantities[t] = int.Parse(dataAsArray[i + 1]);
            }

            if( maxQuantities.Count != 3 )
            {
                throw new InvalidOperationException("to few quantitites");
            }
        }

        /// <summary>
        /// Get the number of max. allowed items for the given slot/ container type
        /// </summary>
        /// <param name="containerType">the type for which to return the max. number of items</param>
        /// <returns>max. number of allowed items for given type</returns>
        public int GetMaxQuantity( ContainerType containerType )
        {
            return maxQuantities[ containerType ];
        }

        /// <summary>
        /// Equality operation
        /// </summary>
        /// <param name="obj">object to compare with</param>
        /// <returns>true when obj is a Product and has the same code, false in any other case</returns>
        public override bool Equals( object obj )
        {
            Product other = obj as Product;

            return other != null
                && this.Code == other.Code;
        }

        /// <summary>
        /// GetHashCode method
        /// since Equals is defined
        /// </summary>
        /// <returns>the product code</returns>
        public override int GetHashCode( )
        {
            return Code.GetHashCode( );
        }

        /// <summary>
        /// Helper function to print the maxQuantities beautifully
        /// </summary>
        /// <returns>a beautifully looking string with all the quantities</returns>
        private string CollapseMaxQuantites()
        {
            return "{" + string.Join(", ", maxQuantities.Select( t => t.Key + "=" + t.Value ) ) + "}";
        }
    }
}
