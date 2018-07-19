using Simplic.FlowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplic.FlowInstance.Service
{
    public class FlowInstanceService : IFlowInstanceService
    {
        private readonly IFlowInstanceRepository flowInstanceRepository;

        public FlowInstanceService(IFlowInstanceRepository flowInstanceRepository)
        {
            this.flowInstanceRepository = flowInstanceRepository;
        }
        
        #region Public Methods

        #region [GetAll]
        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> from the database
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> from the database</returns>
        public IEnumerable<FlowInstance> GetAll()
        {
            return flowInstanceRepository.GetAll();
        }
        #endregion

        #region [GetAllAlive]
        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> which are alive from the database 
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> which are alive from the database</returns>
        public IEnumerable<FlowInstance> GetAllAlive()
        {
            return flowInstanceRepository.GetAllAlive();
        }
        #endregion

        #region [GetById]
        /// <summary>
        /// Gets a <see cref="FlowInstance"/>
        /// </summary>
        /// <param name="flowInstanceId">Id to get</param>
        /// <returns><see cref="FlowInstance"/></returns>
        public FlowInstance GetById(Guid flowInstanceId)
        {
            return flowInstanceRepository.GetById(flowInstanceId);
        }
        #endregion

        #region [Save]
        /// <summary>
        /// Saves a <see cref="FlowInstance"/> into the database
        /// </summary>
        /// <param name="flowInstance">Object to save</param>
        /// <returns>True if successfull</returns>
        public bool Save(FlowInstance flowInstance)
        {
            return flowInstanceRepository.Save(flowInstance);
        }
        #endregion 

        #endregion
    }
}
