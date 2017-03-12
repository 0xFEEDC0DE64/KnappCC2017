using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class ContainerNotAtWorkStationException : Exception
    {
        public ContainerNotAtWorkStationException()
        {
        }

        public ContainerNotAtWorkStationException( string message ) : base( message )
        {
        }

        public ContainerNotAtWorkStationException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected ContainerNotAtWorkStationException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}