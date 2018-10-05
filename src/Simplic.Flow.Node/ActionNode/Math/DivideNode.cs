namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Divide", Name = "DivideNode", Category = "Math")]
    public class DivideNode : ActionNode
    {
        public override string Name { get { return nameof(DivideNode); } }

        public override string FriendlyName { get { return nameof(DivideNode); } }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var dataType = InPinConditionA.DataType;

            if (dataType == typeof(short))
            {
                var a = scope.GetValue<short>(InPinConditionA);
                var b = scope.GetValue<short>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(ushort))
            {
                var a = scope.GetValue<ushort>(InPinConditionA);
                var b = scope.GetValue<ushort>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(int))
            {
                var a = scope.GetValue<int>(InPinConditionA);
                var b = scope.GetValue<int>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(uint))
            {
                var a = scope.GetValue<uint>(InPinConditionA);
                var b = scope.GetValue<uint>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(long))
            {
                var a = scope.GetValue<long>(InPinConditionA);
                var b = scope.GetValue<long>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(ulong))
            {
                var a = scope.GetValue<ulong>(InPinConditionA);
                var b = scope.GetValue<ulong>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(float))
            {
                var a = scope.GetValue<float>(InPinConditionA);
                var b = scope.GetValue<float>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(double))
            {
                var a = scope.GetValue<double>(InPinConditionA);
                var b = scope.GetValue<double>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }
            else if (dataType == typeof(decimal))
            {
                var a = scope.GetValue<decimal>(InPinConditionA);
                var b = scope.GetValue<decimal>(InPinConditionB);

                scope.SetValue(OutPinResult, a / b);
            }

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "503d1743-a6bc-4d73-8a86-6b76b15416d9",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "A",
            IsGeneric = true,
            AllowedTypes = "Int16,UInt16,UInt32,Int32,Int64,UInt64,Single,Double,Decimal")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "b78e9d81-cd26-48e9-a39a-37682a39ed25",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "B",
            IsGeneric = true,
            AllowedTypes = "Int16,UInt16,UInt32,Int32,Int64,UInt64,Single,Double,Decimal")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "2f68c3b7-32a2-4cc4-bc3d-f5b0f25a472f",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.Out,
            Name = "OutPinResult",
            DisplayName = "Result",
            IsGeneric = true)]
        public DataPin OutPinResult { get; set; }
    }
}
