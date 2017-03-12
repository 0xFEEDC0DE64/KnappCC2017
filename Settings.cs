using com.knapp.KCC2017.util;

namespace com.knapp.KCC2017
{
    /// <summary>
    /// class containing settings for the program
    /// 
    /// DO NOT MODIFY THE SETTINGS
    /// 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Directory for the input files
        /// </summary>
        public const string DataPath = @"input";

        /// <summary>
        /// Filename for input of product data
        /// </summary>
        public const string InProductFilename = "products.csv";

        /// <summary>
        /// Filename for input of container data
        /// </summary>
        public const string InContainerFilename = "containers.csv";


        /// <summary>
        /// Directory where the output will be written
        /// </summary>
        public const string OutputPath = @"";

        /// <summary>
        /// Path where the source can be found
        /// </summary>
        public const string sourceDirectory = @"..\..\";

        /// <summary>
        /// Name of the results file
        /// </summary>
        public const string outResultFilename = @"warehouse-operations.csv";

        /// <summary>
        /// Name of the properties file
        /// </summary>
        public const string outPropertyFilename = @"KCC2017.properties";

        /// <summary>
        /// Name of the zip-file that is generated
        /// </summary>
        public const string outZipFilename = @"upload2017.zip";

        /// <summary>
        /// Name of the source directory within the zip file
        /// </summary>
        public const string zipSourceDirectory = "src/";
    }
}
