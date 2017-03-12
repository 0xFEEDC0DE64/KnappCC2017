using com.knapp.KCC2017.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.util
{
    /// <summary>
    /// Write for result file
    /// 
    /// Should not be modified
    /// 
    /// </summary>
    public class ResultWriter : IDisposable
    {
        private readonly StreamWriter resultFileWriter;

        /// <summary>
        /// Create a result writer that will write to the given file
        /// An existing file is deleted
        /// </summary>
        /// <param name="filename"></param>
        public ResultWriter( string filename )
        {
            KContract.Requires( ! string.IsNullOrWhiteSpace( filename ), "filename required but is null or whitespace"  );

            if ( File.Exists( filename ) )
            {
                File.Delete( filename );
            }

            resultFileWriter = new StreamWriter( filename );
        }

        /// <summary>
        /// Write the results within the collection into the file
        /// </summary>
        /// <param name="result">the list with results as Tuples (tick, replenOrder)</param>
        public void Write( object[] result )
        {
            KContract.Requires( result != null, "result mandatory but is required" );

            foreach( var row in result )
            {
                //TODO
            }
        }


        /// <summary>
        /// Dispose
        /// closes resultFilewriter
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                resultFileWriter.Flush( );
                resultFileWriter.Close( );
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
        }

    }
}
