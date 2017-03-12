using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.knapp.KCC2017.data;
using com.knapp.KCC2017.entities;
using System.Collections.ObjectModel;

namespace com.knapp.KCC2017.warehouse
{
    /// <summary>
    /// Class that represents the warehouse
    /// - contains all the data
    /// - provides all necessary functions
    /// - stores your moves to the result file
    /// </summary>
    public class Warehouse
    {
        // ----------------------------------------------------------------------------
        // -- DO NOT CHANGE THESE -----------------------------------------------------
        //    (your result will be evaluated on the server with these settings)
        public const int WORK_STATION_CAPACITY = 5;
        public const int CONTAINER_MAX_GET = 5000;

        /// <summary>
        /// Data coming from the data files (containers and products)
        /// </summary>
        private readonly InputData inputData;

        /// <summary>
        /// The containers at the workstation
        /// </summary>
        private readonly Dictionary<string,Container> workStation = new Dictionary<string, Container>( WORK_STATION_CAPACITY );

        internal int currentNumberOfGets = 0;
        internal int currentNumberOfMoves = 0;
        internal int currentNumberOfPuts = 0;

        private readonly List<WarehouseOperation> result = new List<WarehouseOperation>();

        private WarehouseInfos warehouseInfos;

        public Warehouse( InputData inputData )
        {
            this.inputData = inputData;
        }

        /// <summary>
        /// Get all containers that are in the warehouse
        /// (that is storage and workstation)
        /// </summary>
        /// <returns>the ContainerCollection containing all containers in the warehouse</returns>
        public ReadOnlyCollection<Container> GetAllContainers()
        {
            return inputData.GetContainers();
        }

        /// <summary>
        /// Get all containers currently at the workstation
        /// </summary>
        /// <returns>returns all containers at the workstation, an empty collection if there are none</returns>
        public ReadOnlyCollection<Container> GetContainersAtWorkStation()
        {
            List<Container> copy = new List<Container>();

            foreach( Container container in workStation.Values )
            {
                copy.Add( container );
            }

            return new ReadOnlyCollection<Container>( copy );
        }

        /// <summary>
        /// Get all containers currently in storage
        /// </summary>
        /// <returns>collection of all containers in storage and not in workstation</returns>
        public ReadOnlyCollection<Container> GetContainersInStorage()
        {
            List<Container> copy = new List<Container>( GetAllContainers() );

            return new ReadOnlyCollection<Container>( copy.Except( workStation.Values.ToList() ).ToList() );
        }

        /// <summary>
        /// Get the number of remaining get operations
        /// </summary>
        /// <returns></returns>
        public int GetRemainingMovesToWorkstation()
        {
            return CONTAINER_MAX_GET - currentNumberOfGets;
        }

        /// <summary>
        /// Get or build an WarehoouseInfos instance with the current data
        /// </summary>
        /// <returns>WarehouuseInfo instance with the current data</returns>
        public WarehouseInfos BuildWarehouseInfos()
        {
             if( warehouseInfos == null )
            {
                warehouseInfos = new WarehouseInfos( this );
            }

            return warehouseInfos;
        }

        public IEnumerable<WarehouseOperation> GetResult()
        {
            return result;
        }

        /// <summary>
        /// Move a container form storage to the workstations
        /// </summary>
        /// <param name="container">the container to move</param>
        /// <exception cref="ArgumentException">when container is null</exception>
        /// <exception cref="ContainerNotInStorageException">when the container is already at the workstation</exception>
        /// <exception cref="WorkStationCapcityExceededException">when there are no free slots at the workstation</exception>
        /// <exception cref="ContainerMaxGetExceededException">when the maximum number of gets has been reached</exception> 
        public void MoveToWorkStation( Container container )
        {
            if( container == null )
            {
                throw new ArgumentException( "container must not be null" );
            }

            result.Add( new WarehouseOperation.GetContainer( container ) );

            if( workStation.ContainsKey( container.Code ) )
            {
                throw new ContainerNotInStorageException( "container is already at workstation: " + container );
            }

            if( workStation.Count >= WORK_STATION_CAPACITY )
            {
                throw new WorkStationCapcityExceededException( "containers at workstation are limited to " + WORK_STATION_CAPACITY.ToString() + " => @WorkStation= " + workStation.Values );
            }

            if( currentNumberOfGets >= CONTAINER_MAX_GET )
            {
                throw new ContainerMaxGetExceededException( "container gets are limited to " + CONTAINER_MAX_GET.ToString() );
            }

            workStation.Add( container.Code, container );
            ++currentNumberOfGets;
        }

