using System;
using System.Collections.Generic;
using System.IO;

namespace Simplic.Flow.Node.IO
{
    [ActionNodeDefinition(DisplayName = "Get Directory Content", Name = nameof(GetDirectoryContentNode), Category = "IO")]
    public class GetDirectoryContentNode : ActionNode
    {        
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var path = scope.GetValue<string>(InPinDirectoryPath);
            var extensionPath = scope.GetValue<string>(InPinSearchPattern);

            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var childScope = scope.CreateChild();

                    childScope.SetValue(OutPinFilePath, file);
                    childScope.SetValue(OutPinFileNameExtension, Path.GetExtension(file));
                    childScope.SetValue(OutPinFilePathWithoutFileName, Path.GetDirectoryName(file));
                    childScope.SetValue(OutPinFileName, Path.GetFileName(file));

                    if (OutNodeEachFile != null)
                        runtime.EnqueueNode(OutNodeEachFile, childScope);
                }
                if (OutNodeCompleted != null)
                    runtime.EnqueueNode(OutNodeCompleted, scope);
                return true;
            }
            else
            {
                Console.WriteLine($"Could not find direcotry {path}");
                return false;
            }
        }

        [DataPinDefinition(
            Id = "488f578c-d77c-4bca-80e8-8c456208d8c8",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinDirectoryPath),
            DisplayName = "Directory Path")]
        public DataPin InPinDirectoryPath { get; set; }

        [DataPinDefinition(
            Id = "b405732e-089e-4a20-b002-281721909629",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinSearchPattern),
            DisplayName = "Search Pattern")]
        public DataPin InPinSearchPattern { get; set; }

        [DataPinDefinition(
            Id = "258b98eb-c052-464b-95b1-927660b7e225",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.In,
            Name = nameof(InPinIncludeSubDirectories),
            DisplayName = "Include Sub Directories")]
        public DataPin InPinIncludeSubDirectories { get; set; }

        [FlowPinDefinition(DisplayName = "Each File", Name = nameof(OutNodeEachFile), PinDirection = PinDirection.Out)]
        public ActionNode OutNodeEachFile { get; set; }

        [FlowPinDefinition(DisplayName = "Completed", Name = nameof(OutNodeCompleted), PinDirection = PinDirection.Out)]
        public ActionNode OutNodeCompleted { get; set; }

        [DataPinDefinition(
            Id = "9dd53fba-3a13-44d2-8f41-5abfd5820562",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = nameof(OutPinFilePath),
            DisplayName = "File Path")]
        public DataPin OutPinFilePath { get; set; }

        [DataPinDefinition(
            Id = "fec0c781-45e9-4d4b-a6a6-90a759690bc2",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = nameof(OutPinFilePathWithoutFileName),
            DisplayName = "File Path without File Name")]
        public DataPin OutPinFilePathWithoutFileName { get; set; }

        [DataPinDefinition(
            Id = "d5ce2a7f-4710-455b-99c5-7b92ef2f71d7",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = nameof(OutPinFileName),
            DisplayName = "File Name")]
        public DataPin OutPinFileName { get; set; }

        [DataPinDefinition(
            Id = "d85c387e-61fe-40fa-a59b-88eb117e21d1",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = nameof(OutPinFileNameExtension),
            DisplayName = "File Name Extension")]
        public DataPin OutPinFileNameExtension { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }
    }
}
