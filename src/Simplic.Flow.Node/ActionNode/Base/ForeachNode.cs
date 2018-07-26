using System;

namespace Simplic.Flow.Node
{
    public class ForeachNode : ActionNode
    {
        public ForeachNode()
        {
            OutPin = new DataPin
            {
                DataType = typeof(object),
                ContainerType = DataPinContainerType.Single,
                Owner = this,
                Id = Guid.NewGuid(),
                Name = "Each item out",
                Direction = PinDirection.Out,
                Description = "Each item out"
            };
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var values = scope.GetListValue<object>(InPinList);
            foreach (var value in values)
            {
                var newScope = scope.CreateChild();
                newScope.SetValue(OutPin, value);

                runtime.EnqueueNode(OutNodeEachItem, newScope);
            }

            runtime.EnqueueNode(OutNodeCompleted, scope);

            return true;
        }

        public ActionNode OutNodeEachItem { get; set; }
        public ActionNode OutNodeCompleted { get; set; }
        public DataPin InPinList { get; set; }
        public DataPin OutPin { get; set; }
        public override string FriendlyName { get { return nameof(ForeachNode); } }
        public override string Name { get { return nameof(ForeachNode); } }
    }
}