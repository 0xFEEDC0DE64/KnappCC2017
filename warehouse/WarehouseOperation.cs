using com.knapp.KCC2017.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.warehouse
{
    /// <summary>
    /// **KNAPP use only
    /// Base class for all operations carried out during optimization
    /// as get, move, store
    /// </summary>
    public abstract class WarehouseOperation
    {
        private readonly string resultString;

        /// <summary>
        /// Create instance with given data
        /// </summary>
        /// <param name="args">data of operation as object array</param>
        protected WarehouseOperation( params object[] args )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( GetType().Name ).Append( ";" );

            foreach( var arg in args )
            {
                sb.Append( arg.ToString() ).Append( ";" );
            }

            resultString = sb.ToString();
        }

        public override string ToString()
        {
            return resultString;
        }

        public string ToResultString()
        {
            return resultString;
        }

        /// <summary>
        /// Representation of MoveToWorkStation action
        /// </summary>
        public class GetContainer : WarehouseOperation
        {
            public GetContainer( Container container )
                : base ( container.Code )
            {  /** empty **/ }
        }

        /// <summary>
        /// Representation of TransferItems action
        /// </summary>
        public class MoveItems : WarehouseOperation
        {
            public ContainerSlot Source { get; private set; }
                
            public ContainerSlot Destination { get; private set; }

            public int Quantity { get; private set; }

            public MoveItems( ContainerSlot source, ContainerSlot destination, int quantity )
                : base ( source.Container.Code, source.Index
                        , destination.Container.Code, destination.Index
                        , quantity )
            {
                this.Source = source;
                this.Destination = destination;
                this.Quantity = quantity;
            }
            public override string ToString()
            {
                return "MoveItems[source=" + Source.Container.Code + "/" + Source.Index 
                        + ", destination="+ Destination.Container.Code + "/" + Destination.Index 
                        + ", quantity=" + Quantity + "]";
            }
        }

        /// <summary>
        /// Representation of MoveToStoreage action
        /// </summary>
        public class PutContainer : WarehouseOperation
        {
            public PutContainer( Container container )
                : base( container.Code )
            {  /** empty **/}
        }

    }
}
