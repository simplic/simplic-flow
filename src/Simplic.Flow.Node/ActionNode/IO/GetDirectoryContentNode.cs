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
            var extensionPath = scope.GetValue<string>(InPinFileExtension);

            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var childScope = scope.CreateChild();

                    childScope.SetValue(OutPinFilePath, file);
                    childScope.SetValue(OutPinFileExtension, file.Substring(file.LastIndexOf(".") + 1));
                    childScope.SetValue(OutPinFilePathWithoutFileName, file.Substring(0, file.LastIndexOf(@"\")));
                    childScope.SetValue(OutPinFileName, file.Substring(file.LastIndexOf(@"\") + 1, file.LastIndexOf(".") - file.LastIndexOf(@"\") - 1));

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

        [FlowPinDefinition(DisplayName = "Check Directory", Name = "InNodeCheckDirectory", PinDirection = PinDirection.In)]
        public ActionNode InNodeCheckDirectory { get; set; }

        [DataPinDefinition(
            Id = "488f578c-d77c-4bca-80e8-8c456208d8c8",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinDirectoryPath",
            DisplayName = "Directory Path")]
        public DataPin InPinDirectoryPath { get; set; }

        [DataPinDefinition(
            Id = "b405732e-089e-4a20-b002-281721909629",
            ContainerType = DataPinContainerType.List,
            DataType = typeof(List<string>),
            Direction = PinDirection.In,
            Name = "InPinFileExtension",
            DisplayName = "File Extension")]
        public DataPin InPinFileExtension { get; set; }

        [FlowPinDefinition(DisplayName = "Each File", Name = "OutNodeEachFile", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeEachFile { get; set; }

        [FlowPinDefinition(DisplayName = "Completed", Name = "OutNodeCompleted", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeCompleted { get; set; }

        [DataPinDefinition(
            Id = "9dd53fba-3a13-44d2-8f41-5abfd5820562",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFilePath",
            DisplayName = "File Path")]
        public DataPin OutPinFilePath { get; set; }

        [DataPinDefinition(
            Id = "fec0c781-45e9-4d4b-a6a6-90a759690bc2",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFilePathWithoutFileName",
            DisplayName = "File Path without File Name")]
        public DataPin OutPinFilePathWithoutFileName { get; set; }

        [DataPinDefinition(
            Id = "d5ce2a7f-4710-455b-99c5-7b92ef2f71d7",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFileName",
            DisplayName = "File Name")]
        public DataPin OutPinFileName { get; set; }

        [DataPinDefinition(
            Id = "d85c387e-61fe-40fa-a59b-88eb117e21d1",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFileNameExtension",
            DisplayName = "File Name Extension")]
        public DataPin OutPinFileExtension { get; set; }

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
