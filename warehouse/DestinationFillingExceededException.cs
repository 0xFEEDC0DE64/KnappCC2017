using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class DestinationFillingExceededException : Exception
    {
        public DestinationFillingExceededException()
        {
        }

        public DestinationFillingExceededException( string message ) : base( message )
        {
        }

        public DestinationFillingExceededException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected DestinationFillingExceededException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}