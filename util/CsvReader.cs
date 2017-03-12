using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace com.knapp.KCC2017.util
{
    internal  static class CsvReader
    {

        /// <summary>
        ///  read lines from a CSV file, create instances of type Target by calling a string[] constructor
        /// </summary>
        /// <typeparam name="Target">type to create</typeparam>
        /// <param name="fullFileName">Full path to the csv file</param>
        /// <returns>a list of instances or a empty list</returns>
        public static List<Target> ReadCsvFile<Target>( string fullFileName )
            where Target : class
        {
            KContract.Requires( !string.IsNullOrWhiteSpace( fullFileName ), "fullFileName mandatory but is null" );

            if ( false == File.Exists( fullFileName ) )
            {
                throw new InvalidOperationException(string.Format("CSV-Input file does not exist: '{0}'", fullFileName ) );
            }

            ConstructorInfo ctorInfo = typeof( Target ).GetConstructor( new [] { typeof(string[]) } );

            if ( null == ctorInfo )
            {
                throw new InvalidOperationException( string.Format( "Target '{0}' can not be constructed because it does not contain a ctor with string [] as argument"
                    , typeof( Target).FullName ) );
            }


            List<Target> objects = new List<Target>();

            using (StreamReader streamReader = new StreamReader( fullFileName ))
            {
                string line;
                while ((line = streamReader.ReadLine() ) != null )
                {
                    if ( !string.IsNullOrWhiteSpace( line )
                        && !line.StartsWith("#"))
                    {
                        string[] fields = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        Target t = (Target)ctorInfo.Invoke(new object[] { fields });

                        objects.Add(t);
                    }
                }
            }


            return objects;
        }
    }
}
