using System;
using System.IO;
using System.Management;
using System.Text;

namespace wsy
{
    public partial class SystemInfo

    {
        #region SystemInformation

        #region SystemInformation

        public static string SystemInformation()
        {
            var StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                StringBuilder1.AppendFormat("Operation System:  {0}\r\n", Environment.OSVersion);
                if (Environment.Is64BitOperatingSystem)
                    StringBuilder1.AppendFormat("\t\t  64 Bit Operating System\r\n");
                else
                    StringBuilder1.AppendFormat("\t\t  32 Bit Operating System\r\n");
                StringBuilder1.AppendFormat("SystemDirectory:  {0}\r\n", Environment.SystemDirectory);
                StringBuilder1.AppendFormat("ProcessorCount:  {0}\r\n", Environment.ProcessorCount);
                StringBuilder1.AppendFormat("UserDomainName:  {0}\r\n", Environment.UserDomainName);
                StringBuilder1.AppendFormat("UserName: {0}\r\n", Environment.UserName);
                //Drives  
                StringBuilder1.AppendFormat("LogicalDrives:\r\n");
                foreach (var DriveInfo1 in DriveInfo.GetDrives())
                {
                    try
                    {
                        StringBuilder1.AppendFormat("\t Drive: {0}\r\n\t\t VolumeLabel: " +
                                                    "{1}\r\n\t\t DriveType: {2}\r\n\t\t DriveFormat: {3}\r\n\t\t " +
                                                    "TotalSize: {4}\r\n\t\t AvailableFreeSpace: {5}\r\n",
                            DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                            DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                    }
                    catch
                    {
                    }
                }
                StringBuilder1.AppendFormat("SystemPageSize:  {0}\r\n", Environment.SystemPageSize);
                StringBuilder1.AppendFormat("Version:  {0}", Environment.Version);
            }
            catch
            {
            }
            return StringBuilder1.ToString();
        }

        public static string DeviceInformation(Win32Provider provider)
        {
            return DeviceInformation(provider.ToString());
        }

        public static string DeviceInformation(string provider)
        {
            var StringBuilder1 = new StringBuilder(string.Empty);
            var ManagementClass1 = new ManagementClass(provider);
            //Create a ManagementObjectCollection to loop through
            ManagementObjectCollection ManagemenobjCol;
            try
            {
                ManagemenobjCol = ManagementClass1.GetInstances();
            }
            catch (Exception)
            {
                return "error";
            }

            //Get the properties in the class
            var properties = ManagementClass1.Properties;
            foreach (ManagementObject obj in ManagemenobjCol)
            {
                foreach (var property in properties)
                {
                    try
                    {
                        StringBuilder1.AppendLine(property.Name + ":  " +
                                                  obj.Properties[property.Name].Value);
                    }
                    catch
                    {
                        //Add codes to manage more informations
                    }
                }
                StringBuilder1.AppendLine();
            }
            if (StringBuilder1.Length == 0) return "error";
            return StringBuilder1.ToString();
        }

        #endregion

        #region Win32Provider

