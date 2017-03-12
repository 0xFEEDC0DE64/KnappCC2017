using com.knapp.KCC2017.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.data
{
    public class ContainerSlot
    {
        public Container Container { get; private set; }

        public Product Product { get; private set; }
        public int Index { get; private set; }


        public int Quantity { get; private set; }


        public ContainerSlot( Container container, int index )
        {
            this.Container = container;
            this.Index = index;
        }

        public bool IsEmpty()
        {
            return Product == null;
        }

        public override string ToString()
        {
            if( IsEmpty() )
            {
                return "ContainerSlot[]";
            }

            return "ContainerSlot[ product= " + Product + ", quantitiy= " + Quantity + "]";
        }


        // ----------------------------------------------------------------------------
        // ----------------------------------------------------------------------------
        // should only be called internal!!! otherwise you could tamper with the result
        //   (Warehouse.apply(moveItems), InputData.readContainers())

        public void _SetProduct( Product product)
        {
            this.Product = product;
        }

        public void _SetQuantity( int quantity )
        {
            this.Quantity = quantity;
            if ( quantity == 0 )
            {
                Product = null;
            }
        }
    }
}
