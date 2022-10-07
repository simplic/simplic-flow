using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public interface IFlowConfigurationRepository
    {
        /// <summary>
        /// Gets FlowConfiguration by Id.
        /// </summary>
        FlowConfiguration Get(Guid id);

        /// <summary>
        /// Saves FlowConfiguration to data base.
        /// </summary>
        bool Save(FlowConfiguration flowConfiguration);

        /// <summary>
        /// Gets all active flows, that are not flagged deleted.
        /// </summary>
        IEnumerable<FlowConfiguration> GetAll(bool getOnlyActive = true);

        /// <summary>
        /// Sets status of flow.
        /// </summary>
        bool SetStatus(Guid id, bool isActive);

        /// <summary>
        /// Sets the deleted flag of a flow.
        /// </summary>
        bool SetDeleted(Guid id);

        /// <summary>
        /// Gets FlowConfiguration by exportId.
        /// </summary>
        FlowConfiguration GetByExportId(Guid exportId);
    }
}
