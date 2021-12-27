using Newtonsoft.Json;
using Simplic.Collections.Generic;
using Simplic.Configuration;
using Simplic.Flow.Configuration;
using Simplic.Flow.Event;
using Simplic.Flow.EventQueue;
using Simplic.Flow.Log;
using Simplic.Flow.Node;
using Simplic.Flow.Node.IO;
using Simplic.FlowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Flow.Service
{
    public class FlowService : IFlowService
    {
        #region Events
        private event EventHandler OnStarted;
        private event EventHandler OnCompleted;
        private object eventLock = new Object();

        event EventHandler IFlowService.OnStarted
        {
            add
            {
                lock (eventLock)
                {
                    OnStarted += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    OnStarted -= value;
                }
            }
        }

        event EventHandler IFlowService.OnCompleted
        {
            add
            {
                lock (eventLock)
                {
                    OnCompleted += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    OnCompleted -= value;
                }
            }
        }
        #endregion

        #region Private Members                
        private IList<FlowConfiguration> flowConfigurations;
        private IList<ActiveFlow.ActiveFlow> finishedInstances = new List<ActiveFlow.ActiveFlow>();
        private ConcurrentList<ThreadStateInfo> executions = new ConcurrentList<ThreadStateInfo>();
        private IDictionary<string, IList<EventDelegate>> eventDelegates;
        private Dequeue<EventCall> eventQueue;

        private readonly IUnityContainer unityContainer;
        private readonly IFlowInstanceService flowInstanceService;
        private readonly IFlowConfigurationService flowConfigurationService;
        private readonly IFlowEventQueueService flowEventQueueService;
        private readonly IFlowEventService flowEventService;
        private readonly IFlowLogService flowLogService;
        private readonly IConfigurationService configurationService;

        private string machineName;
        private string serviceName;
        private const int defaultThreadCount = 4;
        private int threadCount = defaultThreadCount;
        #endregion

        #region Constructor
        public FlowService(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;

            Flows = new List<Flow>();
            ActiveFlows = new List<Simplic.ActiveFlow.ActiveFlow>();
            eventQueue = new Dequeue<EventCall>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();

            this.flowInstanceService = unityContainer.Resolve<IFlowInstanceService>();
            this.flowConfigurationService = unityContainer.Resolve<IFlowConfigurationService>();
            this.flowEventQueueService = unityContainer.Resolve<IFlowEventQueueService>();
            this.flowEventService = unityContainer.Resolve<IFlowEventService>();
            this.flowLogService = unityContainer.Resolve<IFlowLogService>();
            this.configurationService = unityContainer.Resolve<IConfigurationService>();

            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ConsoleWriteLineNode>>("ConsoleWriteLineNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ForeachNode>>("ForeachNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SequenceNode>>("SequenceNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<StartWithConditionNode>>("StartWithConditionNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<EndWithConditionNode>>("EndWithConditionNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<DeleteFileNode>>("DeleteFileNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<GetDirectoryContentNode>>(nameof(GetDirectoryContentNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<OnCheckDirectoryContentNode>>(nameof(OnCheckDirectoryContentNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<GetFileExtensionNode>>(nameof(GetFileExtensionNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<GetVariableNode>>(nameof(GetVariableNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SetVariableNode>>(nameof(SetVariableNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<AddNode>>(nameof(AddNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<DivideNode>>(nameof(DivideNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<MultiplyNode>>(nameof(MultiplyNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SubtractNode>>(nameof(SubtractNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ReadAllTextNode>>(nameof(ReadAllTextNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ReadAllBytesNode>>(nameof(ReadAllBytesNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ClearPinNode>>(nameof(ClearPinNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ThreadSleepNode>>(nameof(ThreadSleepNode));

            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ToStringNode>>(nameof(ToStringNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<DateTimeNowNode>>(nameof(DateTimeNowNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ConcatStringNode>>(nameof(ConcatStringNode));

            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ExecuteFlowNode>>(nameof(ExecuteFlowNode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<OnExecuteFlowEvent>>(nameof(OnExecuteFlowEvent));

            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ProcessStartNode>>(nameof(ProcessStartNode));

            // Generated node register
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryCreateDirectory_String>>(nameof(System_IODirectoryCreateDirectory_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryCreateDirectory_String_DirectorySecurity>>(nameof(System_IODirectoryCreateDirectory_String_DirectorySecurity));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryDelete_String>>(nameof(System_IODirectoryDelete_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryDelete_String_Boolean>>(nameof(System_IODirectoryDelete_String_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateDirectories_String>>(nameof(System_IODirectoryEnumerateDirectories_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateDirectories_String_String>>(nameof(System_IODirectoryEnumerateDirectories_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateDirectories_String_String_SearchOption>>(nameof(System_IODirectoryEnumerateDirectories_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFiles_String>>(nameof(System_IODirectoryEnumerateFiles_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFiles_String_String>>(nameof(System_IODirectoryEnumerateFiles_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFiles_String_String_SearchOption>>(nameof(System_IODirectoryEnumerateFiles_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFileSystemEntries_String>>(nameof(System_IODirectoryEnumerateFileSystemEntries_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFileSystemEntries_String_String>>(nameof(System_IODirectoryEnumerateFileSystemEntries_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryEnumerateFileSystemEntries_String_String_SearchOption>>(nameof(System_IODirectoryEnumerateFileSystemEntries_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryExists_String>>(nameof(System_IODirectoryExists_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetAccessControl_String>>(nameof(System_IODirectoryGetAccessControl_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetAccessControl_String_AccessControlSections>>(nameof(System_IODirectoryGetAccessControl_String_AccessControlSections));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetCreationTime_String>>(nameof(System_IODirectoryGetCreationTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetCreationTimeUtc_String>>(nameof(System_IODirectoryGetCreationTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetCurrentDirectory>>(nameof(System_IODirectoryGetCurrentDirectory));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetDirectories_String>>(nameof(System_IODirectoryGetDirectories_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetDirectories_String_String>>(nameof(System_IODirectoryGetDirectories_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetDirectories_String_String_SearchOption>>(nameof(System_IODirectoryGetDirectories_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetDirectoryRoot_String>>(nameof(System_IODirectoryGetDirectoryRoot_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFiles_String>>(nameof(System_IODirectoryGetFiles_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFiles_String_String>>(nameof(System_IODirectoryGetFiles_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFiles_String_String_SearchOption>>(nameof(System_IODirectoryGetFiles_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFileSystemEntries_String>>(nameof(System_IODirectoryGetFileSystemEntries_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFileSystemEntries_String_String>>(nameof(System_IODirectoryGetFileSystemEntries_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetFileSystemEntries_String_String_SearchOption>>(nameof(System_IODirectoryGetFileSystemEntries_String_String_SearchOption));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetLastAccessTime_String>>(nameof(System_IODirectoryGetLastAccessTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetLastAccessTimeUtc_String>>(nameof(System_IODirectoryGetLastAccessTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetLastWriteTime_String>>(nameof(System_IODirectoryGetLastWriteTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetLastWriteTimeUtc_String>>(nameof(System_IODirectoryGetLastWriteTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetLogicalDrives>>(nameof(System_IODirectoryGetLogicalDrives));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryGetParent_String>>(nameof(System_IODirectoryGetParent_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectoryMove_String_String>>(nameof(System_IODirectoryMove_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetAccessControl_String_DirectorySecurity>>(nameof(System_IODirectorySetAccessControl_String_DirectorySecurity));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetCreationTime_String_DateTime>>(nameof(System_IODirectorySetCreationTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetCreationTimeUtc_String_DateTime>>(nameof(System_IODirectorySetCreationTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetCurrentDirectory_String>>(nameof(System_IODirectorySetCurrentDirectory_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetLastAccessTime_String_DateTime>>(nameof(System_IODirectorySetLastAccessTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetLastAccessTimeUtc_String_DateTime>>(nameof(System_IODirectorySetLastAccessTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetLastWriteTime_String_DateTime>>(nameof(System_IODirectorySetLastWriteTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IODirectorySetLastWriteTimeUtc_String_DateTime>>(nameof(System_IODirectorySetLastWriteTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathChangeExtension_String_String>>(nameof(System_IOPathChangeExtension_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathCombine_String_>>(nameof(System_IOPathCombine_String_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathCombine_String_String>>(nameof(System_IOPathCombine_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathCombine_String_String_String>>(nameof(System_IOPathCombine_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathCombine_String_String_String_String>>(nameof(System_IOPathCombine_String_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetDirectoryName_String>>(nameof(System_IOPathGetDirectoryName_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetExtension_String>>(nameof(System_IOPathGetExtension_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetFileName_String>>(nameof(System_IOPathGetFileName_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetFileNameWithoutExtension_String>>(nameof(System_IOPathGetFileNameWithoutExtension_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetFullPath_String>>(nameof(System_IOPathGetFullPath_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetInvalidFileNameChars>>(nameof(System_IOPathGetInvalidFileNameChars));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetInvalidPathChars>>(nameof(System_IOPathGetInvalidPathChars));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetPathRoot_String>>(nameof(System_IOPathGetPathRoot_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetRandomFileName>>(nameof(System_IOPathGetRandomFileName));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetTempFileName>>(nameof(System_IOPathGetTempFileName));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathGetTempPath>>(nameof(System_IOPathGetTempPath));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathHasExtension_String>>(nameof(System_IOPathHasExtension_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOPathIsPathRooted_String>>(nameof(System_IOPathIsPathRooted_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileAppendAllLines_String_IEnumerable_1>>(nameof(System_IOFileAppendAllLines_String_IEnumerable_1));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileAppendAllLines_String_IEnumerable_1_Encoding>>(nameof(System_IOFileAppendAllLines_String_IEnumerable_1_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileAppendAllText_String_String>>(nameof(System_IOFileAppendAllText_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileAppendAllText_String_String_Encoding>>(nameof(System_IOFileAppendAllText_String_String_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCopy_String_String>>(nameof(System_IOFileCopy_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCopy_String_String_Boolean>>(nameof(System_IOFileCopy_String_String_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCreate_String>>(nameof(System_IOFileCreate_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCreate_String_Int32>>(nameof(System_IOFileCreate_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCreate_String_Int32_FileOptions>>(nameof(System_IOFileCreate_String_Int32_FileOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileCreate_String_Int32_FileOptions_FileSecurity>>(nameof(System_IOFileCreate_String_Int32_FileOptions_FileSecurity));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileDecrypt_String>>(nameof(System_IOFileDecrypt_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileDelete_String>>(nameof(System_IOFileDelete_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileEncrypt_String>>(nameof(System_IOFileEncrypt_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileExists_String>>(nameof(System_IOFileExists_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetAccessControl_String>>(nameof(System_IOFileGetAccessControl_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetAccessControl_String_AccessControlSections>>(nameof(System_IOFileGetAccessControl_String_AccessControlSections));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetAttributes_String>>(nameof(System_IOFileGetAttributes_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetCreationTime_String>>(nameof(System_IOFileGetCreationTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetCreationTimeUtc_String>>(nameof(System_IOFileGetCreationTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetLastAccessTime_String>>(nameof(System_IOFileGetLastAccessTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetLastAccessTimeUtc_String>>(nameof(System_IOFileGetLastAccessTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetLastWriteTime_String>>(nameof(System_IOFileGetLastWriteTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileGetLastWriteTimeUtc_String>>(nameof(System_IOFileGetLastWriteTimeUtc_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileMove_String_String>>(nameof(System_IOFileMove_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileOpen_String_FileMode>>(nameof(System_IOFileOpen_String_FileMode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileOpen_String_FileMode_FileAccess>>(nameof(System_IOFileOpen_String_FileMode_FileAccess));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileOpen_String_FileMode_FileAccess_FileShare>>(nameof(System_IOFileOpen_String_FileMode_FileAccess_FileShare));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileOpenRead_String>>(nameof(System_IOFileOpenRead_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileOpenWrite_String>>(nameof(System_IOFileOpenWrite_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadAllBytes_String>>(nameof(System_IOFileReadAllBytes_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadAllLines_String>>(nameof(System_IOFileReadAllLines_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadAllLines_String_Encoding>>(nameof(System_IOFileReadAllLines_String_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadAllText_String>>(nameof(System_IOFileReadAllText_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadAllText_String_Encoding>>(nameof(System_IOFileReadAllText_String_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadLines_String>>(nameof(System_IOFileReadLines_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReadLines_String_Encoding>>(nameof(System_IOFileReadLines_String_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReplace_String_String_String>>(nameof(System_IOFileReplace_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileReplace_String_String_String_Boolean>>(nameof(System_IOFileReplace_String_String_String_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetAccessControl_String_FileSecurity>>(nameof(System_IOFileSetAccessControl_String_FileSecurity));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetAttributes_String_FileAttributes>>(nameof(System_IOFileSetAttributes_String_FileAttributes));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetCreationTime_String_DateTime>>(nameof(System_IOFileSetCreationTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetCreationTimeUtc_String_DateTime>>(nameof(System_IOFileSetCreationTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetLastAccessTime_String_DateTime>>(nameof(System_IOFileSetLastAccessTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetLastAccessTimeUtc_String_DateTime>>(nameof(System_IOFileSetLastAccessTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetLastWriteTime_String_DateTime>>(nameof(System_IOFileSetLastWriteTime_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileSetLastWriteTimeUtc_String_DateTime>>(nameof(System_IOFileSetLastWriteTimeUtc_String_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllBytes_String_Byte_>>(nameof(System_IOFileWriteAllBytes_String_Byte_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllLines_String_String_>>(nameof(System_IOFileWriteAllLines_String_String_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllLines_String_IEnumerable_1>>(nameof(System_IOFileWriteAllLines_String_IEnumerable_1));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllLines_String_String__Encoding>>(nameof(System_IOFileWriteAllLines_String_String__Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllLines_String_IEnumerable_1_Encoding>>(nameof(System_IOFileWriteAllLines_String_IEnumerable_1_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllText_String_String>>(nameof(System_IOFileWriteAllText_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_IOFileWriteAllText_String_String_Encoding>>(nameof(System_IOFileWriteAllText_String_String_Encoding));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertChangeType_Object_TypeCode>>(nameof(SystemConvertChangeType_Object_TypeCode));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertChangeType_Object_Type>>(nameof(SystemConvertChangeType_Object_Type));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertChangeType_Object_TypeCode_IFormatProvider>>(nameof(SystemConvertChangeType_Object_TypeCode_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertChangeType_Object_Type_IFormatProvider>>(nameof(SystemConvertChangeType_Object_Type_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertFromBase64CharArray_Char__Int32_Int32>>(nameof(SystemConvertFromBase64CharArray_Char__Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertFromBase64String_String>>(nameof(SystemConvertFromBase64String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertGetTypeCode_Object>>(nameof(SystemConvertGetTypeCode_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertIsDBNull_Object>>(nameof(SystemConvertIsDBNull_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32>>(nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions>>(nameof(SystemConvertToBase64CharArray_Byte__Int32_Int32_Char__Int32_Base64FormattingOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64String_Byte_>>(nameof(SystemConvertToBase64String_Byte_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64String_Byte__Base64FormattingOptions>>(nameof(SystemConvertToBase64String_Byte__Base64FormattingOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64String_Byte__Int32_Int32>>(nameof(SystemConvertToBase64String_Byte__Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions>>(nameof(SystemConvertToBase64String_Byte__Int32_Int32_Base64FormattingOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Object>>(nameof(SystemConvertToBoolean_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Boolean>>(nameof(SystemConvertToBoolean_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_SByte>>(nameof(SystemConvertToBoolean_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Char>>(nameof(SystemConvertToBoolean_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Byte>>(nameof(SystemConvertToBoolean_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Int16>>(nameof(SystemConvertToBoolean_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_UInt16>>(nameof(SystemConvertToBoolean_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Int32>>(nameof(SystemConvertToBoolean_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_UInt32>>(nameof(SystemConvertToBoolean_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Int64>>(nameof(SystemConvertToBoolean_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_UInt64>>(nameof(SystemConvertToBoolean_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_String>>(nameof(SystemConvertToBoolean_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Single>>(nameof(SystemConvertToBoolean_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Double>>(nameof(SystemConvertToBoolean_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Decimal>>(nameof(SystemConvertToBoolean_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_DateTime>>(nameof(SystemConvertToBoolean_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_Object_IFormatProvider>>(nameof(SystemConvertToBoolean_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToBoolean_String_IFormatProvider>>(nameof(SystemConvertToBoolean_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Object>>(nameof(SystemConvertToByte_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Boolean>>(nameof(SystemConvertToByte_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Byte>>(nameof(SystemConvertToByte_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Char>>(nameof(SystemConvertToByte_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_SByte>>(nameof(SystemConvertToByte_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Int16>>(nameof(SystemConvertToByte_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_UInt16>>(nameof(SystemConvertToByte_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Int32>>(nameof(SystemConvertToByte_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_UInt32>>(nameof(SystemConvertToByte_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Int64>>(nameof(SystemConvertToByte_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_UInt64>>(nameof(SystemConvertToByte_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Single>>(nameof(SystemConvertToByte_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Double>>(nameof(SystemConvertToByte_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Decimal>>(nameof(SystemConvertToByte_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_String>>(nameof(SystemConvertToByte_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_DateTime>>(nameof(SystemConvertToByte_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_Object_IFormatProvider>>(nameof(SystemConvertToByte_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_String_IFormatProvider>>(nameof(SystemConvertToByte_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToByte_String_Int32>>(nameof(SystemConvertToByte_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Object>>(nameof(SystemConvertToChar_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Boolean>>(nameof(SystemConvertToChar_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Char>>(nameof(SystemConvertToChar_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_SByte>>(nameof(SystemConvertToChar_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Byte>>(nameof(SystemConvertToChar_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Int16>>(nameof(SystemConvertToChar_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_UInt16>>(nameof(SystemConvertToChar_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Int32>>(nameof(SystemConvertToChar_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_UInt32>>(nameof(SystemConvertToChar_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Int64>>(nameof(SystemConvertToChar_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_UInt64>>(nameof(SystemConvertToChar_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_String>>(nameof(SystemConvertToChar_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Single>>(nameof(SystemConvertToChar_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Double>>(nameof(SystemConvertToChar_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Decimal>>(nameof(SystemConvertToChar_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_DateTime>>(nameof(SystemConvertToChar_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_Object_IFormatProvider>>(nameof(SystemConvertToChar_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToChar_String_IFormatProvider>>(nameof(SystemConvertToChar_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_DateTime>>(nameof(SystemConvertToDateTime_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Object>>(nameof(SystemConvertToDateTime_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_String>>(nameof(SystemConvertToDateTime_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_SByte>>(nameof(SystemConvertToDateTime_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Byte>>(nameof(SystemConvertToDateTime_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Int16>>(nameof(SystemConvertToDateTime_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_UInt16>>(nameof(SystemConvertToDateTime_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Int32>>(nameof(SystemConvertToDateTime_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_UInt32>>(nameof(SystemConvertToDateTime_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Int64>>(nameof(SystemConvertToDateTime_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_UInt64>>(nameof(SystemConvertToDateTime_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Boolean>>(nameof(SystemConvertToDateTime_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Char>>(nameof(SystemConvertToDateTime_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Single>>(nameof(SystemConvertToDateTime_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Double>>(nameof(SystemConvertToDateTime_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Decimal>>(nameof(SystemConvertToDateTime_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_Object_IFormatProvider>>(nameof(SystemConvertToDateTime_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDateTime_String_IFormatProvider>>(nameof(SystemConvertToDateTime_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Object>>(nameof(SystemConvertToDecimal_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_SByte>>(nameof(SystemConvertToDecimal_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Byte>>(nameof(SystemConvertToDecimal_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Char>>(nameof(SystemConvertToDecimal_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Int16>>(nameof(SystemConvertToDecimal_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_UInt16>>(nameof(SystemConvertToDecimal_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Int32>>(nameof(SystemConvertToDecimal_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_UInt32>>(nameof(SystemConvertToDecimal_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Int64>>(nameof(SystemConvertToDecimal_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_UInt64>>(nameof(SystemConvertToDecimal_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Single>>(nameof(SystemConvertToDecimal_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Double>>(nameof(SystemConvertToDecimal_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_String>>(nameof(SystemConvertToDecimal_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Decimal>>(nameof(SystemConvertToDecimal_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Boolean>>(nameof(SystemConvertToDecimal_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_DateTime>>(nameof(SystemConvertToDecimal_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_Object_IFormatProvider>>(nameof(SystemConvertToDecimal_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDecimal_String_IFormatProvider>>(nameof(SystemConvertToDecimal_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Object>>(nameof(SystemConvertToDouble_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_SByte>>(nameof(SystemConvertToDouble_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Byte>>(nameof(SystemConvertToDouble_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Int16>>(nameof(SystemConvertToDouble_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Char>>(nameof(SystemConvertToDouble_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_UInt16>>(nameof(SystemConvertToDouble_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Int32>>(nameof(SystemConvertToDouble_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_UInt32>>(nameof(SystemConvertToDouble_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Int64>>(nameof(SystemConvertToDouble_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_UInt64>>(nameof(SystemConvertToDouble_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Single>>(nameof(SystemConvertToDouble_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Double>>(nameof(SystemConvertToDouble_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Decimal>>(nameof(SystemConvertToDouble_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_String>>(nameof(SystemConvertToDouble_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Boolean>>(nameof(SystemConvertToDouble_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_DateTime>>(nameof(SystemConvertToDouble_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_Object_IFormatProvider>>(nameof(SystemConvertToDouble_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToDouble_String_IFormatProvider>>(nameof(SystemConvertToDouble_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Object>>(nameof(SystemConvertToInt16_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Boolean>>(nameof(SystemConvertToInt16_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Char>>(nameof(SystemConvertToInt16_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_SByte>>(nameof(SystemConvertToInt16_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Byte>>(nameof(SystemConvertToInt16_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_UInt16>>(nameof(SystemConvertToInt16_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Int32>>(nameof(SystemConvertToInt16_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_UInt32>>(nameof(SystemConvertToInt16_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Int16>>(nameof(SystemConvertToInt16_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Int64>>(nameof(SystemConvertToInt16_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_UInt64>>(nameof(SystemConvertToInt16_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Single>>(nameof(SystemConvertToInt16_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Double>>(nameof(SystemConvertToInt16_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Decimal>>(nameof(SystemConvertToInt16_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_String>>(nameof(SystemConvertToInt16_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_DateTime>>(nameof(SystemConvertToInt16_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_Object_IFormatProvider>>(nameof(SystemConvertToInt16_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_String_IFormatProvider>>(nameof(SystemConvertToInt16_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt16_String_Int32>>(nameof(SystemConvertToInt16_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Object>>(nameof(SystemConvertToInt32_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Boolean>>(nameof(SystemConvertToInt32_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Char>>(nameof(SystemConvertToInt32_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_SByte>>(nameof(SystemConvertToInt32_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Byte>>(nameof(SystemConvertToInt32_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Int16>>(nameof(SystemConvertToInt32_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_UInt16>>(nameof(SystemConvertToInt32_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_UInt32>>(nameof(SystemConvertToInt32_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Int32>>(nameof(SystemConvertToInt32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Int64>>(nameof(SystemConvertToInt32_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_UInt64>>(nameof(SystemConvertToInt32_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Single>>(nameof(SystemConvertToInt32_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Double>>(nameof(SystemConvertToInt32_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Decimal>>(nameof(SystemConvertToInt32_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_String>>(nameof(SystemConvertToInt32_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_DateTime>>(nameof(SystemConvertToInt32_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_Object_IFormatProvider>>(nameof(SystemConvertToInt32_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_String_IFormatProvider>>(nameof(SystemConvertToInt32_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt32_String_Int32>>(nameof(SystemConvertToInt32_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Object>>(nameof(SystemConvertToInt64_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Boolean>>(nameof(SystemConvertToInt64_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Char>>(nameof(SystemConvertToInt64_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_SByte>>(nameof(SystemConvertToInt64_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Byte>>(nameof(SystemConvertToInt64_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Int16>>(nameof(SystemConvertToInt64_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_UInt16>>(nameof(SystemConvertToInt64_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Int32>>(nameof(SystemConvertToInt64_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_UInt32>>(nameof(SystemConvertToInt64_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_UInt64>>(nameof(SystemConvertToInt64_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Int64>>(nameof(SystemConvertToInt64_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Single>>(nameof(SystemConvertToInt64_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Double>>(nameof(SystemConvertToInt64_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Decimal>>(nameof(SystemConvertToInt64_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_String>>(nameof(SystemConvertToInt64_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_DateTime>>(nameof(SystemConvertToInt64_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_Object_IFormatProvider>>(nameof(SystemConvertToInt64_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_String_IFormatProvider>>(nameof(SystemConvertToInt64_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToInt64_String_Int32>>(nameof(SystemConvertToInt64_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Object>>(nameof(SystemConvertToSByte_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Boolean>>(nameof(SystemConvertToSByte_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_SByte>>(nameof(SystemConvertToSByte_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Char>>(nameof(SystemConvertToSByte_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Byte>>(nameof(SystemConvertToSByte_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Int16>>(nameof(SystemConvertToSByte_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_UInt16>>(nameof(SystemConvertToSByte_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Int32>>(nameof(SystemConvertToSByte_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_UInt32>>(nameof(SystemConvertToSByte_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Int64>>(nameof(SystemConvertToSByte_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_UInt64>>(nameof(SystemConvertToSByte_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Single>>(nameof(SystemConvertToSByte_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Double>>(nameof(SystemConvertToSByte_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Decimal>>(nameof(SystemConvertToSByte_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_String>>(nameof(SystemConvertToSByte_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_DateTime>>(nameof(SystemConvertToSByte_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_Object_IFormatProvider>>(nameof(SystemConvertToSByte_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_String_IFormatProvider>>(nameof(SystemConvertToSByte_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSByte_String_Int32>>(nameof(SystemConvertToSByte_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Object>>(nameof(SystemConvertToSingle_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_SByte>>(nameof(SystemConvertToSingle_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Byte>>(nameof(SystemConvertToSingle_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Char>>(nameof(SystemConvertToSingle_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Int16>>(nameof(SystemConvertToSingle_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_UInt16>>(nameof(SystemConvertToSingle_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Int32>>(nameof(SystemConvertToSingle_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_UInt32>>(nameof(SystemConvertToSingle_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Int64>>(nameof(SystemConvertToSingle_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_UInt64>>(nameof(SystemConvertToSingle_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Single>>(nameof(SystemConvertToSingle_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Double>>(nameof(SystemConvertToSingle_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Decimal>>(nameof(SystemConvertToSingle_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_String>>(nameof(SystemConvertToSingle_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Boolean>>(nameof(SystemConvertToSingle_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_DateTime>>(nameof(SystemConvertToSingle_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_Object_IFormatProvider>>(nameof(SystemConvertToSingle_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToSingle_String_IFormatProvider>>(nameof(SystemConvertToSingle_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Object>>(nameof(SystemConvertToString_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Char>>(nameof(SystemConvertToString_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_SByte>>(nameof(SystemConvertToString_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Byte>>(nameof(SystemConvertToString_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int16>>(nameof(SystemConvertToString_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt16>>(nameof(SystemConvertToString_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int32>>(nameof(SystemConvertToString_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt32>>(nameof(SystemConvertToString_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int64>>(nameof(SystemConvertToString_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt64>>(nameof(SystemConvertToString_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Single>>(nameof(SystemConvertToString_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Double>>(nameof(SystemConvertToString_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Decimal>>(nameof(SystemConvertToString_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_DateTime>>(nameof(SystemConvertToString_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_String>>(nameof(SystemConvertToString_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Boolean>>(nameof(SystemConvertToString_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Object_IFormatProvider>>(nameof(SystemConvertToString_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Char_IFormatProvider>>(nameof(SystemConvertToString_Char_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_SByte_IFormatProvider>>(nameof(SystemConvertToString_SByte_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Byte_IFormatProvider>>(nameof(SystemConvertToString_Byte_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int16_IFormatProvider>>(nameof(SystemConvertToString_Int16_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt16_IFormatProvider>>(nameof(SystemConvertToString_UInt16_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int32_IFormatProvider>>(nameof(SystemConvertToString_Int32_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt32_IFormatProvider>>(nameof(SystemConvertToString_UInt32_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int64_IFormatProvider>>(nameof(SystemConvertToString_Int64_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_UInt64_IFormatProvider>>(nameof(SystemConvertToString_UInt64_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Single_IFormatProvider>>(nameof(SystemConvertToString_Single_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Double_IFormatProvider>>(nameof(SystemConvertToString_Double_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Decimal_IFormatProvider>>(nameof(SystemConvertToString_Decimal_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_DateTime_IFormatProvider>>(nameof(SystemConvertToString_DateTime_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_String_IFormatProvider>>(nameof(SystemConvertToString_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Byte_Int32>>(nameof(SystemConvertToString_Byte_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int16_Int32>>(nameof(SystemConvertToString_Int16_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int32_Int32>>(nameof(SystemConvertToString_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Int64_Int32>>(nameof(SystemConvertToString_Int64_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToString_Boolean_IFormatProvider>>(nameof(SystemConvertToString_Boolean_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Object>>(nameof(SystemConvertToUInt16_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Boolean>>(nameof(SystemConvertToUInt16_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Char>>(nameof(SystemConvertToUInt16_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_SByte>>(nameof(SystemConvertToUInt16_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Byte>>(nameof(SystemConvertToUInt16_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Int16>>(nameof(SystemConvertToUInt16_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Int32>>(nameof(SystemConvertToUInt16_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_UInt16>>(nameof(SystemConvertToUInt16_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_UInt32>>(nameof(SystemConvertToUInt16_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Int64>>(nameof(SystemConvertToUInt16_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_UInt64>>(nameof(SystemConvertToUInt16_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Single>>(nameof(SystemConvertToUInt16_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Double>>(nameof(SystemConvertToUInt16_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Decimal>>(nameof(SystemConvertToUInt16_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_String>>(nameof(SystemConvertToUInt16_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_DateTime>>(nameof(SystemConvertToUInt16_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_Object_IFormatProvider>>(nameof(SystemConvertToUInt16_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_String_IFormatProvider>>(nameof(SystemConvertToUInt16_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt16_String_Int32>>(nameof(SystemConvertToUInt16_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Object>>(nameof(SystemConvertToUInt32_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Boolean>>(nameof(SystemConvertToUInt32_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Char>>(nameof(SystemConvertToUInt32_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_SByte>>(nameof(SystemConvertToUInt32_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Byte>>(nameof(SystemConvertToUInt32_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Int16>>(nameof(SystemConvertToUInt32_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_UInt16>>(nameof(SystemConvertToUInt32_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Int32>>(nameof(SystemConvertToUInt32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_UInt32>>(nameof(SystemConvertToUInt32_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Int64>>(nameof(SystemConvertToUInt32_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_UInt64>>(nameof(SystemConvertToUInt32_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Single>>(nameof(SystemConvertToUInt32_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Double>>(nameof(SystemConvertToUInt32_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Decimal>>(nameof(SystemConvertToUInt32_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_String>>(nameof(SystemConvertToUInt32_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_DateTime>>(nameof(SystemConvertToUInt32_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_Object_IFormatProvider>>(nameof(SystemConvertToUInt32_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_String_IFormatProvider>>(nameof(SystemConvertToUInt32_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt32_String_Int32>>(nameof(SystemConvertToUInt32_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Object>>(nameof(SystemConvertToUInt64_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Boolean>>(nameof(SystemConvertToUInt64_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Char>>(nameof(SystemConvertToUInt64_Char));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_SByte>>(nameof(SystemConvertToUInt64_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Byte>>(nameof(SystemConvertToUInt64_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Int16>>(nameof(SystemConvertToUInt64_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_UInt16>>(nameof(SystemConvertToUInt64_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Int32>>(nameof(SystemConvertToUInt64_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_UInt32>>(nameof(SystemConvertToUInt64_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Int64>>(nameof(SystemConvertToUInt64_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_UInt64>>(nameof(SystemConvertToUInt64_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Single>>(nameof(SystemConvertToUInt64_Single));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Double>>(nameof(SystemConvertToUInt64_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Decimal>>(nameof(SystemConvertToUInt64_Decimal));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_String>>(nameof(SystemConvertToUInt64_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_DateTime>>(nameof(SystemConvertToUInt64_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_Object_IFormatProvider>>(nameof(SystemConvertToUInt64_Object_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_String_IFormatProvider>>(nameof(SystemConvertToUInt64_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemConvertToUInt64_String_Int32>>(nameof(SystemConvertToUInt64_String_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_String>>(nameof(SystemStringCompare_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_String_Boolean>>(nameof(SystemStringCompare_String_String_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_String_StringComparison>>(nameof(SystemStringCompare_String_String_StringComparison));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_String_CultureInfo_CompareOptions>>(nameof(SystemStringCompare_String_String_CultureInfo_CompareOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_String_Boolean_CultureInfo>>(nameof(SystemStringCompare_String_String_Boolean_CultureInfo));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_Int32_String_Int32_Int32>>(nameof(SystemStringCompare_String_Int32_String_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_Int32_String_Int32_Int32_Boolean>>(nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_Int32_String_Int32_Int32_StringComparison>>(nameof(SystemStringCompare_String_Int32_String_Int32_Int32_StringComparison));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo>>(nameof(SystemStringCompare_String_Int32_String_Int32_Int32_Boolean_CultureInfo));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions>>(nameof(SystemStringCompare_String_Int32_String_Int32_Int32_CultureInfo_CompareOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompareOrdinal_String_String>>(nameof(SystemStringCompareOrdinal_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCompareOrdinal_String_Int32_String_Int32_Int32>>(nameof(SystemStringCompareOrdinal_String_Int32_String_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_Object>>(nameof(SystemStringConcat_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_Object_>>(nameof(SystemStringConcat_Object_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_IEnumerable_1>>(nameof(SystemStringConcat_IEnumerable_1));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_String_>>(nameof(SystemStringConcat_String_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_Object_Object>>(nameof(SystemStringConcat_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_String_String>>(nameof(SystemStringConcat_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_Object_Object_Object>>(nameof(SystemStringConcat_Object_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_String_String_String>>(nameof(SystemStringConcat_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_Object_Object_Object_Object>>(nameof(SystemStringConcat_Object_Object_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringConcat_String_String_String_String>>(nameof(SystemStringConcat_String_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringCopy_String>>(nameof(SystemStringCopy_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringEquals_String_String>>(nameof(SystemStringEquals_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringEquals_String_String_StringComparison>>(nameof(SystemStringEquals_String_String_StringComparison));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_String_Object>>(nameof(SystemStringFormat_String_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_String_Object_>>(nameof(SystemStringFormat_String_Object_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_String_Object_Object>>(nameof(SystemStringFormat_String_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_IFormatProvider_String_Object>>(nameof(SystemStringFormat_IFormatProvider_String_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_IFormatProvider_String_Object_>>(nameof(SystemStringFormat_IFormatProvider_String_Object_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_String_Object_Object_Object>>(nameof(SystemStringFormat_String_Object_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_IFormatProvider_String_Object_Object>>(nameof(SystemStringFormat_IFormatProvider_String_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringFormat_IFormatProvider_String_Object_Object_Object>>(nameof(SystemStringFormat_IFormatProvider_String_Object_Object_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringIntern_String>>(nameof(SystemStringIntern_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringIsInterned_String>>(nameof(SystemStringIsInterned_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringIsNullOrEmpty_String>>(nameof(SystemStringIsNullOrEmpty_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringIsNullOrWhiteSpace_String>>(nameof(SystemStringIsNullOrWhiteSpace_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringJoin_String_String_>>(nameof(SystemStringJoin_String_String_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringJoin_String_Object_>>(nameof(SystemStringJoin_String_Object_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringJoin_String_IEnumerable_1>>(nameof(SystemStringJoin_String_IEnumerable_1));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemStringJoin_String_String__Int32_Int32>>(nameof(SystemStringJoin_String_String__Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeCompare_DateTime_DateTime>>(nameof(SystemDateTimeCompare_DateTime_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeDaysInMonth_Int32_Int32>>(nameof(SystemDateTimeDaysInMonth_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeEquals_DateTime_DateTime>>(nameof(SystemDateTimeEquals_DateTime_DateTime));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeFromBinary_Int64>>(nameof(SystemDateTimeFromBinary_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeFromFileTime_Int64>>(nameof(SystemDateTimeFromFileTime_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeFromFileTimeUtc_Int64>>(nameof(SystemDateTimeFromFileTimeUtc_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeFromOADate_Double>>(nameof(SystemDateTimeFromOADate_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeIsLeapYear_Int32>>(nameof(SystemDateTimeIsLeapYear_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParse_String>>(nameof(SystemDateTimeParse_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParse_String_IFormatProvider>>(nameof(SystemDateTimeParse_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParse_String_IFormatProvider_DateTimeStyles>>(nameof(SystemDateTimeParse_String_IFormatProvider_DateTimeStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParseExact_String_String_IFormatProvider>>(nameof(SystemDateTimeParseExact_String_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParseExact_String_String_IFormatProvider_DateTimeStyles>>(nameof(SystemDateTimeParseExact_String_String_IFormatProvider_DateTimeStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles>>(nameof(SystemDateTimeParseExact_String_String__IFormatProvider_DateTimeStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeSpecifyKind_DateTime_DateTimeKind>>(nameof(SystemDateTimeSpecifyKind_DateTime_DateTimeKind));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeTryParse_String_DateTime_>>(nameof(SystemDateTimeTryParse_String_DateTime_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeTryParse_String_IFormatProvider_DateTimeStyles_DateTime_>>(nameof(SystemDateTimeTryParse_String_IFormatProvider_DateTimeStyles_DateTime_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeTryParseExact_String_String_IFormatProvider_DateTimeStyles_DateTime_>>(nameof(SystemDateTimeTryParseExact_String_String_IFormatProvider_DateTimeStyles_DateTime_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_>>(nameof(SystemDateTimeTryParseExact_String_String__IFormatProvider_DateTimeStyles_DateTime_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeNow>>(nameof(SystemDateTimeNow));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeUtcNow>>(nameof(SystemDateTimeUtcNow));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDateTimeToday>>(nameof(SystemDateTimeToday));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanCompare_TimeSpan_TimeSpan>>(nameof(SystemTimeSpanCompare_TimeSpan_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanEquals_TimeSpan_TimeSpan>>(nameof(SystemTimeSpanEquals_TimeSpan_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromDays_Double>>(nameof(SystemTimeSpanFromDays_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromHours_Double>>(nameof(SystemTimeSpanFromHours_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromMilliseconds_Double>>(nameof(SystemTimeSpanFromMilliseconds_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromMinutes_Double>>(nameof(SystemTimeSpanFromMinutes_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromSeconds_Double>>(nameof(SystemTimeSpanFromSeconds_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanFromTicks_Int64>>(nameof(SystemTimeSpanFromTicks_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParse_String>>(nameof(SystemTimeSpanParse_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParse_String_IFormatProvider>>(nameof(SystemTimeSpanParse_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParseExact_String_String_IFormatProvider>>(nameof(SystemTimeSpanParseExact_String_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParseExact_String_String__IFormatProvider>>(nameof(SystemTimeSpanParseExact_String_String__IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles>>(nameof(SystemTimeSpanParseExact_String_String_IFormatProvider_TimeSpanStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles>>(nameof(SystemTimeSpanParseExact_String_String__IFormatProvider_TimeSpanStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParse_String_TimeSpan_>>(nameof(SystemTimeSpanTryParse_String_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParse_String_IFormatProvider_TimeSpan_>>(nameof(SystemTimeSpanTryParse_String_IFormatProvider_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpan_>>(nameof(SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpan_>>(nameof(SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_>>(nameof(SystemTimeSpanTryParseExact_String_String_IFormatProvider_TimeSpanStyles_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_>>(nameof(SystemTimeSpanTryParseExact_String_String__IFormatProvider_TimeSpanStyles_TimeSpan_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32Parse_String>>(nameof(SystemInt32Parse_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32Parse_String_NumberStyles>>(nameof(SystemInt32Parse_String_NumberStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32Parse_String_IFormatProvider>>(nameof(SystemInt32Parse_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32Parse_String_NumberStyles_IFormatProvider>>(nameof(SystemInt32Parse_String_NumberStyles_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32TryParse_String_Int32_>>(nameof(SystemInt32TryParse_String_Int32_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_>>(nameof(SystemInt32TryParse_String_NumberStyles_IFormatProvider_Int32_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleIsInfinity_Double>>(nameof(SystemDoubleIsInfinity_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleIsNaN_Double>>(nameof(SystemDoubleIsNaN_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleIsNegativeInfinity_Double>>(nameof(SystemDoubleIsNegativeInfinity_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleIsPositiveInfinity_Double>>(nameof(SystemDoubleIsPositiveInfinity_Double));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleParse_String>>(nameof(SystemDoubleParse_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleParse_String_NumberStyles>>(nameof(SystemDoubleParse_String_NumberStyles));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleParse_String_IFormatProvider>>(nameof(SystemDoubleParse_String_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleParse_String_NumberStyles_IFormatProvider>>(nameof(SystemDoubleParse_String_NumberStyles_IFormatProvider));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleTryParse_String_Double_>>(nameof(SystemDoubleTryParse_String_Double_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemDoubleTryParse_String_NumberStyles_IFormatProvider_Double_>>(nameof(SystemDoubleTryParse_String_NumberStyles_IFormatProvider_Double_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemGuidNewGuid>>(nameof(SystemGuidNewGuid));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemGuidParse_String>>(nameof(SystemGuidParse_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemGuidParseExact_String_String>>(nameof(SystemGuidParseExact_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemGuidTryParse_String_Guid_>>(nameof(SystemGuidTryParse_String_Guid_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemGuidTryParseExact_String_String_Guid_>>(nameof(SystemGuidTryParseExact_String_String_Guid_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayBinarySearch_Array_Object>>(nameof(SystemArrayBinarySearch_Array_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayBinarySearch_Array_Object_IComparer>>(nameof(SystemArrayBinarySearch_Array_Object_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayBinarySearch_Array_Int32_Int32_Object>>(nameof(SystemArrayBinarySearch_Array_Int32_Int32_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayBinarySearch_Array_Int32_Int32_Object_IComparer>>(nameof(SystemArrayBinarySearch_Array_Int32_Int32_Object_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayClear_Array_Int32_Int32>>(nameof(SystemArrayClear_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32>>(nameof(SystemArrayConstrainedCopy_Array_Int32_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCopy_Array_Array_Int32>>(nameof(SystemArrayCopy_Array_Array_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCopy_Array_Array_Int64>>(nameof(SystemArrayCopy_Array_Array_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCopy_Array_Int32_Array_Int32_Int32>>(nameof(SystemArrayCopy_Array_Int32_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCopy_Array_Int64_Array_Int64_Int64>>(nameof(SystemArrayCopy_Array_Int64_Array_Int64_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int32>>(nameof(SystemArrayCreateInstance_Type_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int32_>>(nameof(SystemArrayCreateInstance_Type_Int32_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int64_>>(nameof(SystemArrayCreateInstance_Type_Int64_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int32__Int32_>>(nameof(SystemArrayCreateInstance_Type_Int32__Int32_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int32_Int32>>(nameof(SystemArrayCreateInstance_Type_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayCreateInstance_Type_Int32_Int32_Int32>>(nameof(SystemArrayCreateInstance_Type_Int32_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayIndexOf_Array_Object>>(nameof(SystemArrayIndexOf_Array_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayIndexOf_Array_Object_Int32>>(nameof(SystemArrayIndexOf_Array_Object_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayIndexOf_Array_Object_Int32_Int32>>(nameof(SystemArrayIndexOf_Array_Object_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayLastIndexOf_Array_Object>>(nameof(SystemArrayLastIndexOf_Array_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayLastIndexOf_Array_Object_Int32>>(nameof(SystemArrayLastIndexOf_Array_Object_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayLastIndexOf_Array_Object_Int32_Int32>>(nameof(SystemArrayLastIndexOf_Array_Object_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayReverse_Array>>(nameof(SystemArrayReverse_Array));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArrayReverse_Array_Int32_Int32>>(nameof(SystemArrayReverse_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array>>(nameof(SystemArraySort_Array));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Array>>(nameof(SystemArraySort_Array_Array));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_IComparer>>(nameof(SystemArraySort_Array_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Int32_Int32>>(nameof(SystemArraySort_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Array_IComparer>>(nameof(SystemArraySort_Array_Array_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Array_Int32_Int32>>(nameof(SystemArraySort_Array_Array_Int32_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Int32_Int32_IComparer>>(nameof(SystemArraySort_Array_Int32_Int32_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemArraySort_Array_Array_Int32_Int32_IComparer>>(nameof(SystemArraySort_Array_Array_Int32_Int32_IComparer));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumFormat_Type_Object_String>>(nameof(SystemEnumFormat_Type_Object_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumGetName_Type_Object>>(nameof(SystemEnumGetName_Type_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumGetNames_Type>>(nameof(SystemEnumGetNames_Type));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumGetUnderlyingType_Type>>(nameof(SystemEnumGetUnderlyingType_Type));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumGetValues_Type>>(nameof(SystemEnumGetValues_Type));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumIsDefined_Type_Object>>(nameof(SystemEnumIsDefined_Type_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumParse_Type_String>>(nameof(SystemEnumParse_Type_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumParse_Type_String_Boolean>>(nameof(SystemEnumParse_Type_String_Boolean));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_Object>>(nameof(SystemEnumToObject_Type_Object));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_SByte>>(nameof(SystemEnumToObject_Type_SByte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_Int16>>(nameof(SystemEnumToObject_Type_Int16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_Int32>>(nameof(SystemEnumToObject_Type_Int32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_Byte>>(nameof(SystemEnumToObject_Type_Byte));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_UInt16>>(nameof(SystemEnumToObject_Type_UInt16));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_UInt32>>(nameof(SystemEnumToObject_Type_UInt32));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_Int64>>(nameof(SystemEnumToObject_Type_Int64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SystemEnumToObject_Type_UInt64>>(nameof(SystemEnumToObject_Type_UInt64));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName>>(nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_>>(nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder_));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String>>(nameof(System_Text_RegularExpressionsRegexCompileToAssembly_RegexCompilationInfo__AssemblyName_CustomAttributeBuilder__String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexEscape_String>>(nameof(System_Text_RegularExpressionsRegexEscape_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexIsMatch_String_String>>(nameof(System_Text_RegularExpressionsRegexIsMatch_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexIsMatch_String_String_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexIsMatch_String_String_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexIsMatch_String_String_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexIsMatch_String_String_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatch_String_String>>(nameof(System_Text_RegularExpressionsRegexMatch_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatch_String_String_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexMatch_String_String_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatch_String_String_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexMatch_String_String_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatches_String_String>>(nameof(System_Text_RegularExpressionsRegexMatches_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexMatches_String_String_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_String>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_String_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexReplace_String_String_MatchEvaluator_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexSplit_String_String>>(nameof(System_Text_RegularExpressionsRegexSplit_String_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions>>(nameof(System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions_TimeSpan>>(nameof(System_Text_RegularExpressionsRegexSplit_String_String_RegexOptions_TimeSpan));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexUnescape_String>>(nameof(System_Text_RegularExpressionsRegexUnescape_String));
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<System_Text_RegularExpressionsRegexCacheSize>>(nameof(System_Text_RegularExpressionsRegexCacheSize));
        }
        #endregion

        #region Private Methods

        #region [CreateActiveFlowsFrom]
        /// <summary>
        /// Creates <see cref="ActiveFlow.ActiveFlow"/> objects from a list of <see cref="FlowInstance.FlowInstance"/> objects
        /// </summary>
        /// <param name="flowInstances">Objects to create from</param>
        private void CreateActiveFlowsFrom(List<FlowInstance> flowInstances)
        {
            if (flowInstances.Count > 0)
                flowLogService.Info($"> Creating active flows from {string.Join(",", flowInstances.Select(x => $"\"{x.Flow.Name}\""))}");

            foreach (var flowInstance in flowInstances)
            {
                CreateActiveFlow(flowInstance);
            }

            if (ActiveFlows.Count > 0)
                flowLogService.Info($"> {ActiveFlows.Count} active flows were created.");
        }
        #endregion

        #region [CreateActiveFlow]
        /// <summary>
        /// CreateActiveFlow
        /// </summary>
        /// <param name="flowInstance"></param>
        private void CreateActiveFlow(FlowInstance flowInstance)
        {
            if (flowInstance.Flow == null)
                flowInstance.Flow = Flows.FirstOrDefault(x => x.Id == flowInstance.FlowId);

            if (flowInstance.IsAlive)
            {
                var activeFlow = new ActiveFlow.ActiveFlow
                {
                    FlowInstanceId = flowInstance.Id,
                    CurrentEventNodes = flowInstance.CurrentNodes,
                    FlowId = flowInstance?.Flow?.Id ?? flowInstance.FlowId
                };

                ActiveFlows.Add(activeFlow);
            }
        }
        #endregion

        #region [CreateFlowsFromConfiguration]
        /// <summary>
        /// Copy a flow instance before using it.
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        private Flow CopyFlow(Flow flow)
        {
            // TODO: Make this optional, so that the user can decide whether to use a copy (slower but better memory management).
            return flow;
        }

        /// <summary>
        /// Creates a list <see cref="Flow"/> objects from <see cref="FlowConfiguration"/> objects
        /// </summary>
        /// <returns>A list <see cref="Flow"/> objects</returns>
        private IList<Flow> CreateFlowsFromConfiguration()
        {
            flowLogService.Info($"> Creating flows from configurations...");

            var list = new List<Flow>();
            foreach (var flowConfiguration in flowConfigurations)
            {
                var nodes = new List<BaseNode>();
                foreach (var node in flowConfiguration.Nodes)
                {
                    var resolver = unityContainer.Resolve<INodeResolver>($"{node.ClassName}");
                    var newNode = resolver.Create(node.Id, node.IsStartEvent);

                    // Create data pin instances inside node
                    newNode.CreateDataPins();

                    nodes.Add(newNode);
                    // Set default values
                    if (node?.Pins != null)
                    {
                        foreach (var pin in node.Pins)
                        {
                            var pinProperty = newNode.GetType().GetProperty(pin.Name,
                                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                            var defaultValueProperty = typeof(DataPin).GetProperty("DefaultValue",
                                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                            if (pin.DefaultValue != null)
                            {
                                var pinInstance = pinProperty.GetValue(newNode);

                                defaultValueProperty.SetValue(pinInstance, pin.DefaultValue);
                            }
                        }
                    }
                }

                foreach (var pin in flowConfiguration.Pins)
                {
                    // Find from to pin
                    var fromNode = nodes.FirstOrDefault(x => x.Id == pin.From.NodeId);
                    var toNode = nodes.FirstOrDefault(x => x.Id == pin.To.NodeId);

                    // Find from member
                    var fromProperty = fromNode.GetType().GetProperty(pin.From.PinName,
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    var toProperty = toNode.GetType().GetProperty(pin.To.PinName,
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    // Set properpty
                    toProperty.SetValue(toNode, fromProperty.GetValue(fromNode));
                }

                foreach (var link in flowConfiguration.Links)
                {
                    // Find from to pin
                    var fromNode = nodes.FirstOrDefault(x => x.Id == link.From.NodeId);
                    var toNode = nodes.FirstOrDefault(x => x.Id == link.To.NodeId);

                    // Find from property
                    var property = fromNode.GetType().GetProperty(link.From.PinName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    /* this one is tricky.
                     * here we check if the property is a list.
                     * if so, we get the value of the property (which is type of IList)
                     * and find the Add method and invoke it.
                     */
                    if (property.PropertyType.Name == "IList`1")
                    {
                        // retrieves current List value to call Add method
                        var customList = property.GetValue(fromNode);

                        // gets metadata of the List.Add method
                        var addMethod = customList.GetType().GetMethod("Add");
                        addMethod.Invoke(customList, new object[] { toNode });
                    }
                    else
                    {
                        property.SetValue(fromNode, toNode);
                    }
                }

                list.Add(new Flow
                {
                    Id = flowConfiguration.Id,
                    Name = flowConfiguration.Name,
                    Nodes = nodes
                });
            }

            flowLogService.Info($"> {flowConfigurations.Count} flows created.");

            return list;
        }
        #endregion

        #region [LoadEventQueue]
        /// <summary>
        /// Loads the event queue from the repository
        /// </summary>
        private void LoadEventQueue()
        {
            var unhandledEvents = flowEventQueueService.GetAllUnhandled(machineName, serviceName);

            if (unhandledEvents.Count() > 0)
                flowLogService.Info($"> {unhandledEvents.Count()} unhandled events were found.");

            foreach (var flowEventQueue in unhandledEvents)
            {
                var eventArgs = JsonConvert.DeserializeObject<FlowEventArgs>(
                    Encoding.UTF8.GetString(flowEventQueue.Args),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                // if there are no event args create an empty object
                if (eventArgs == null)
                    eventArgs = new FlowEventArgs();


                eventArgs.EventName = flowEventQueue.EventName;
                eventArgs.Id = flowEventQueue.Id;
                eventArgs.UserId = flowEventQueue.CreateUserId;

                EnqueueEvent(eventArgs);
            }

            if (eventQueue.Count() > 0)
                flowLogService.Info($"> {eventQueue.Count()} events were created.");
        }
        #endregion

        #region [SetEventQueueHandled/SetEventQueueFailed]
        /// <summary>
        /// Sets an event queue to handled
        /// </summary>
        /// <param name="eventQueueId">Event queue to update</param>
        private bool SetEventQueueHandled(string eventQueueId)
        {
            flowLogService.Info($"Running SetEventQueueHandled with {eventQueueId}");
            var result = flowEventQueueService.SetHandled(eventQueueId, true);
            flowEventQueueService.Remove(eventQueueId);

            return result;
        }

        /// <summary>
        /// Sets an event queue to failed
        /// </summary>
        /// <param name="eventQueueId">Event queue to update</param>
        private void SetEventQueueFailed(string eventQueueId)
        {
            flowLogService.Info($"Running SetEventQueueFAILED with {eventQueueId}");
            flowEventQueueService.SetFailed(eventQueueId);
        }
        #endregion

        #region [ProcessEvent]
        /// <summary>
        /// Processing thread worker for a particular flow instance
        /// </summary>
        /// <param name="param"><see cref="ThreadStateInfo"/> Object to process</param>
        private void ProcessEvent(object param)
        {
            var threadInfo = param as ThreadStateInfo;

            // Create runtime
            var runtime = new FlowRuntimeService(flowLogService, threadInfo.EventCall.Args);

            try
            {
                if (threadInfo.IsStartEvent)
                {
                    // Run flow service
                    runtime.Run(threadInfo.FlowInstance, threadInfo.EventCall);

                    // Save or remove active flow
                    SaveOrDeleteFlowInstance(threadInfo.FlowInstance);
                }
                else
                {
                    runtime.Run(threadInfo.FlowInstance, threadInfo.EventCall);
                    CreateActiveFlow(threadInfo.FlowInstance);

                    // Save or remove active flow
                    SaveOrDeleteFlowInstance(threadInfo.FlowInstance);
                }
            }
            catch (Exception ex)
            {
                // TODO: When implementing a profiler, report the failed nodes
                var lastActiveFlowNodes = threadInfo.FlowInstance.CurrentNodes.Select(x => x.Node?.Name ?? "").ToList();
                flowLogService.Error($"Error during executing flow node. Current nodes: {string.Join(", ", lastActiveFlowNodes)}", ex);

                threadInfo.FlowInstance.IsFailed = true;

                // We don't have any current nodes when the flow failes
                threadInfo.FlowInstance.CurrentNodes.Clear();

                // Save or remove active flow
                SaveOrDeleteFlowInstance(threadInfo.FlowInstance);
            }

            // Remove from active flows
            if (!threadInfo.FlowInstance.IsAlive || threadInfo.FlowInstance.IsFailed)
                ActiveFlows.Remove(threadInfo.ActiveFlow);
        }

        /// <summary>
        /// Save or delete flow instance
        /// </summary>
        /// <param name="flowInstance">Flow instance</param>
        private void SaveOrDeleteFlowInstance(FlowInstance flowInstance)
        {
            if (flowInstance.IsAlive)
                flowInstanceService.Save(flowInstance);
            else
            {
                flowInstanceService.Delete(flowInstance);
            }
        }
        #endregion

        #endregion

        #region Public Methods
        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="machineName">Machine name</param>
        /// <param name="serviceName">Service name</param>
        public void Initialize(string machineName, string serviceName)
        {
            this.machineName = machineName;
            this.serviceName = serviceName;

            // Load thread count
            var plugIn = "Flow";
            var configurationName = $"ThreadCount_{machineName}_{serviceName}";

            if (!configurationService.Exists(configurationName, plugIn))
            {
                threadCount = defaultThreadCount;

                configurationService.Create<int>(configurationName, plugIn, 1, true, defaultThreadCount);
                Console.WriteLine($"Create new flow-thread-count setting {plugIn}/{configurationName}");
            }
            else
                threadCount = configurationService.GetValue<int>(configurationName, plugIn, ""); ;

            Console.WriteLine($"Flow thread count: {threadCount}");

            // Load active flow configurations
            flowConfigurations = flowConfigurationService.GetAll()
                .Where(x => x.IsActive
                       && (string.IsNullOrWhiteSpace(x.MachineName) || x.MachineName?.ToLower() == machineName?.ToLower())
                       && (string.IsNullOrWhiteSpace(x.ServiceName) || x.ServiceName?.ToLower() == serviceName?.ToLower()))
                .ToList();

            if (flowConfigurations.Count > 0)
                flowLogService.Info($"# {flowConfigurations.Count} Active flow configurations were found: {string.Join(", ", flowConfigurations.Select(x => $"\"{x.Name}\""))}");
            else
            {
                flowLogService.Info("No active flow configurations were found!");
                return;
            }

            Flows = CreateFlowsFromConfiguration();
            CreateActiveFlowsFrom(flowInstanceService.GetAllAlive().ToList());

            RefreshEventDelegates();
        }

        #region [Run]
        /// <summary>
        /// Run a single cycle
        /// </summary>
        public void Run()
        {
            // Raise event before the process has begun.
            OnStarted?.Invoke(this, EventArgs.Empty);
            int maxParallelTasks = threadCount;

            //flowLogService.Info($"> Running at {DateTime.Now}");
            try
            {
                if (executions.Count == 0)
                {
                    // load event queue from db            
                    LoadEventQueue();

                    if (eventQueue.Count() == 0)
                    {
                        //flowLogService.Info($"- Event queue is empty. Nothing to do.");
                        return;
                    }

                    while (eventQueue.Count > 0)
                    {
                        // pop event entries from queue first
                        var queueEntry = eventQueue.PopFirst();

                        //flowLogService.Info($"> Processing {queueEntry.Args.EventName}...");

                        if (queueEntry.Delegate.IsStartEvent)
                        {
                            //flowLogService.Info($"- Create new flow instance {queueEntry.Args.EventName} : {queueEntry.Delegate.FlowId}");

                            var newFlowInstance = new FlowInstance
                            {
                                Flow = CopyFlow(Flows.FirstOrDefault(x => x.Id == queueEntry.Delegate.FlowId)),
                                Id = Guid.NewGuid()
                            };

                            executions.Add(new ThreadStateInfo
                            {
                                FlowInstance = newFlowInstance,
                                EventCall = queueEntry
                            });
                        }
                        else
                        {
                            // Notify ALL instances, which MIGHT BE continued
                            foreach (var activeFlow in ActiveFlows.Where(x => x.FlowId == queueEntry.Delegate.FlowId))
                            {
                                //flowLogService.Info($"Continuing flow instance: {activeFlow.FlowInstanceId}");

                                // Get from database
                                var flowInstance = flowInstanceService.GetById(activeFlow.FlowInstanceId);
                                flowInstance.Flow = CopyFlow(Flows.FirstOrDefault(x => x.Id == flowInstance.FlowId));

                                executions.Add(new ThreadStateInfo
                                {
                                    IsStartEvent = false,
                                    FlowInstance = flowInstance,
                                    EventCall = queueEntry,
                                    ActiveFlow = activeFlow
                                });
                            }
                        }
                    }
                }

                var failed = new ConcurrentList<string>();
                var success = new ConcurrentList<string>();

                int runningTasks = 0;
                var executedThreads = new List<Thread>();
                var lastDebugText = "";

                // Group by event delegate and take a given amount....
                var tempJobs = new List<ThreadStateInfo>();
                foreach (var eventGroup in executions.GroupBy(x => x.EventCall.Id))
                {
                    tempJobs.AddRange(eventGroup);

                    // Limit the amount of parallel tasks
                    if (tempJobs.Count > maxParallelTasks * 3)
                        break;
                }

                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Flows in execution queue: {executions.Count}. Executed in the next loop: {tempJobs.Count}");
                Console.ForegroundColor = color;

                while (tempJobs.Any() && executions.Any())
                {
                    var currentlyExecutedJobs = tempJobs.Take(maxParallelTasks - runningTasks).ToList();

                    var nextDebugText = $"Flows to execute/total {tempJobs.Count}/{executions.Count}. Max parallel flows {maxParallelTasks}. Currently running flows: {runningTasks}";

                    if (lastDebugText != nextDebugText)
                    {
                        Console.WriteLine(nextDebugText);
                        lastDebugText = nextDebugText;
                    }

                    if (!currentlyExecutedJobs.Any())
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    var tempThreads = new List<Thread>();

                    foreach (var job in currentlyExecutedJobs)
                    {
                        // Make a task reservation
                        runningTasks++;

                        Console.WriteLine($"   [{DateTime.Now.ToLongTimeString()}] Create threads: {job.EventCall?.Delegate.EventName}");
                        var thread = new Thread(() =>
                        {
                            executions.Remove(job);
                            tempJobs.Remove(job);

                            try
                            {
                                ProcessEvent(job);
                                success.Add(job.EventCall.Id);
                            }
                            catch
                            {
                                failed.Add(job.EventCall.Id);
                            }

                            runningTasks--;
                        });

                        tempThreads.Add(thread);
                    }

                    // Execute
                    foreach (var thread in tempThreads)
                    {
                        Console.WriteLine($" > [{DateTime.Now.ToLongTimeString()}] Start thread {thread.ManagedThreadId}");
                        thread.Start();
                        executedThreads.Add(thread);
                    }

                    Thread.Sleep(30);
                }

                foreach (var thread in executedThreads)
                {
                    var id = thread.ManagedThreadId;
                    thread.Join();

                    Console.WriteLine($" < [{DateTime.Now.ToLongTimeString()}] Thread joined {id}");
                }

                // Remove failed from success
                foreach (var failedJob in failed.Distinct())
                {
                    SetEventQueueFailed(failedJob);
                    success.Remove(failedJob);
                }

                // Set handled jobs and remove them
                foreach (var successJob in success.Distinct())
                    SetEventQueueHandled(successJob);

                color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Flow execution complete. Flows: {executedThreads.Count}");
                Console.ForegroundColor = color;

                executedThreads.Clear();
            }
            catch (Exception ex)
            {
                flowLogService.Error($"- FlowService.Run could not be run", ex);

                // Raise event after the process has begun.
                OnCompleted?.Invoke(this, EventArgs.Empty);
                throw;
            }

            // Raise event after the process has begun.
            OnCompleted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region [EnqueueEvent]
        /// <summary>
        /// Enqueue event
        /// </summary>
        /// <param name="args"></param>
        public void EnqueueEvent(FlowEventArgs args)
        {
            flowLogService.Info($"- Enqueue event: {args.EventName} / object id {args.ObjectId ?? "<unset>"}");

            bool isEnqueued = false;

            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                foreach (var del in delegates)
                {
                    flowLogService.Info($"- Create event call: {args.EventName} / Flow: {del.FlowId} / Is start event {del.IsStartEvent}");

                    eventQueue.PushBack(new EventCall { Id = args.Id, Args = args, Delegate = del });
                    isEnqueued = true;
                }
            }

            if (!isEnqueued)
            {
                flowLogService.Info($"- No delegate was found for {args.EventName}, changing status to handled.");

                // Remove events that can't be handled
                flowEventQueueService.Remove(args.Id);
            }
        }
        #endregion

        #region [RefreshEventDelegates]
        /// <summary>
        /// Cache event delegates
        /// </summary>
        public void RefreshEventDelegates()
        {
            flowLogService.Info("> Creating event delegates...");

            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
            foreach (var flow in Flows)
            {
                foreach (var eventNode in flow.Nodes.OfType<EventNode>())
                {
                    flowLogService.Info($"> Add {flow.Name}/{eventNode.Name}");

                    IList<EventDelegate> eventDelegateList = null;

                    if (!eventDelegates.ContainsKey(eventNode.EventName))
                    {
                        eventDelegateList = new List<EventDelegate>();
                        eventDelegates[eventNode.EventName] = eventDelegateList;
                    }
                    else
                        eventDelegateList = eventDelegates[eventNode.EventName];

                    eventDelegateList.Add(new EventDelegate
                    {
                        FlowId = flow.Id,
                        EventName = eventNode.EventName,
                        EventNodeId = eventNode.Id,
                        IsStartEvent = eventNode.IsStartEvent
                    });
                }
            }

            flowLogService.Info($"> {eventDelegates.Count} event delegates were created. \"{string.Join(", ", eventDelegates.Select(x => x.Key))}\"");
        }
        #endregion 

        #endregion

        #region Public Properties

        public IList<ActiveFlow.ActiveFlow> ActiveFlows { get; set; }

        public IList<Flow> Flows { get; set; }

        #endregion
    }
}