        public enum Win32Provider
        {
            Win32_WindowsProductActivation,
            Win32_VolumeUserQuota,
            Win32_VolumeQuotaSetting,
            Win32_VolumeQuota,
            Win32_VolumeChangeEvent,
            Win32_Volume,
            Win32_UTCTime,
            Win32_UserInDomain,
            Win32_UserDesktop,
            Win32_UserAccount,
            Win32_Trustee,
            Win32_TokenPrivileges,
            Win32_TokenGroups,
            Win32_TimeZone,
            Win32_ThreadTrace,
            Win32_ThreadStopTrace,
            Win32_ThreadStartTrace,
            Win32_Thread,
            Win32_SystemUsers,
            Win32_SystemTrace,
            Win32_SystemTimeZone,
            Win32_SystemSystemDriver,
            Win32_SystemSetting,
            Win32_SystemServices,
            Win32_SystemResources,
            Win32_SystemProgramGroups,
            Win32_SystemProcesses,
            Win32_SystemPartitions,
            Win32_SystemOperatingSystem,
            Win32_SystemNetworkConnections,
            Win32_SystemLogicalMemoryConfiguration,
            Win32_SystemLoadOrderGroups,
            Win32_SystemDriver,
            Win32_SystemDevices,
            Win32_SystemDesktop,
            Win32_SystemConfigurationChangeEvent,
            Win32_SystemBootConfiguration,
            Win32_SystemAccount,
            Win32_SubDirectory,
            Win32_StartupCommand,
            Win32_SIDandAttributes,
            Win32_SID,
            Win32_ShortcutFile,
            Win32_ShareToDirectory,
            Win32_Share,
            Win32_ShadowVolumeSupport,
            Win32_ShadowStorage,
            Win32_ShadowProvider,
            Win32_ShadowOn,
            Win32_ShadowFor,
            Win32_ShadowDiffVolumeSupport,
            Win32_ShadowCopy,
            Win32_ShadowContext,
            Win32_ShadowBy,
            Win32_SessionProcess,
            Win32_SessionConnection,
            Win32_Service,
            Win32_ServerSession,
            Win32_ServerConnection,
            Win32_SecuritySettingOwner,
            Win32_SecuritySettingOfObject,
            Win32_SecuritySettingOfLogicalShare,
            Win32_SecuritySettingOfLogicalFile,
            Win32_SecuritySettingGroup,
            Win32_SecuritySettingAuditing,
            Win32_SecuritySettingAccess,
            Win32_SecuritySetting,
            Win32_SecurityDescriptor,
            Win32_ScheduledJob,
            Win32_Registry,
            Win32_QuotaSetting,
            Win32_QuickFixEngineering,
            Win32_Proxy,
            Win32_ProtocolBinding,
            Win32_ProgramGroupOrItem,
            Win32_ProgramGroupContents,
            Win32_ProgramGroup,
            Win32_ProcessTrace,
            Win32_ProcessStopTrace,
            Win32_ProcessStartup,
            Win32_ProcessStartTrace,
            Win32_Process,
            Win32_PrivilegesStatus,
            Win32_PrinterShare,
            Win32_PingStatus,
            Win32_PageFileUsage,
            Win32_PageFileSetting,
            Win32_PageFileElementSetting,
            Win32_PageFile,
            Win32_OSRecoveryConfiguration,
            Win32_OperatingSystemQFE,
            Win32_OperatingSystemAutochkSetting,
            Win32_OperatingSystem,
            Win32_NTLogEventUser,
            Win32_NTLogEventLog,
            Win32_NTLogEventComputer,
            Win32_NTLogEvent,
            Win32_NTEventlogFile,
            Win32_NTDomain,
            Win32_NetworkProtocol,
            Win32_NetworkLoginProfile,
            Win32_NetworkConnection,
            Win32_NetworkClient,
            Win32_NamedJobObjectStatistics,
            Win32_NamedJobObjectSecLimitSetting,
            Win32_NamedJobObjectSecLimit,
            Win32_NamedJobObjectProcess,
            Win32_NamedJobObjectLimitSetting,
            Win32_NamedJobObjectLimit,
            Win32_NamedJobObjectActgInfo,
            Win32_NamedJobObject,
            Win32_ModuleTrace,
            Win32_ModuleLoadTrace,
            Win32_MappedLogicalDisk,
            Win32_LUIDandAttributes,
            Win32_LUID,
            Win32_LogonSessionMappedDisk,
            Win32_LogonSession,
            Win32_LogicalShareSecuritySetting,
            Win32_LogicalShareAuditing,
            Win32_LogicalShareAccess,
            Win32_LogicalProgramGroupItemDataFile,
            Win32_LogicalProgramGroupItem,
            Win32_LogicalProgramGroupDirectory,
            Win32_LogicalProgramGroup,
            Win32_LogicalMemoryConfiguration,
            Win32_LogicalFileSecuritySetting,
            Win32_LogicalFileOwner,
            Win32_LogicalFileGroup,
            Win32_LogicalFileAuditing,
            Win32_LogicalFileAccess,
            Win32_LogicalDiskToPartition,
            Win32_LogicalDiskRootDirectory,
            Win32_LogicalDisk,
            Win32_LocalTime,
            Win32_LoadOrderGroupServiceMembers,
            Win32_LoadOrderGroupServiceDependencies,
            Win32_LoadOrderGroup,
            Win32_IP4RouteTableEvent,
            Win32_IP4RouteTable,
            Win32_IP4PersistedRouteTable,
            Win32_ImplementedCategory,
            Win32_GroupUser,
            Win32_GroupInDomain,
            Win32_Group,
            Win32_Environment,
            Win32_DriverVXD,
            Win32_DiskQuota,
            Win32_DiskPartition,
            Win32_DiskDriveToDiskPartition,
            Win32_DirectorySpecification,
            Win32_Directory,
            Win32_DFSTarget,
            Win32_DFSNodeTarget,
            Win32_DFSNode,
            Win32_DeviceChangeEvent,
            Win32_Desktop,
            Win32_DependentService,
            Win32_DCOMApplicationSetting,
            Win32_DCOMApplicationLaunchAllowedSetting,
            Win32_DCOMApplicationAccessAllowedSetting,
            Win32_DCOMApplication,
            Win32_CurrentTime,
            Win32_ConnectionShare,
            Win32_COMSetting,
            Win32_ComputerSystemWindowsProductActivationSetting,
            Win32_ComputerSystemProduct,
            Win32_ComputerSystemProcessor,
            Win32_ComputerSystemEvent,
            Win32_ComputerSystem,
            Win32_ComputerShutdownEvent,
            Win32_ComponentCategory,
            Win32_ComClassEmulator,
            Win32_ComClassAutoEmulator,
            Win32_COMClass,
            Win32_COMApplicationSettings,
            Win32_COMApplicationClasses,
            Win32_COMApplication,
            Win32_CollectionStatistics,
            Win32_CodecFile,
            Win32_ClientApplicationSetting,
            Win32_ClassicCOMClassSettings,
            Win32_ClassicCOMClass,
            Win32_ClassicCOMApplicationClasses,
            Win32_CIMLogicalDeviceCIMDataFile,
            Win32_BootConfiguration,
            Win32_BaseService,
            Win32_ActiveRoute,
            Win32_ACE,
            Win32_AccountSID,
            Win32_Account,
            Win32_Fan,
            Win32_HeatPipe,
            Win32_Refrigeration,
            Win32_TemperatureProbe,
            Win32_Keyboard,
            Win32_PointingDevice,
            Win32_AutochkSetting,
            Win32_CDROMDrive,
            Win32_DiskDrive,
            Win32_FloppyDrive,
            Win32_PhysicalMedia,
            Win32_TapeDrive,
            Win32_1394Controller,
            Win32_1394ControllerDevice,
            Win32_AllocatedResource,
            Win32_AssociatedProcessorMemory,
            Win32_BaseBoard,
            Win32_BIOS,
            Win32_Bus,
            Win32_CacheMemory,
            Win32_ControllerHasHub,
            Win32_DeviceBus,
            Win32_DeviceMemoryAddress,
            Win32_DeviceSettings,
            Win32_DMAChannel,
            Win32_FloppyController,
            Win32_IDEController,
            Win32_IDEControllerDevice,
            Win32_InfraredDevice,
            Win32_IRQResource,
            Win32_MemoryArray,
            Win32_MemoryArrayLocation,
            Win32_MemoryDevice,
            Win32_MemoryDeviceArray,
            Win32_MemoryDeviceLocation,
            Win32_MotherboardDevice,
            Win32_OnBoardDevice,
            Win32_ParallelPort,
            Win32_PCMCIAController,
            Win32_PhysicalMemory,
            Win32_PhysicalMemoryArray,
            Win32_PhysicalMemoryLocation,
            Win32_PNPAllocatedResource,
            Win32_PNPDevice,
            Win32_PNPEntity,
            Win32_PortConnector,
            Win32_PortResource,
            Win32_Processor,
            Win32_SCSIController,
            Win32_SCSIControllerDevice,
            Win32_SerialPort,
            Win32_SerialPortConfiguration,
            Win32_SerialPortSetting,
            Win32_SMBIOSMemory,
            Win32_SoundDevice,
            Win32_SystemBIOS,
            Win32_SystemDriverPNPEntity,
            Win32_SystemEnclosure,
            Win32_SystemMemoryResource,
            Win32_SystemSlot,
            Win32_USBController,
            Win32_USBControllerDevice,
            Win32_USBHub,
            Win32_NetworkAdapter,
            Win32_NetworkAdapterConfiguration,
            Win32_NetworkAdapterSetting,
            Win32_AssociatedBattery,
            Win32_Battery,
            Win32_CurrentProbe,
            Win32_PortableBattery,
            Win32_PowerManagementEvent,
            Win32_UninterruptiblePowerSupply,
            Win32_VoltageProbe,
            Win32_DriverForDevice,
            Win32_Printer,
            Win32_PrinterConfiguration,
            Win32_PrinterController,
            Win32_PrinterDriver,
            Win32_PrinterDriverDll,
            Win32_PrinterSetting,
            Win32_PrintJob,
            Win32_TCPIPPrinterPort,
            Win32_POTSModem,
            Win32_POTSModemToSerialPort,
            Win32_DesktopMonitor,
            Win32_DisplayConfiguration,
            Win32_DisplayControllerConfiguration,
            Win32_VideoController,
            Win32_VideoSettings,
            Win32_PerfFormattedData,
            Win32_PerfFormattedData_ASP_ActiveServerPages,
            Win32_PerfFormattedData_ContentFilter_IndexingServiceFilter,
            Win32_PerfFormattedData_ContentIndex_IndexingService,
            Win32_PerfFormattedData_InetInfo_InternetInformationServicesGlobal,
            Win32_PerfFormattedData_ISAPISearch_HttpIndexingService,
            Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator,
            Win32_PerfFormattedData_NTFSDRV_SMTPNTFSStoreDriver,
            Win32_PerfFormattedData_PerfDisk_LogicalDisk,
            Win32_PerfFormattedData_PerfDisk_PhysicalDisk,
            Win32_PerfFormattedData_PerfNet_Browser,
            Win32_PerfFormattedData_PerfNet_Redirector,
            Win32_PerfFormattedData_PerfNet_Server,
            Win32_PerfFormattedData_PerfNet_ServerWorkQueues,
            Win32_PerfFormattedData_PerfOS_Cache,
            Win32_PerfFormattedData_PerfOS_Memory,
            Win32_PerfFormattedData_PerfOS_Objects,
            Win32_PerfFormattedData_PerfOS_PagingFile,
            Win32_PerfFormattedData_PerfOS_Processor,
            Win32_PerfFormattedData_PerfOS_System,
            Win32_PerfFormattedData_PerfProc_FullImage_Costly,
            Win32_PerfFormattedData_PerfProc_Image_Costly,
            Win32_PerfFormattedData_PerfProc_JobObject,
            Win32_PerfFormattedData_PerfProc_JobObjectDetails,
            Win32_PerfFormattedData_PerfProc_Process,
            Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly,
            Win32_PerfFormattedData_PerfProc_Thread,
            Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly,
            Win32_PerfFormattedData_PSched_PSchedFlow,
            Win32_PerfFormattedData_PSched_PSchedPipe,
            Win32_PerfFormattedData_RemoteAccess_RASPort,
            Win32_PerfFormattedData_RemoteAccess_RASTotal,
            Win32_PerfFormattedData_RSVP_ACSRSVPInterfaces,
            Win32_PerfFormattedData_RSVP_ACSRSVPService,
            Win32_PerfFormattedData_SMTPSVC_SMTPServer,
            Win32_PerfFormattedData_Spooler_PrintQueue,
            Win32_PerfFormattedData_TapiSrv_Telephony,
            Win32_PerfFormattedData_Tcpip_ICMP,
            Win32_PerfFormattedData_Tcpip_IP,
            Win32_PerfFormattedData_Tcpip_NBTConnection,
            Win32_PerfFormattedData_Tcpip_NetworkInterface,
            Win32_PerfFormattedData_Tcpip_TCP,
            Win32_PerfFormattedData_Tcpip_UDP,
            Win32_PerfFormattedData_TermService_TerminalServices,
            Win32_PerfFormattedData_TermService_TerminalServicesSession,
            Win32_PerfFormattedData_W3SVC_WebService,
            Win32_PerfRawData,
            Win32_PerfRawData_ASP_ActiveServerPages,
            Win32_PerfRawData_ContentFilter_IndexingServiceFilter,
            Win32_PerfRawData_ContentIndex_IndexingService,
            Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal,
            Win32_PerfRawData_ISAPISearch_HttpIndexingService,
            Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator,
            Win32_PerfRawData_NTFSDRV_SMTPNTFSStoreDriver,
            Win32_PerfRawData_PerfDisk_LogicalDisk,
            Win32_PerfRawData_PerfDisk_PhysicalDisk,
            Win32_PerfRawData_PerfNet_Browser,
            Win32_PerfRawData_PerfNet_Redirector,
            Win32_PerfRawData_PerfNet_Server,
            Win32_PerfRawData_PerfNet_ServerWorkQueues,
            Win32_PerfRawData_PerfOS_Cache,
            Win32_PerfRawData_PerfOS_Memory,
            Win32_PerfRawData_PerfOS_Objects,
            Win32_PerfRawData_PerfOS_PagingFile,
            Win32_PerfRawData_PerfOS_Processor,
            Win32_PerfRawData_PerfOS_System,
            Win32_PerfRawData_PerfProc_FullImage_Costly,
            Win32_PerfRawData_PerfProc_Image_Costly,
            Win32_PerfRawData_PerfProc_JobObject,
            Win32_PerfRawData_PerfProc_JobObjectDetails,
            Win32_PerfRawData_PerfProc_Process,
            Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly,
            Win32_PerfRawData_PerfProc_Thread,
            Win32_PerfRawData_PerfProc_ThreadDetails_Costly,
            Win32_PerfRawData_PSched_PSchedFlow,
            Win32_PerfRawData_PSched_PSchedPipe,
            Win32_PerfRawData_RemoteAccess_RASPort,
            Win32_PerfRawData_RemoteAccess_RASTotal,
            Win32_PerfRawData_RSVP_ACSRSVPInterfaces,
            Win32_PerfRawData_RSVP_ACSRSVPService,
            Win32_PerfRawData_SMTPSVC_SMTPServer,
            Win32_PerfRawData_Spooler_PrintQueue,
            Win32_PerfRawData_TapiSrv_Telephony,
            Win32_PerfRawData_Tcpip_ICMP,
            Win32_PerfRawData_Tcpip_IP,
            Win32_PerfRawData_Tcpip_NBTConnection,
            Win32_PerfRawData_Tcpip_NetworkInterface,
            Win32_PerfRawData_Tcpip_TCP,
            Win32_PerfRawData_Tcpip_UDP,
            Win32_PerfRawData_TermService_TerminalServices,
            Win32_PerfRawData_TermService_TerminalServicesSession,
            Win32_PerfRawData_W3SVC_WebService,
            Win32_MethodParameterClass,
            Win32_WMISetting,
            Win32_WMIElementSetting
        }

        #endregion

        #endregion
    }

}
