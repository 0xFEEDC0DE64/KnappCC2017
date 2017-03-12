using System;
using System.Runtime.Serialization;

namespace com.knapp.KCC2017.warehouse
{
    [Serializable]
    internal class WorkStationCapcityExceededException : Exception
    {
        public WorkStationCapcityExceededException()
        {
        }

        public WorkStationCapcityExceededException( string message ) : base( message )
        {
        }

        public WorkStationCapcityExceededException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected WorkStationCapcityExceededException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}