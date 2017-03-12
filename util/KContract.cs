using System;
using System.Diagnostics;

namespace com.knapp.KCC2017.util
{
    /// <summary>
    /// Helper class to assert preconditions in methods
    /// </summary>
    public static class KContract
    {
        /// <summary>
        /// Assert condition and throw ArgumentException when not satisfied
        /// </summary>
        /// <param name="condition">condition to check --> false throws exception</param>
        /// <param name="message">message that is shown</param>
        public static void Assert( bool condition, string message )
        {
            if ( !condition )
            {
                Assert<ArgumentException>( condition, message );
            }
        }

        /// <summary>
        /// Assert condition and throw ArgumentException when not satisfied
        /// </summary>
        /// <typeparam name="X">Type of exception to throw</typeparam>
        /// <param name="condition">condition to check --> false throws exception</param>
        /// <param name="message">message that is shown</param>
        public static void Assert<X>( bool condition, string message )
                    where X : Exception, new()
        {
            if ( !condition )
            {
                StackTrace trace = new StackTrace( 1, false );
                StackFrame frame = trace.GetFrame( 1 );
                Type offendingType = frame.GetMethod( ).DeclaringType;
                string methodName = frame.GetMethod( ).Name;
                string fullMessage = string.Format( "Methode {0}.{1}: {2}"
                    , offendingType.Name
                    , methodName
                    , message
                );

                X x = (X)Activator.CreateInstance( typeof( X ), fullMessage );

                throw x;
            }
        }

        /// <summary>
        /// Require precondition and throw ArgumentException when not satisfied
        /// </summary>
        /// <param name="condition">condition to check --> false throws exception</param>
        /// <param name="message">message that is shown</param>
        public static void Requires( bool condition
                                    , string message = "Ungültige Argumente (Requires)"
                                    )
        {
            if ( !condition )
            {
                Requires<ArgumentException>( condition, message );
            }
        }

        /// <summary>
        /// Require precondition and throw ArgumentException when not satisfied
        /// </summary>
        /// <typeparam name="X">Type of exception to throw</typeparam>
        /// <param name="condition">condition to check --> false throws exception</param>
        /// <param name="message">message that is shown</param>
        public static void Requires<X>( bool condition
                            , string message = "Ungültige Argumente (Requires)"
                            ) where X : Exception,new()
        {
            if ( !condition )
            {
                StackTrace trace = new StackTrace( 1, false );
                StackFrame frame = trace.GetFrame( 1 );
                Type offendingType = frame.GetMethod( ).DeclaringType;
                string methodName = frame.GetMethod( ).Name;
                string fullMessage = string.Format( "Methode {0}.{1}: {2}"
                    , offendingType.Name
                    , methodName
                    , message
                );

                X x = (X)Activator.CreateInstance( typeof( X ), fullMessage );

                throw x;
            }

        }
    }
}
