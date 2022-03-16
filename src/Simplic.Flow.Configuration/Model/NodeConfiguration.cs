﻿using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public class NodeConfiguration
    {
        public Guid Id { get; set; }
        public string NodeType { get; set; }
        public string ClassName { get; set; }
        public bool IsStartEvent { get; set; }
        public List<NodePinConfiguration> Pins { get; set; } = new List<NodePinConfiguration>();
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the user notes.
        /// </summary>
        public string UserNotes { get; set; }
    }
}