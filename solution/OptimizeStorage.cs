using System;
using System.Linq;
using System.Collections.Generic;

using com.knapp.KCC2017.data;
using com.knapp.KCC2017.entities;
using com.knapp.KCC2017.util;
using com.knapp.KCC2017.warehouse;

namespace com.knapp.KCC2017.solution
{
    public class OptimizeStorage
    {

        /// <summary>
        /// 
        /// Your name
        /// Please set in constructor 
        /// </summary>
        public string ParticipantName { get; private set; }

        /// <summary>
        /// 
        /// The Id of your institute - please refer to the handout
        /// Please set in constructor
        /// </summary>
        public string InstituteId { get; private set; }

        /// <summary>
        /// local reference to the global warehouse
        /// </summary>
        private readonly Warehouse warehouse;

        /// <summary>
        /// Create the solution instance 
        /// 
        /// Do all your preparations here
        /// 
        /// </summary>
        public OptimizeStorage( Warehouse warehouse )
        {
            KContract.Requires( warehouse != null, "input required but is null" );

            this.warehouse = warehouse;

            //Your code goes here
            ParticipantName = ;
            InstituteId = ;
        }

        public void Optimize()
        {

            //YOUR CODE GOES HERE
        }
    }
}
