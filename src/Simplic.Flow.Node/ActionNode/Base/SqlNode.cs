using CommonServiceLocator;
using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simplic.Sql;
using Dapper;
using Microsoft.SqlServer.Server;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "Sql Select Statement", Name = nameof(SqlNode), Category = "Common")]
    public class SqlNode : ActionNode
    {
        private readonly ISqlService sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>();

        /// <summary>
        /// Gets all object from the sql statement.
        /// </summary>
        /// <param name="sql">The string with the select statement.</param>
        /// <returns>Returns a enumerable of of all objects of the table.</returns>
        private IEnumerable<object> GetAllTableData(string sql)
        {
            return sqlService.OpenConnection((connection) =>
            {
                return connection.Query<object>(sql);
            });
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="runtime">Runtime instance.</param>
        /// <param name="scope">Scope instance.</param>
        /// <returns>True or false, if the table name is not specified.</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                var eventService = ServiceLocator.Current.GetInstance<IFlowEventService>();

                var tableName = scope.GetValue<string>(InPinTableName);

                if (string.IsNullOrWhiteSpace(tableName))
                {
                    Console.WriteLine($"No table name is set in {nameof(SqlNode)}");
                    return false;
                }

                var column1 = scope.GetValue<string>(InPinColumn01);
                var column2 = scope.GetValue<string>(InPinColumn02);
                var column3 = scope.GetValue<string>(InPinColumn03);

                var parameter1 = scope.GetValue<string>(InPinParameter01);
                var parameter2 = scope.GetValue<string>(InPinParameter02);
                var parameter3 = scope.GetValue<string>(InPinParameter03);

                // Start with the basic SELECT statement.
                string sql = $"SELECT * FROM {tableName}";

                // Create the WHERE condition depending on the existing parameters.
                List<string> conditions = new List<string>();

                if (!string.IsNullOrEmpty(column1) && !string.IsNullOrEmpty(parameter1))
                {
                    conditions.Add($"{column1} = '{parameter1}'");
                }

                if (!string.IsNullOrEmpty(column2) && !string.IsNullOrEmpty(parameter2))
                {
                    conditions.Add($"{column2} = '{parameter2}'");
                }

                if (!string.IsNullOrEmpty(column3) && !string.IsNullOrEmpty(parameter3))
                {
                    conditions.Add($"{column3} = '{parameter3}'");
                }

                // Add the WHERE clause to the SQL statement if there are clauses.
                if (conditions.Count > 0)
                {
                    sql += " WHERE " + string.Join(" AND ", conditions);
                }

                var tableData = GetAllTableData(sql);
                if (tableData != null)
                {
                    scope.SetValue(OutPinSelectedData, tableData);
                }

                if (SuccessOutNode != null)
                    runtime.EnqueueNode(SuccessOutNode, scope);
            }
            catch
            {
                if (FailedOutNode != null)
                    runtime.EnqueueNode(FailedOutNode, scope);
            }

            return true;
        }

        /// <summary>
        /// Gets or sets the flow out node.
        /// </summary>
        [FlowPinDefinition(DisplayName = "Success", Name = "SuccessOutNode", PinDirection = PinDirection.Out)]
        public ActionNode SuccessOutNode { get; set; }

        /// <summary>
        /// Gets or sets the flow out node.
        /// </summary>
        [FlowPinDefinition(DisplayName = "Failed", Name = "FailedOutNode", PinDirection = PinDirection.Out)]
        public ActionNode FailedOutNode { get; set; }

        /// <summary>
        /// Gets or sets the pin node for the table name.
        /// </summary>
        [DataPinDefinition(
            Id = "ebe80936-81cf-4db9-b5d2-b2311ad3d5fc",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinTableName),
            DisplayName = "Tabellenname")]
        public DataPin InPinTableName { get; set; }
        

        /// <summary>
        /// Gets or sets the pin node for the first column.
        /// </summary>
        [DataPinDefinition(
            Id = "621a5ae3-9f6c-48d1-8976-9ee7d13c04f0",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinColumn01),
            DisplayName = "Spalte 01")]
        public DataPin InPinColumn01 { get; set; }
        
        /// <summary>
        /// Gets or sets the pin node for the second column.
        /// </summary>
        [DataPinDefinition(
            Id = "643ab06b-da8e-400f-8c83-df06f4262e53",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinColumn02),
            DisplayName = "Spalte 02")]
        public DataPin InPinColumn02 { get; set; }

        /// <summary>
        /// Gets or sets the pin node for the third column.
        /// </summary>
        [DataPinDefinition(
            Id = "201544fd-5040-402f-af06-a62a49748f57",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinColumn03),
            DisplayName = "Spalte 03")]
        public DataPin InPinColumn03 { get; set; }

        /// <summary>
        /// Gets or sets the pin node for the first parameter.
        /// </summary>
        [DataPinDefinition(
            Id = "676b24e3-6603-46cf-bfac-68ea95c4fb6e",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinParameter01),
            DisplayName = "Parameter 01")]
        public DataPin InPinParameter01 { get; set; }

        /// <summary>
        /// Gets or sets the pin node for the second parameter.
        /// </summary>
        [DataPinDefinition(
            Id = "e2013ad9-c2c8-41eb-8f12-60301d254e48",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinParameter02),
            DisplayName = "Parameter 02")]
        public DataPin InPinParameter02 { get; set; }

        /// <summary>
        /// Gets or sets the pin node for the third parameter.
        /// </summary>
        [DataPinDefinition(
            Id = "f3e143db-6ba0-426b-b8d7-9d9e1f92d4af",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinParameter03),
            DisplayName = "Parameter 03")]
        public DataPin InPinParameter03 { get; set; }

        /// <summary>
        /// Gets or sets the out pin for the selected data.
        /// </summary>
        [DataPinDefinition(
            Id = "1ce10e98-a73a-4187-85df-7be4825be48d",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = "OutPinSelectedData",
            DisplayName = "Ausgewählte Daten")]
        public DataPin OutPinSelectedData { get; set; }

        /// <summary>
        /// Gets or sets the friendly name.
        /// </summary>
        public override string FriendlyName { get { return nameof(SqlNode); } }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name { get { return nameof(SqlNode); } }
    }
}