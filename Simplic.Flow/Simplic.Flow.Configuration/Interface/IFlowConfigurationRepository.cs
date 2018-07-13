﻿using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public interface IFlowConfigurationRepository
    {
        FlowConfiguration Get(Guid id);
        bool Save(FlowConfiguration flowConfiguration);
        IEnumerable<FlowConfiguration> GetAll();
    }
}
