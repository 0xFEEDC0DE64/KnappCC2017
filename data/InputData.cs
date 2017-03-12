using com.knapp.KCC2017.data;
using com.knapp.KCC2017.entities;
using com.knapp.KCC2017.util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace com.knapp.KCC2017
{
    /// <summary>
    /// Container class for all input into the solution
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// List of all products that are used in this contest
        /// </summary>
        private readonly List<Product> products = new List<Product>();


        /// <summary>
        /// List of all containers (storage and workstation) 
        /// </summary>
        private readonly List<Container> containers = new List<Container>();

        /// <summary>
        /// Get a read-only collection of all products
        /// </summary>
        /// <returns>readon-only collection with all products</returns>
        public ReadOnlyCollection<Product> GetProducts()
        {
            return new ReadOnlyCollection<Product>( products );
        }


        /// <summary>
        /// Get a read-only collection of all containes (storage and workstation)
        /// </summary>
        /// <returns>read-onyl collection of all containers</returns>
        public ReadOnlyCollection<Container> GetContainers()
        {
            return new ReadOnlyCollection<Container>( containers );
        }


        /// <summary>
        /// Create from outside only via CreateFromCsv
        /// </summary>
        private InputData( )
        {/* EMPTY */  }

        /// <summary>
        /// Load all input data from the csv files and create instance (and composite instances)
        /// </summary>
        /// <returns>a newly created ínstance of the inout</returns>
        public static InputData CreateFromCsv()
        {
            InputData input = new InputData();

            input.LoadProductsFromCsv( System.IO.Path.Combine( Settings.DataPath, Settings.InProductFilename ) );

            input.LoadContainersFromCsv( System.IO.Path.Combine( Settings.DataPath, Settings.InContainerFilename ) );

            return input;
        }

        /// <summary>
        /// Load all products from the CSV
        /// </summary>
        /// <param name="fullFilename">full path of the csv</param>
        private void LoadProductsFromCsv( string fullFilename )
        {
            KContract.Requires( !string.IsNullOrWhiteSpace( fullFilename ), "fullFilename mandatory but is null or whitespace" );

            foreach ( Product product in CsvReader.ReadCsvFile<Product>( fullFilename ) )
            {
                products.Add( product );
            }
            System.Console.Out.WriteLine( "+++ loaded: {0} products", products.Count );

        }

        /// <summary>
        /// Load all containers from csv
        /// </summary>
        /// <param name="fullFilename">full path of the csv</param>
        private void LoadContainersFromCsv( string fullFilename )
        {
            KContract.Requires( !string.IsNullOrWhiteSpace( fullFilename ), "fullFilename mandatory but is null or whitespace" );

            using ( StreamReader streamReader = new StreamReader( fullFilename ) )
            {
                string line;
                while ( ( line = streamReader.ReadLine() ) != null )
                {
                    if ( !string.IsNullOrWhiteSpace( line )
                        && !line.StartsWith( "#" ) )
                    {
                        string[] fields = line.Split(new[] { ';' });

                        string code = fields[ 0 ].Trim();
                        ContainerType type = ContainerType.Get( fields[1].Trim() );

                        Container container = new Container( code, type );

                        for ( int i = 2, s = 0; i < fields.Length; i += 2, ++s )
                        {
                            if ( !string.IsNullOrWhiteSpace( fields[ i ] ) )
                            {
                                Product p = FindProductByCode(  fields[i].Trim() );
                                int quantity = int.Parse( fields[i+1]);

                                container.GetSlots()[ s ]._SetProduct( p );
                                container.GetSlots()[ s ]._SetQuantity( quantity );
                            }
                        }

                        containers.Add( container );
                    }
                }
            }

            System.Console.Out.WriteLine( "+++ loaded: {0} containers", containers.Count );

        }

        /// <summary>
        /// Helper function to find product
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns>product wuth goven code</returns>
        /// <exception cref=""></exception>
        private Product FindProductByCode( string productCode )
        {
            Product result =  products.Find( p => p.Code.Equals( productCode ) );

            if( result == null )
            {
                throw new KeyNotFoundException("no product found for code " + productCode );
            } 

            return result;
        }
    }
}
