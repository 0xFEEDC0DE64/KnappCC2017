using com.knapp.KCC2017.data;
using com.knapp.KCC2017.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.knapp.KCC2017.warehouse
{
    /// <summary>
    /// Helper class to store a SNAPSHOT of the warehouse statistics
    /// </summary>
    public class WarehouseInfos
    {
        public readonly int[] totalContainers;
        public readonly int[] totalSlots;
        public readonly ReadOnlyCollection<Container> containersAtWorkStation;
    
        public readonly int numberOfGets;
        public readonly int numberOfPuts;
        public readonly int numberOfMoves;
        
        public readonly int[] emptyContainers;
        public readonly int[] emptySlots;

        public WarehouseInfos( Warehouse warehouse )
        {
            totalContainers = CountTotalContainers( warehouse );
            totalSlots = CountTotalSlots( warehouse );
            emptyContainers = CountEmptyContainers( warehouse );
            emptySlots = CountEmptysSlots( warehouse ); 

            containersAtWorkStation = warehouse.GetContainersAtWorkStation();

            numberOfGets = warehouse.currentNumberOfGets;
            numberOfPuts = warehouse.currentNumberOfPuts;
            numberOfMoves = warehouse.currentNumberOfMoves;
        }

        private int[] CountTotalContainers( Warehouse warehouse )
        {
            int[] result = new int[ContainerType.Count + 1 ];

            foreach( var container in warehouse.GetAllContainers() )
            {
                result[ 0 ]++;
                result[ container.ContainerType.Ordinal ]++;
            }

            return result;
        }

        private int[] CountTotalSlots( Warehouse warehouse )
        {
            int[] result = new int[ContainerType.Count + 1 ];

            foreach ( var container in warehouse.GetAllContainers() )
            {
                result[ 0 ] += container.ContainerType.NumberOfSlots;
                result[ container.ContainerType.Ordinal ] += container.ContainerType.NumberOfSlots;
            }

            return result;
        }
        private int[] CountEmptysSlots( Warehouse warehouse )
        {
            int[] result = new int[ContainerType.Count + 1 ];

            foreach ( var container in warehouse.GetAllContainers() )
            {
                foreach ( var slot in container.GetSlots() )
                {
                    if ( slot.IsEmpty() )
                    {
                        result[ 0 ]++;
                        result[ container.ContainerType.Ordinal ]++;
                    }
                }
            }

            return result;
        }

        private int[] CountEmptyContainers( Warehouse warehouse )
        {
            int[] result = new int[ContainerType.Count + 1 ];

            foreach( var container in warehouse.GetAllContainers() )
            {
                if( container.IsEmpy() )
                {
                    result[ 0 ]++;
                    result[ container.ContainerType.Ordinal ]++;
                }
            }

            return result;
        }


        public override string ToString()
        {
            return "WarehouseInfos[totalContainers=" + totalContainers.ToDelimitedString(",")
                     + ", totalSlots=" + totalSlots.ToDelimitedString(",") 
                    + ", containersAtWorkStation=" + containersAtWorkStation.ToDelimitedString(",")
                    + ", numberOfGets=" + numberOfGets + ", numberOfPuts=" + numberOfPuts //
                    + ", numberOfMoves=" + numberOfMoves //
                    + ", emptyContainers=" + emptyContainers.ToDelimitedString(",")
                    + ", emptySlots=" + emptySlots.ToDelimitedString(",")
                    + "]";
        }

    }
}
