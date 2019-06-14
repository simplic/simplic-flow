namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Multiply", Name = "MultiplyNode", Category = "Math")]
    public class MultiplyNode : ActionNode
    {
        public override string Name { get { return nameof(MultiplyNode); } }
        public override string FriendlyName { get { return nameof(MultiplyNode); } }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var dataType = InPinConditionA.DataType;

            if (dataType == typeof(short))
            {
                var a = scope.GetValue<short>(InPinConditionA);
                var b = scope.GetValue<short>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(ushort))
            {
                var a = scope.GetValue<ushort>(InPinConditionA);
                var b = scope.GetValue<ushort>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(int))
            {
                var a = scope.GetValue<int>(InPinConditionA);
                var b = scope.GetValue<int>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(uint))
            {
                var a = scope.GetValue<uint>(InPinConditionA);
                var b = scope.GetValue<uint>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(long))
            {
                var a = scope.GetValue<long>(InPinConditionA);
                var b = scope.GetValue<long>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(ulong))
            {
                var a = scope.GetValue<ulong>(InPinConditionA);
                var b = scope.GetValue<ulong>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(float))
            {
                var a = scope.GetValue<float>(InPinConditionA);
                var b = scope.GetValue<float>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(double))
            {
                var a = scope.GetValue<double>(InPinConditionA);
                var b = scope.GetValue<double>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }
            else if (dataType == typeof(decimal))
            {
                var a = scope.GetValue<decimal>(InPinConditionA);
                var b = scope.GetValue<decimal>(InPinConditionB);

                scope.SetValue(OutPinResult, a * b);
            }            

            return true; 
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "46eea693-27b3-472b-acf2-b6d838cbd823",
            ContainerType = DataPinContainerType.Single,            
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "A",
            IsGeneric = true,
            AllowedTypes = "Int16,UInt16,UInt32,Int32,Int64,UInt64,Single,Double,Decimal")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "1e76a3f3-4ca3-40fc-99e0-a8347f2e21ed",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "B",
            IsGeneric = true,
            AllowedTypes = "Int16,UInt16,UInt32,Int32,Int64,UInt64,Single,Double,Decimal")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "123163c6-115e-47c0-a19f-306ed8279d8a",
            ContainerType = DataPinContainerType.Single,            
            Direction = PinDirection.Out,
            Name = "OutPinResult",
            DisplayName = "Result",
            IsGeneric = true)]
        public DataPin OutPinResult { get; set; }
    }
}
