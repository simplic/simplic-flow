using System;
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
