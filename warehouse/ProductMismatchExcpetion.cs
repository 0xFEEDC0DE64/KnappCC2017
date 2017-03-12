using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class ProductMismatchExcpetion : Exception
    {
        public ProductMismatchExcpetion()
        {
        }

        public ProductMismatchExcpetion( string message ) : base( message )
        {
        }

        public ProductMismatchExcpetion( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected ProductMismatchExcpetion( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}