        /// <summary>
        /// Transfer a counted number of items from one slot to another slot
        /// </summary>
        /// <param name="source">slot from which to remove the items</param>
        /// <param name="destination">slot into whcih to put the items</param>
        /// <param name="quantity">the number of items to transfer</param>
        /// <exception cref="ArgumentException">when either source or destination is null or  or quantity &lt;= 0 or there are no products at the source</exception>
        /// <exception cref="ContainerNotAtWorkStationException">when either source or destination container are not at the workstation</exception> 
        /// <exception cref="SourceFillingExhaustedException">when the quantity to transfer is higher than the number of items in the source</exception>
        /// <exception cref="ProductMismatchExcpetion">when the destination is not empty and contains already a different product</exception>
        /// <exception cref="DestinationFillingExceededException">when the total quantity in the destination (existing+transferred) exceeds the max quantity of the product for that type of slot/ container</exception>
        public void TransferItems( ContainerSlot source, ContainerSlot destination, int quantity )
        {
            warehouseInfos = null; //invalidate the statistics

            if( source == null || destination == null )
            {
                throw new ArgumentException( "source or dest is null" );
            }

            WarehouseOperation.MoveItems moveItemOperation = new WarehouseOperation.MoveItems( source, destination, quantity );

            if ( quantity <= 0 )
            {
                throw new ArgumentException("quantity must be >0: " + moveItemOperation );
            }

            if ( source.IsEmpty() )
            {
                throw new ArgumentException("no products at source: " + moveItemOperation );
            }

            if ( !workStation.ContainsKey( source.Container.Code ) )
            {
                throw new ContainerNotAtWorkStationException( "source container not at workstation: " + moveItemOperation + "=> @WorkStation: " + workStation.Values );
            }

            if ( !workStation.ContainsKey( destination.Container.Code ) )
            {
                throw new ContainerNotAtWorkStationException( "destination container not at workstation: " + moveItemOperation + "=> @WorkStation: " + workStation.Values );
            }

            if( source.Quantity < quantity )
            {
                throw new SourceFillingExhaustedException("can't take " + quantity + " items from source: " + moveItemOperation );
            }

            if ( !destination.IsEmpty() )
            {
                if( source.Product != destination.Product )
                {
                    throw new ProductMismatchExcpetion("mismatching products: " + moveItemOperation );
                }
            }

            if( destination.Quantity + quantity > source.Product.GetMaxQuantity( destination.Container.ContainerType ) )
            {
                throw new DestinationFillingExceededException( "can't put #" + quantity + " to destination: " + moveItemOperation
                    + " => max/slot=" + source.Product.GetMaxQuantity( destination.Container.ContainerType ) );
            }

            Product sourceProduct = source.Product;

            source._SetQuantity( source.Quantity - quantity );
            destination._SetQuantity( destination.Quantity + quantity );
            destination._SetProduct( sourceProduct );

            result.Add( moveItemOperation );
            ++currentNumberOfMoves;
        }

        /// <summary>
        /// Move containers from workstation back to storage
        /// </summary>
        /// <param name="container">the container to move</param>
        /// <exception cref="ArgumentException">when container is null</exception>
        /// <exception cref="ContainerNotAtWorkStationException">when the container is not at the workstation</exception> 
        public void MoveToStorage( Container container )
        {
            if( container == null )
            {
                throw new ArgumentException("container must not be null");
            }

            result.Add( new WarehouseOperation.PutContainer( container ) );

            if ( !workStation.ContainsKey( container.Code ) )
            {
                throw new ContainerNotAtWorkStationException( "source container not at workstation: " + container );
            }

            workStation.Remove( container.Code );
            ++currentNumberOfPuts;
        }
    }
}
