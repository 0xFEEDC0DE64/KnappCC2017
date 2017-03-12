using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class SourceFillingExhaustedException : Exception
    {
        public SourceFillingExhaustedException()
        {
        }

        public SourceFillingExhaustedException( string message ) : base( message )
        {
        }

        public SourceFillingExhaustedException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected SourceFillingExhaustedException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}