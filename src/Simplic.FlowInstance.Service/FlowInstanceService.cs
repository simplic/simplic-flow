using Simplic.FlowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simplic.Flow;

namespace Simplic.FlowInstance.Service
{
    /// <summary>
    /// Flow instance service implementation
    /// </summary>
    public class FlowInstanceService : IFlowInstanceService
    {
        private readonly IFlowInstanceRepository flowInstanceRepository;

        public FlowInstanceService(IFlowInstanceRepository flowInstanceRepository)
        {
            this.flowInstanceRepository = flowInstanceRepository;
        }

        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> from the database
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> from the database</returns>
        public IEnumerable<Flow.FlowInstance> GetAll()
        {
            return flowInstanceRepository.GetAll();
        }

        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> which are alive from the database 
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> which are alive from the database</returns>
        public IEnumerable<Flow.FlowInstance> GetAllAlive()
        {
            return flowInstanceRepository.GetAllAlive();
        }

        /// <summary>
        /// Gets a <see cref="FlowInstance"/>
        /// </summary>
        /// <param name="flowInstanceId">Id to get</param>
        /// <returns><see cref="FlowInstance"/></returns>
        public Flow.FlowInstance GetById(Guid flowInstanceId)
        {
            return flowInstanceRepository.GetById(flowInstanceId);
        }

        /// <summary>
        /// Saves a <see cref="FlowInstance"/> into the database
        /// </summary>
        /// <param name="flowInstance">Object to save</param>
        /// <returns>True if successfull</returns>
        public bool Save(Flow.FlowInstance flowInstance)
        {
            return flowInstanceRepository.Save(flowInstance);
        }

        /// <summary>
        /// Delete flow instance
        /// </summary>
        /// <param name="flowInstance">Flow instance</param>
        /// <returns>True if successfull</returns>
        public bool Delete(Flow.FlowInstance flowInstance)
        {
            return flowInstanceRepository.Delete(flowInstance);
        }
    }
}
