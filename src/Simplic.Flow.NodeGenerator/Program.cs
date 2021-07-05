using Newtonsoft.Json;
using Simplic.Flow.Editor.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
               typeof(Regex),
               typeof(Console)
            };

            var forbiddenReturnTypes = new List<Type>
            {
                typeof(Stream), typeof(StreamReader), typeof(StreamWriter), typeof(MemoryStream),
                typeof(TextReader), typeof(TextWriter)
            };

            var actionNodes = new List<ActionNodeDefinition>();

            foreach (var type in types)
            {
                // Get all static and public methods
                foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .OrderBy(x => x.Name).ThenBy(x => x.GetParameters().Count()))
                {
                    var skip = false;

                    var node = new ActionNodeDefinition();

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

                    // Do not create iterable for byte-array
                    if (method.ReturnType != typeof(byte[]) && method.ReturnType != typeof(string))
                    {
                        if (method.ReturnType.IsArray || typeof(System.Collections.IEnumerable).IsAssignableFrom(method.ReturnType))
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

                            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(method.ReturnType) && method.ReturnType.GenericTypeArguments.Any())
                            {
                                node.OutDataPins.Add(new DataPinDefinition
                                {
                                    Id = Guid.NewGuid(),
                                    DisplayName = "Current",
                                    Name = "OutPinCurrent",
                                    Type = method.ReturnType.GenericTypeArguments[0],
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
                        if (forbiddenReturnTypes.Contains(parameter.ParameterType))
                            skip = true;

                        // TODO: Support out-parameter
                        if (parameter.IsOut)
                            skip = true;

                        Console.WriteLine($" -> {parameter.Name}:{parameter.ParameterType} `{parameter.DefaultValue}`");

                        var parameterName = FirstCharToUpper(parameter.Name);

                        node.InDataPins.Add(new DataPinDefinition
                        {
                            Id = Guid.NewGuid(),
                            DisplayName = parameterName,
                            Name = $"OutPin{parameterName}",
                            Type = parameter.ParameterType,
                            IsGeneric = false,
                            PinDirection = PinDirectionDefinition.In
                        });

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

                    node.Name = $"{CleanName(type.Namespace)}{type.Name}{method.Name}{signature}";
                    node.Tooltip = $"{type.Namespace}{type.Name}{method.Name}{signature}";
                    node.DisplayName = $"{method.Name}({displaySignature})";

                    node.Category = $"System/{type.Name}";

                    if (skip)
                        Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    else
                        actionNodes.Add(node);
                }

                foreach (var staticProperty in type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
                {
                    var node = new ActionNodeDefinition();

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
                        Name = "OutPinValue",
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

                    node.Category = $"System/{type.Name}";


                    actionNodes.Add(node);
                }
            }

            var nodeJson = JsonConvert.SerializeObject(actionNodes);
            File.WriteAllText("C:\\dev\\actionNodes.json", nodeJson);

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
    }
}
