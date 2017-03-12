using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class ContainerNotInStorageException : Exception
    {
        public ContainerNotInStorageException()
        {
        }

        public ContainerNotInStorageException( string message ) : base( message )
        {
        }

        public ContainerNotInStorageException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected ContainerNotInStorageException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}