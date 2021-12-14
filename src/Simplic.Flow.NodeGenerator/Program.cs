using Newtonsoft.Json;
using Simplic.Flow.Editor.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simplic.Flow.NodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var types = new List<Type>
            {
               typeof(Directory),
               typeof(Path),
               typeof(File),
               typeof(Convert),
               typeof(String),
               typeof(DateTime),
               typeof(TimeSpan),
               typeof(Int32),
               typeof(Double),
               typeof(Guid),
               typeof(Array),
               typeof(Enum),
               typeof(Regex)
            };
            var forbiddenReturnTypes = new List<Type>
            {
                typeof(Stream),
                typeof(StreamReader),
                typeof(StreamWriter),
                typeof(MemoryStream),
                typeof(TextReader),
                typeof(TextWriter)
            };
            var allNodeRegistration = new StringBuilder();
            foreach (var type in types)
            {
                var typeNodes = new List<ActionNodeDefinition>();

                // Get all static and public methods
                foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .OrderBy(x => x.Name).ThenBy(x => x.GetParameters().Count()))
                {
                    var methodName = method.Name;
                    var skip = false;

                    var node = new ActionNodeGenerationDefinition();

                    node.OutFlowPins.Add(new FlowPinDefinition
                    {
                        AllowMultiple = false,
                        DisplayName = "Success",
                        Id = Guid.NewGuid(),
                        Name = "OutNodeSuccess",
                        PinDirection = PinDirectionDefinition.Out
                    });

                    node.OutFlowPins.Add(new FlowPinDefinition
                    {
                        AllowMultiple = false,
                        DisplayName = "Failed",
                        Id = Guid.NewGuid(),
                        Name = "OutNodeFailed",
                        PinDirection = PinDirectionDefinition.Out
                    });

                    if (forbiddenReturnTypes.Contains(method.ReturnType))
                    {
                        Console.WriteLine("SKIP the following method:");
                        skip = true;
                    }

                    Console.WriteLine($"{type.Namespace}.{type.Name}.{method.Name}: {method.ReturnType}");

                    if (method.ReturnType != typeof(void))
                    {
                        
                            node.OutDataPins.Add(new DataPinDefinition
                            {
                                Id = Guid.NewGuid(),
                                DisplayName = "Return",
                                Name = "OutPinReturn",
                                Type = method.ReturnType,
                                IsGeneric = false,
                                PinDirection = PinDirectionDefinition.Out
                            });
                        
                    }

                    if (method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
                        skip = true;

                    if (method.Name.StartsWith("get_"))
                        skip = true;

                    if (method.Name.StartsWith("set_"))
                        skip = true;

                    if (method.Name.StartsWith("op_"))
                        skip = true;

                    if (method.ContainsGenericParameters)
                        skip = true;

                    // Do not create iterable for byte-array
                    if (method.ReturnType != typeof(byte[]) && method.ReturnType != typeof(string))
                    {
                        if (method.ReturnType.IsArray || typeof(IEnumerable).IsAssignableFrom(method.ReturnType))
                        {
                            Console.WriteLine(" --> Enumerable!");

                            node.OutFlowPins.Add(new FlowPinDefinition
                            {
                                AllowMultiple = false,
                                DisplayName = "Each item",
                                Id = Guid.NewGuid(),
                                Name = "OutNodeEachItem",
                                PinDirection = PinDirectionDefinition.Out
                            });

                            if (method.ReturnType.IsArray)
                            {
                                node.OutDataPins.Add(new DataPinDefinition
                                {
                                    Id = Guid.NewGuid(),
                                    DisplayName = "Current",
                                    Name = "OutPinCurrent",
                                    Type = method.ReturnType.GetElementType(),
                                    IsGeneric = false,
                                    PinDirection = PinDirectionDefinition.Out
                                });
                            }
                            else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(method.ReturnType) && method.ReturnType.GenericTypeArguments.Any())
                            {
                                node.OutDataPins.Add(new DataPinDefinition
                                {
                                    Id = Guid.NewGuid(),
                                    DisplayName = "Current",
                                    Name = "OutPinCurrent",
                                    Type = method.ReturnType.GetGenericArguments()[0],
                                    IsGeneric = false,
                                    PinDirection = PinDirectionDefinition.Out
                                });
                            }
                        }
                    }

                    var signature = new StringBuilder();
                    var displaySignature = new StringBuilder();
                    foreach (var parameter in method.GetParameters())
                    {       

                        var parameterName = FirstCharToUpper(parameter.Name);

                        if (forbiddenReturnTypes.Contains(parameter.ParameterType))
                            skip = true;

                        Console.WriteLine($" -> {parameter.Name}:{parameter.ParameterType} `{parameter.DefaultValue}`");

                        if (parameter.IsOut)
                        {
                            node.OutDataPins.Add(new DataPinDefinition
                            {
                                Id = Guid.NewGuid(),
                                DisplayName = parameterName,
                                Name = $"OutParameterPin{parameterName}",
                                Type = parameter.ParameterType,
                                IsGeneric = false,
                                PinDirection = PinDirectionDefinition.Out
                            });
                        }
                        else
                        {
                            node.InDataPins.Add(new DataPinDefinition
                            {
                                Id = Guid.NewGuid(),
                                DisplayName = parameterName,
                                Name = $"InPin{parameterName}",
                                Type = parameter.ParameterType,
                                IsGeneric = false,
                                PinDirection = PinDirectionDefinition.In
                            });
                        }

                        signature.Append($"_{CleanName(parameter.ParameterType.Name)}");

                        if (displaySignature.Length > 0)
                            displaySignature.Append(",");

                        displaySignature.Append(parameter.ParameterType.Name);
                    }

                    if (method.ReturnType == typeof(System.Boolean))
                    {
                        node.OutFlowPins.Add(new FlowPinDefinition
                        {
                            AllowMultiple = false,
                            DisplayName = "True",
                            Id = Guid.NewGuid(),
                            Name = "OutNodeTrue",
                            PinDirection = PinDirectionDefinition.Out
                        });

                        node.OutFlowPins.Add(new FlowPinDefinition
                        {
                            AllowMultiple = false,
                            DisplayName = "False",
                            Id = Guid.NewGuid(),
                            Name = "OutNodeFalse",
                            PinDirection = PinDirectionDefinition.Out
                        });
                    }

                    node.Name = $"{CleanName(type.Namespace)}{type.Name}{methodName}{signature}";
                    node.Tooltip = $"{type.Namespace}{type.Name}{methodName}{signature}";
                    node.DisplayName = $"{methodName}({displaySignature})";
                    node.MethodName = methodName;
                    node.Namespace = type.Namespace;

                    node.Category = $"System/{type.Name}";

                    if (skip)
                        Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    else
                        typeNodes.Add(node);
                }

                foreach (var staticProperty in type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
                {
                    var node = new ActionNodeGenerationDefinition();

                    node.OutFlowPins.Add(new FlowPinDefinition
                    {
                        AllowMultiple = false,
                        DisplayName = "Success",
                        Id = Guid.NewGuid(),
                        Name = "OutNodeSuccess",
                        PinDirection = PinDirectionDefinition.Out
                    });

                    node.OutFlowPins.Add(new FlowPinDefinition
                    {
                        AllowMultiple = false,
                        DisplayName = "Failed",
                        Id = Guid.NewGuid(),
                        Name = "OutNodeFailed",
                        PinDirection = PinDirectionDefinition.Out
                    });

                    node.OutDataPins.Add(new DataPinDefinition
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "Value",
                        Name = "OutPinStaticValue",
                        Type = staticProperty.PropertyType,
                        IsGeneric = false,
                        PinDirection = PinDirectionDefinition.Out
                    });

                    foreach (var subProperty in staticProperty.PropertyType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                    {
                        node.OutDataPins.Add(new DataPinDefinition
                        {
                            Id = Guid.NewGuid(),
                            DisplayName = subProperty.Name,
                            Name = $"OutPinSub{subProperty.Name}",
                            Type = subProperty.PropertyType,
                            IsGeneric = false,
                            PinDirection = PinDirectionDefinition.Out
                        });
                    }

                    node.Name = $"{CleanName(type.Namespace)}{type.Name}{staticProperty.Name}";
                    node.Tooltip = $"{type.Namespace}{type.Name}{staticProperty.Name}";
                    node.DisplayName = $"{type.Name}.{staticProperty.Name}";
                    node.MethodName = staticProperty.Name;
                    node.Namespace = type.Namespace;

                    node.Category = $"System/{type.Name}";


                    typeNodes.Add(node);
                }

                // Check if file exists then create
                if (!File.Exists($"C:\\dev\\{type.Namespace}.{type.Name}.actionNodes.json"))
                {
                    var nodeJson = JsonConvert.SerializeObject(typeNodes);
                    Directory.CreateDirectory($"C:\\dev\\{type.Namespace}.{type.Name}");
                    File.WriteAllText($"C:\\dev\\{type.Namespace}.{type.Name}\\{type.Namespace}.{type.Name}.actionNodes.json", nodeJson);
                    var nodeRegistration = string.Empty;
                    Console.WriteLine(Generate(nodeJson, out nodeRegistration));
                    allNodeRegistration.Append(nodeRegistration);
                }

            }
            var registration = allNodeRegistration.ToString();
            File.WriteAllText($"C:\\dev\\registration.txt", registration);
            Console.ReadLine();
        }

        public static string CleanName(string name)
        {
            return Regex.Replace(name, @"[^A-Za-z0-9]+", "_");
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static List<string> Generate(string jsonNode, out string registration)
        {

            if (string.IsNullOrWhiteSpace(jsonNode))
            {
                Console.WriteLine("string empty");
                registration = null;
                return null;
            }
            var nodeRegistration = new StringBuilder();

            var definitions = JsonConvert.DeserializeObject<JsonNodeDefinition[]>(jsonNode);
            var nodeDefinitions = new List<string>();
            foreach (var definition in definitions)
            {
                var namespaceName = $"{definition.Namespace}.{definition.Category.Split('/').Last()}";
                var className = definition.Name;
                var code = new StringBuilder();
                nodeRegistration.Append($@"
unityContainer.RegisterType<INodeResolver, GenericNodeResolver<{className}>>(nameof({className}));");
                code.Append($@"// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{{  
    [ActionNodeDefinition(Name = nameof({className}), DisplayName = ""{definition.DisplayName}"", Category = ""{definition.Category}"")]
    public class {className} : ActionNode 
    {{ 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        {{ 
            try
            {{
                ");

                if (definition.OutDataPins != null && definition.OutDataPins.Any())
                {
                    code.Append($"var returnValue = {namespaceName}.{definition.MethodName}");

                    if (!definition.OutDataPins.Any(p => p.Name.StartsWith("OutPinStaticValue")))
                    {
                        code.Append("(");
                    }
                    
                }
                else
                {
                    code.Append($"{ namespaceName}.{ definition.MethodName}(");
                }

                if (definition.InDataPins.Any())
                {
                    for (int i = 0; i < definition.InDataPins.Count(); i++)
                    {
                        code.Append($@"
                scope.GetValue<{(
                definition.InDataPins[i].Type.GenericTypeArguments.Any() ?
                "System.Collections.Generic.IEnumerable<" + definition.InDataPins[i].Type.GetGenericArguments()[0].FullName.Replace("&", "") + " > " :
                definition.InDataPins[i].Type.FullName.Replace("&", ""))}>({definition.InDataPins[i].Name})");

                        if (definition.InDataPins.Last() == definition.InDataPins[i])
                        {
                            if (definition.OutDataPins.Any(p => p.Name.StartsWith("OutParameterPin")))
                            {
                                foreach (var dataOutPinDefinition in definition.OutDataPins.Where(p => p.Name.StartsWith("OutParameterPin")))
                                {
                                    code.Append($@"
                , out {(
                dataOutPinDefinition.Type.GenericTypeArguments.Any() ?
                "System.Collections.Generic.IEnumerable<" + definition.InDataPins[i].Type.GetGenericArguments()[0].FullName.Replace("&", "") + " > " :
                dataOutPinDefinition.Type.FullName.Replace("&", ""))} {dataOutPinDefinition.DisplayName}var");
                                }
                            }
                            
                            code.Append(@");");
                        }
                        else
                        {
                            code.Append(",");
                        }
                    }

                }
                else
                {
                    if (!definition.OutDataPins.Any(p => p.Name.StartsWith("OutPinStaticValue")))
                    {
                        code.Append(")");
                    }
                    code.Append(";");
                }

                if (definition.OutDataPins != null && definition.OutDataPins.Any())
                {
                    code.Append($@"
                scope.SetValue({definition.OutDataPins.First(p => p.Name.StartsWith("OutPin")).Name}, returnValue);
");
                }

                if (definition.OutDataPins != null && definition.OutDataPins.Any(p => p.Name.StartsWith("OutParameterPin")))
                {
                    foreach (var dataOutPinDefinition in definition.OutDataPins)
                        if (dataOutPinDefinition.Name.StartsWith("OutParameterPin"))
                        {
                            code.Append($@"
                scope.SetValue({dataOutPinDefinition.Name}, {dataOutPinDefinition.DisplayName}var);");
                        }
                }
                if (definition.OutFlowPins.Any(p => p.Name == "OutNodeEachItem") && definition.OutFlowPins.Any(p => p.Name == "OutNodeCurrent"))
                {
                    code.Append($@"
                foreach (var item in returnValue)
                {{
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinCurrent, item);

                    if (OutNodeEachItem != null)
                        runtime.EnqueueNode(OutNodeEachItem, childScope);
                }}
                    ");
                }

                if (definition.OutFlowPins != null && definition.OutFlowPins.Any(p => p.Name == "True") && definition.OutFlowPins.Any(p => p.Name == "False"))
                {
                    code.Append($@"
                if (OutNodeTrue != null)
                {{
                    runtime.EnqueueNode(OutNodeTrue, scope);
                }} 
                else if (OutNodeFalse != null)
                {{
                    runtime.EnqueueNode(OutNodeFalse, scope);
                }}
                    ");
                }
                code.Append($@"
                if (OutNodeSuccess != null) 
                {{
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }}
            }}
            catch (Exception ex) 
            {{
                Simplic.Log.LogManagerInstance.Instance.Error(""Error in {className}: "", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }}
            return true; 
        }}  

        public override string Name => nameof({className}); 
        public override string FriendlyName => nameof({className}); 
");
                if (definition.InFlowPins != null)
                {
                    foreach (var flowInPinDefinition in definition.InFlowPins)
                    {
                        code.Append($@"
        [FlowPinDefinition(
        PinDirection = PinDirection.In, 
        DisplayName = ""{flowInPinDefinition.DisplayName}"", 
        Name = nameof({flowInPinDefinition.Name}), 
        Id = ""{flowInPinDefinition.Id}"",
        AllowMultiple = {flowInPinDefinition.AllowMultiple.ToString().ToLower()})] 
        public ActionNode {flowInPinDefinition.Name} {{ get; set; }}
"
                        );
                    }
                }

                if (definition.OutFlowPins != null)
                {
                    foreach (var flowOutPinDefinition in definition.OutFlowPins)
                    {
                        code.Append($@"
        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = ""{flowOutPinDefinition.DisplayName}"", 
        Name = nameof({flowOutPinDefinition.Name}), 
        AllowMultiple = {flowOutPinDefinition.AllowMultiple.ToString().ToLower()})] 
        public ActionNode {flowOutPinDefinition.Name} {{ get; set; }} 
"
                        );
                    }
                }

                if (definition.InDataPins != null)
                {
                    foreach (var dataInPinDefinition in definition.InDataPins)
                    {
                        code.Append($@"
        [DataPinDefinition(
        Id = ""{dataInPinDefinition.Id}"",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof({(dataInPinDefinition.Type.GenericTypeArguments.Any() ? "System.Collections.Generic.IEnumerable<" + dataInPinDefinition.Type.GetGenericArguments()[0].FullName + ">" : dataInPinDefinition.Type.Namespace + "." + dataInPinDefinition.Type.Name).Replace("&", "")}),
        Direction = PinDirection.In,
        Name = nameof({dataInPinDefinition.Name}),
        DisplayName = ""{dataInPinDefinition.DisplayName}"",
        IsGeneric = {dataInPinDefinition.IsGeneric.ToString().ToLower()},
        AllowedTypes = {dataInPinDefinition.AllowedTypes ?? "null"})]
        public DataPin {dataInPinDefinition.Name} {{ get; set; }} 
"
                        );
                    }
                }

                if (definition.OutDataPins != null)
                {
                    foreach (var dataOutPinDefinition in definition.OutDataPins)
                    {
                        code.Append($@"
        [DataPinDefinition(
        Id = ""{dataOutPinDefinition.Id}"",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof({(dataOutPinDefinition.Type.GenericTypeArguments.Any() ? "System.Collections.Generic.IEnumerable<" + dataOutPinDefinition.Type.GetGenericArguments()[0].FullName + ">" : dataOutPinDefinition.Type.Namespace + "." + dataOutPinDefinition.Type.Name).Replace("&", "")}),
        Direction = PinDirection.Out,
        Name = nameof({dataOutPinDefinition.Name}),
        DisplayName = ""{dataOutPinDefinition.DisplayName}"",
        IsGeneric = {dataOutPinDefinition.IsGeneric.ToString().ToLower()},
        AllowedTypes = {dataOutPinDefinition.AllowedTypes ?? "null"})]
        public DataPin {dataOutPinDefinition.Name} {{ get; set; }} 
"

                        );
                    }
                }

                code.Append(@"
    }
}");
                if (!File.Exists($"C:\\dev\\{namespaceName}\\{FirstCharToUpper(className)}Node.cs"))
                {
                    File.WriteAllText($"C:\\dev\\{namespaceName}\\{FirstCharToUpper(className)}Node.cs", code.ToString());
                }
                nodeDefinitions.Add(code.ToString());

            }
            registration = nodeRegistration.ToString();
            return nodeDefinitions;
        }
    }
}
