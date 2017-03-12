using com.knapp.KCC2017.entities;
using com.knapp.KCC2017.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.data
{
    public class Container
    {
        /// <summary>
        /// Container code (it's unique name)
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Container type
        /// </summary>
        public ContainerType ContainerType { get; private set; }

        private readonly ContainerSlot[] slots;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="code">code of this container</param>
        /// <param name="containerType">type of this container</param>
        public Container( string code, ContainerType containerType )
        {
            this.Code = code;
            this.ContainerType = containerType;

            slots = new ContainerSlot[ ContainerType.NumberOfSlots ];

            for( int i = 0; i < slots.Length; ++i )
            {
                slots[ i ] = new ContainerSlot( this, i );
            }
        }


        /// <summary>
        /// Is container empty (all slots are empty)
        /// </summary>
        /// <returns>true container is empty, false in any other case</returns>
        public bool IsEmpy()
        {
            return ! slots.Any( s => !s.IsEmpty() );
        }

        /// <summary>
        /// Return a collection with all slots
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<ContainerSlot> GetSlots()
        {
            return new ReadOnlyCollection<ContainerSlot>( slots );
        }

        /// <summary>
        /// Get a list with all empty slots in the container
        /// </summary>
        /// <returns>a list with empty slots, an empty list if non are empty</returns>
        public List<ContainerSlot> GetEmptySlots()
        {
            return slots.Where( s => s.IsEmpty() ).ToList();
        }

        /// <summary>
        /// Stringify this
        /// </summary>
        /// <returns>a human readable string representing this instance</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( "Container[ code= " ).Append( Code );
            sb.Append( ", type = " ).Append( ContainerType );
            sb.Append( "] {" );

            foreach( var slot in slots )
            {
                if( slot.IsEmpty() )
                {
                    sb.Append("<empty>, ");
                }
                else
                {
                    sb.Append( slot.Product.Code ).Append( ", #" ).Append(slot.Quantity).Append(",");
                }
            }
            
            return sb.ToString();
        }
    }
}
