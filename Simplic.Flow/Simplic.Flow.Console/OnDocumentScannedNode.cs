using System;
using System.Collections.Generic;

namespace Simplic.Flow.Console
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

        public override bool Execute(IFlowRuntimeService runtime)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            // Load by .Arguments...
            var value = new DocumentWithBarcode
            {
                
            };

            var sope = new PinScope
            {
                Pin = DocumentOut,
                Value = value
            };

            runtime.SetValue(DocumentOut, value);

            runtime.EnqueueNode(FlowOut, sope);

            return true;
        }

        public override string FriendlyName { get { return "New Contact Added Event"; } }
        public override bool NeedsState { get; set; }
        public ActionNode FlowOut { get; set; }
        public DataPin DocumentOut { get; set; }
    }
}