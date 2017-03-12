using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class ContainerMaxGetExceededException : Exception
    {
        public ContainerMaxGetExceededException()
        {
        }

        public ContainerMaxGetExceededException( string message ) : base( message )
        {
        }

        public ContainerMaxGetExceededException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected ContainerMaxGetExceededException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}