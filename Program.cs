using System;
using System.Collections.Generic;
using System.IO;
using com.knapp.KCC2017.solution;
using com.knapp.KCC2017.util;
using com.knapp.KCC2017.warehouse;
using com.knapp.KCC2017.data;

namespace com.knapp.KCC2017
{
    static class Program
    {
        static void Main( )
        {
            Console.Out.WriteLine("KNAPP Coding Contest 2017: Starting...");

            Warehouse warehouse = null;
            WarehouseInfos infosBefore = null;
            WarehouseInfos infosAfter = null;

            try
            {
                Console.Out.WriteLine("#... LOADING DATA ...");
                InputData input = InputData.CreateFromCsv( );

                warehouse = new Warehouse( input );

                infosBefore = warehouse.BuildWarehouseInfos();
            }
            catch( Exception e )
            {
                ShowException( e, "Exception in startup code" );
                Console.Out.WriteLine( "Press <enter>" );
                Console.In.ReadLine( );
                throw;
            }

            Console.Out.WriteLine( "# optimization" );
            Console.Out.WriteLine( "### Your output starts here" );

            OptimizeStorage optimizer = new OptimizeStorage( warehouse );

            WriteProperties( optimizer, Settings.outPropertyFilename );

            optimizer.Optimize();

            Console.Out.WriteLine( "### Your output stops here" );


            try
            {
                infosAfter = warehouse.BuildWarehouseInfos();

                PrintStatisticsDiff( Console.Out, infosBefore, infosAfter );


                PrepareUpload.WriteResult( warehouse.GetResult() );
                PrepareUpload.CreateZipFile();
                Console.Out.WriteLine( ">>> Created " + Settings.outZipFilename );
            }
            catch ( Exception e )
            {
                ShowException( e, "Exception in shutdown code" );
                throw;
            }

            Console.Out.WriteLine( "Press <enter>" );
            Console.In.ReadLine( );
        }

        private static void PrintStatisticsDiff( TextWriter writer, WarehouseInfos left, WarehouseInfos right )
        {
            writer.WriteLine( "# =============================================================================" );
            writer.WriteLine( "#" );  
            writer.WriteLine( "#                  Empty | containers       | slot" );
            foreach ( ContainerType ct in ContainerType.Values )
            {
                writer.WriteLine( "#             {0,10} | {1,5} ( {2,6:+##,##0;-##,##0;+0})  | {3,5}  ({4,6:+##,##0;-##,##0;+0})"
                                    , ct
                                    , left.emptyContainers[ ct.Ordinal ]
                                    , right.emptyContainers[ ct.Ordinal ] - left.emptyContainers[ ct.Ordinal ]
                                    , right.emptySlots[ ct.Ordinal ]
                                    , right.emptySlots[ ct.Ordinal ] - left.emptySlots[ ct.Ordinal ]
                                );
            }

            writer.WriteLine( "#             {0,10} | {1,5} ( {2,6:+##,##0;-##,##0;+0})  | {3,5}  ({4,6:+##,##0;-##,##0;+0})"
                            , "[TOTAL]"
                            , left.emptyContainers[ 0 ]
                            , right.emptyContainers[ 0 ] - left.emptyContainers[ 0 ]
                            , left.emptySlots[ 0 ]
                            , right.emptySlots[ 0 ] - left.emptySlots[ 0 ]
                            );
            writer.WriteLine( "#" );
            writer.WriteLine( "# SCORE: {0,6} (exclusive upload time) ", GetScore( left, right ) );
            writer.WriteLine( "#" );
            writer.WriteLine( "# =============================================================================" );

        }

        public static int GetScore( WarehouseInfos left, WarehouseInfos right )
        {
            int score = 0;

            foreach( var ct in ContainerType.Values )
            {
                score += (right.emptySlots[ ct.Ordinal ] - left.emptySlots[ ct.Ordinal ] ) * ( 4 / ct.NumberOfSlots );
            }

            score += 2 * ( right.emptyContainers[ 0 ] - left.emptyContainers[ 0 ] );

            return score;
        }

        /// <summary>
        /// Helper function to write the properties to the file
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="outFilename"></param>
        /// <exception cref="ArgumentException">when either solution.InstituteId or solution.ParticipantName is not valid</exception>
        private static void WriteProperties(OptimizeStorage solution, string outFilename)
        {
            KContract.Requires<ArgumentException>( !string.IsNullOrWhiteSpace( solution.ParticipantName ), "solution.ParticipantName must not be empty - please set to correct value" );
            KContract.Requires<ArgumentException>( !string.IsNullOrWhiteSpace(solution.InstituteId), "solution.InstituteId must not be empty - please set to correct value");

            if (File.Exists(outFilename))
            {
                File.Delete( outFilename);
            }

            using (StreamWriter stream = new StreamWriter(outFilename))
            {
                stream.WriteLine( "# -*- conf-javaprop -*-" );
                stream.WriteLine( "participant = {0} {1}", solution.InstituteId, solution.ParticipantName );
                stream.WriteLine( "technology = c#" );
            }

        }

        /// <summary>
        /// write exception to console.error
        /// includes inner exception and data
        /// </summary>
        /// <param name="e">exception that should be shown</param>
        /// <param name="codeSegment">segment where the exception was caught</param>
        public static void ShowException( Exception e, string codeSegment )
        {
            KContract.Requires( e != null, "e is mandatory but is null" );
            KContract.Requires( ! string.IsNullOrWhiteSpace(codeSegment), "codeSegment is mandatory but is null or whitespace" );

            Console.Out.WriteLine( "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" );
            Console.Out.WriteLine(  codeSegment );
            Console.Out.WriteLine( "[{0}]: {1}", e.GetType( ).Name, e.Message );

            for ( Exception inner = e.InnerException
                ; inner != null
                ; inner = inner.InnerException )
            {
                System.Console.WriteLine( ">>[{0}] {1}"
                                                , inner.GetType( ).Name
                                                , inner.Message
                                            );
            }


            if ( e.Data != null && e.Data.Count > 0 )
            {
                Console.Error.WriteLine( "------------------------------------------------" );
                Console.Error.WriteLine( "Data in exception:" );
                foreach( KeyValuePair<string, string> elem in e.Data )
                {
                    Console.Error.WriteLine( "[{0}] : '{1}'", elem.Key, elem.Value );
                }
            }
            Console.Out.WriteLine( "------------------------------------------------" );
            Console.Out.WriteLine( e.StackTrace );
            Console.Out.WriteLine( "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" );
        }
    }
}
