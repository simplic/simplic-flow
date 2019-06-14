using Simplic.Flow.Configuration;
using System;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// NodeConnectionViewModel
    /// </summary>
    public class NodeConnectionViewModel : Simplic.UI.MVC.ViewModelBase, ILink
    {
        #region Private Members
        private Guid id;
        private NodeViewModel source;
        private NodeViewModel target;
        private ConnectorViewModel sourceConnector;
        private ConnectorViewModel targetConnector;
        private LinkConfiguration flowLink;
        private PinConfiguration dataLink;
        #endregion

        #region Constructor
        public NodeConnectionViewModel(NodeViewModel source, NodeViewModel target,
                ConnectorViewModel sourceConnector, ConnectorViewModel targetConnector)
        {
            this.source = source;
            this.target = target;
            this.sourceConnector = sourceConnector;
            this.targetConnector = targetConnector;

            // TODO: Move is connected to the base class, just need to figure out a way to raise property changed
            SourceConnectorViewModel.IsConnected = true;

            if (TargetConnectorViewModel != null)
                TargetConnectorViewModel.IsConnected = true;
        }
        #endregion

        #region Private Properties
        
        #region [ILink.Source]
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
        #endregion

        #region [ILink.Target]
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
        #endregion

        #endregion

        #region Public Properties

        #region [Id]
        public Guid Id { get { return id == Guid.Empty ? Guid.NewGuid() : id; } }
        #endregion

        #region [SourceViewModel]
        public NodeViewModel SourceViewModel { get { return source; } }
        #endregion

        #region [TargetViewModel]
        public NodeViewModel TargetViewModel { get { return target; } }
        #endregion

        #region [SourceConnectorViewModel]
        public ConnectorViewModel SourceConnectorViewModel
        {
            get { return sourceConnector; }
            set { sourceConnector = value; RaisePropertyChanged(nameof(SourceConnectorViewModel)); }
        }
        #endregion

        #region [TargetConnectorViewModel]
        public ConnectorViewModel TargetConnectorViewModel
        {
            get { return targetConnector; }
            set { targetConnector = value; RaisePropertyChanged(nameof(TargetConnectorViewModel)); }
        }
        #endregion

        #region [FlowLink]
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
        #endregion

        #region [DataLink]
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
        #endregion

        #region [StrokeColor]
        /// <summary>
        /// Gets a color depending on the pin type
        /// </summary>
        public string StrokeColor
        {
            get
            {
                if (SourceConnectorViewModel is FlowConnectorViewModel)
                {
                    return Constants.FlowStrokeColor;
                }
                else
                {
                    return (SourceConnectorViewModel as DataConnectorViewModel).StrokeColor;
                }
            }
        } 
        #endregion


        #endregion
    }
}