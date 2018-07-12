using System;

namespace Simplic.Flow
{
    public class OnDocumentScannedNode : EventNode
    {
        public OnDocumentScannedNode()
        {
            EventName = nameof(OnDocumentScannedNode);

            DocumentOut = new DataPin
            {
                ContainerType = DataPinContainerType.Single,
                DataType = typeof(DocumentWithBarcode),
                Direction = PinDirection.Out,
                IsNullable = false,
                Name = "DocumentWithBarcode",
                FriendlyName = "Document with barcode",
                Owner = this,
                Id = Guid.NewGuid(),
                Description = "Document with barcode from twain interface"
            };
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            // Load by .Arguments...
            var value = new DocumentWithBarcode
            {
                
            };

            scope.SetValue(DocumentOut, value);

            runtime.EnqueueNode(FlowOut, scope);

            return true;
        }

        public override string FriendlyName { get { return "New Contact Added Event"; } }
        public override bool NeedsState { get; set; }
        public ActionNode FlowOut { get; set; }
        public DataPin DocumentOut { get; set; }
    }
}