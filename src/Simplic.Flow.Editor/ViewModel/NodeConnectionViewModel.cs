using System;
using Simplic.Flow.Configuration;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class NodeConnectionViewModel : Simplic.UI.MVC.ViewModelBase, ILink
    {
        private Guid id;
        private NodeViewModel source;
        private NodeViewModel target;
        private ConnectorViewModel sourceConnector;
        private ConnectorViewModel targetConnector;

        private Simplic.Flow.Configuration.LinkConfiguration flowLink;
        private Simplic.Flow.Configuration.PinConfiguration dataLink;

        public NodeConnectionViewModel(NodeViewModel source, NodeViewModel target,
                ConnectorViewModel sourceConnector, ConnectorViewModel targetConnector)
        {
            this.source = source;
            this.target = target;
            this.sourceConnector = sourceConnector;
            this.targetConnector = targetConnector;
        }

        public Guid Id
        {
            get { return id == Guid.Empty ? Guid.NewGuid() : id; }
        }

        public NodeViewModel SourceViewModel
        {
            get { return source; }
        }

        public NodeViewModel TargetViewModel
        {
            get { return target; }
        }

        public ConnectorViewModel SourceConnectorViewModel
        {
            get { return sourceConnector; }
            set { sourceConnector = value; RaisePropertyChanged(nameof(SourceConnectorViewModel)); }
        }

        public ConnectorViewModel TargetConnectorViewModel
        {
            get { return targetConnector; }
            set { targetConnector = value; RaisePropertyChanged(nameof(TargetConnectorViewModel)); }
        }

        public LinkConfiguration FlowLink
        {
            get
            {
                if (flowLink == null)
                {
                    if (sourceConnector != null && targetConnector != null 
                        && sourceConnector is FlowConnectorViewModel)
                    {
                        var sc = sourceConnector as FlowConnectorViewModel;
                        var tc = targetConnector as FlowConnectorViewModel;

                        flowLink = new LinkConfiguration()
                        {
                            From = new Link { NodeId = source.Id, PinName = sc.Name },
                            To = new Link { NodeId = target.Id, PinName = tc.Name }
                        };
                    }
                }

                return flowLink;
            }
        }

        public PinConfiguration DataLink
        {
            get
            {
                if (dataLink == null)
                {
                    if (sourceConnector != null && targetConnector != null
                        && sourceConnector is DataConnectorViewModel)
                    {
                        var sc = sourceConnector as DataConnectorViewModel;
                        var tc = targetConnector as DataConnectorViewModel;

                        dataLink = new PinConfiguration()
                        {
                            From = new Link { NodeId = source.Id, PinName = sc.Name },
                            To = new Link { NodeId = target.Id, PinName = tc.Name }
                        };
                    }
                }

                return dataLink;
            }
        }

        object ILink.Source
        {
            get
            {
                return source;
            }

            set
            {
                source = value as NodeViewModel;
                IsDirty = true;
                RaisePropertyChanged(nameof(ILink.Source));
            }
        }

        object ILink.Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value as NodeViewModel;
                IsDirty = true;
                RaisePropertyChanged(nameof(ILink.Target));
            }
        }
    }
}
