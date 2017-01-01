using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using CTRE.NativeLibraryUtilities;
using HAL.Base;

namespace CTRE
{
  public class HALCanTalonSRX
  {
    public class Constants
    {

      public const int kDefaultControlPeriodMs = 10; //!< default control update rate is 10ms.
                                                     /* mode select enumerations */
      public const int kMode_DutyCycle = 0; //!< Demand is 11bit signed duty cycle [-1023,1023].
      public const int kMode_PositionCloseLoop = 1; //!< Position PIDF.
      public const int kMode_VelocityCloseLoop = 2; //!< Velocity PIDF.
      public const int kMode_CurrentCloseLoop = 3; //!< Current close loop - not done.
      public const int kMode_VoltCompen = 4; //!< Voltage Compensation Mode - not done.  Demand is fixed pt target 8.8 volts.
      public const int kMode_SlaveFollower = 5; //!< Demand is the 6 bit Device ID of the 'master' TALON SRX.
      public const int kMode_NoDrive = 15; //!< Zero the output (honors brake/coast) regardless of demand.  Might be useful if we need to change modes but can't atomically change all the signals we want in between.
                                           /* limit switch enumerations */
      public const int kLimitSwitchOverride_UseDefaultsFromFlash = 1;
      public const int kLimitSwitchOverride_DisableFwd_DisableRev = 4;
      public const int kLimitSwitchOverride_DisableFwd_EnableRev = 5;
      public const int kLimitSwitchOverride_EnableFwd_DisableRev = 6;
      public const int kLimitSwitchOverride_EnableFwd_EnableRev = 7;
      /* brake override enumerations */
      public const int kBrakeOverride_UseDefaultsFromFlash = 0;
      public const int kBrakeOverride_OverrideCoast = 1;
      public const int kBrakeOverride_OverrideBrake = 2;
      /* feedback device enumerations */
      public const int kFeedbackDev_DigitalQuadEnc = 0;
      public const int kFeedbackDev_AnalogPot = 2;
      public const int kFeedbackDev_AnalogEncoder = 3;
      public const int kFeedbackDev_CountEveryRisingEdge = 4;
      public const int kFeedbackDev_CountEveryFallingEdge = 5;
      public const int kFeedbackDev_PosIsPulseWidth = 8;
      /* ProfileSlotSelect enumerations*/
      public const int kProfileSlotSelect_Slot0 = 0;
      public const int kProfileSlotSelect_Slot1 = 1;
      /* Motion Profile status bits */
      public const int kMotionProfileFlag_ActTraj_IsValid = 0x1;
      public const int kMotionProfileFlag_HasUnderrun = 0x2;
      public const int kMotionProfileFlag_IsUnderrun = 0x4;
      public const int kMotionProfileFlag_ActTraj_IsLast = 0x8;
      public const int kMotionProfileFlag_ActTraj_VelOnly = 0x10;
      /* status frame rate types */
      public const int kStatusFrame_General = 0;
      public const int kStatusFrame_Feedback = 1;
      public const int kStatusFrame_Encoder = 2;
      public const int kStatusFrame_AnalogTempVbat = 3;
    }

    public enum ParamID
    {
      eProfileParamSlot0_P = 1,
      eProfileParamSlot0_I = 2,
      eProfileParamSlot0_D = 3,
      eProfileParamSlot0_F = 4,
      eProfileParamSlot0_IZone = 5,
      eProfileParamSlot0_CloseLoopRampRate = 6,
      eProfileParamSlot1_P = 11,
      eProfileParamSlot1_I = 12,
      eProfileParamSlot1_D = 13,
      eProfileParamSlot1_F = 14,
      eProfileParamSlot1_IZone = 15,
      eProfileParamSlot1_CloseLoopRampRate = 16,
      eProfileParamSoftLimitForThreshold = 21,
      eProfileParamSoftLimitRevThreshold = 22,
      eProfileParamSoftLimitForEnable = 23,
      eProfileParamSoftLimitRevEnable = 24,
      eOnBoot_BrakeMode = 31,
      eOnBoot_LimitSwitch_Forward_NormallyClosed = 32,
      eOnBoot_LimitSwitch_Reverse_NormallyClosed = 33,
      eOnBoot_LimitSwitch_Forward_Disable = 34,
      eOnBoot_LimitSwitch_Reverse_Disable = 35,
      eFault_OverTemp = 41,
      eFault_UnderVoltage = 42,
      eFault_ForLim = 43,
      eFault_RevLim = 44,
      eFault_HardwareFailure = 45,
      eFault_ForSoftLim = 46,
      eFault_RevSoftLim = 47,
      eStckyFault_OverTemp = 48,
      eStckyFault_UnderVoltage = 49,
      eStckyFault_ForLim = 50,
      eStckyFault_RevLim = 51,
      eStckyFault_ForSoftLim = 52,
      eStckyFault_RevSoftLim = 53,
      eAppliedThrottle = 61,
      eCloseLoopErr = 62,
      eFeedbackDeviceSelect = 63,
      eRevMotDuringCloseLoopEn = 64,
      eModeSelect = 65,
      eProfileSlotSelect = 66,
      eRampThrottle = 67,
      eRevFeedbackSensor = 68,
      eLimitSwitchEn = 69,
      eLimitSwitchClosedFor = 70,
      eLimitSwitchClosedRev = 71,
      eSensorPosition = 73,
      eSensorVelocity = 74,
      eCurrent = 75,
      eBrakeIsEnabled = 76,
      eEncPosition = 77,
      eEncVel = 78,
      eEncIndexRiseEvents = 79,
      eQuadApin = 80,
      eQuadBpin = 81,
      eQuadIdxpin = 82,
      eAnalogInWithOv = 83,
      eAnalogInVel = 84,
      eTemp = 85,
      eBatteryV = 86,
      eResetCount = 87,
      eResetFlags = 88,
      eFirmVers = 89,
      eSettingsChanged = 90,
      eQuadFilterEn = 91,
      ePidIaccum = 93,
      eStatus1FrameRate = 94, // TALON_Status_1_General_10ms_t
      eStatus2FrameRate = 95, // TALON_Status_2_Feedback_20ms_t
      eStatus3FrameRate = 96, // TALON_Status_3_Enctre_100ms_t
      eStatus4FrameRate = 97, // TALON_Status_4_AinTempVbat_100ms_t
      eStatus6FrameRate = 98, // TALON_Status_6_Eol_t
      eStatus7FrameRate = 99, // TALON_Status_7_Debug_200ms_t
      eClearPositionOnIdx = 100,
      //reserved,
      //reserved,
      //reserved,
      ePeakPosOutput = 104,
      eNominalPosOutput = 105,
      ePeakNegOutput = 106,
      eNominalNegOutput = 107,
      eQuadIdxPolarity = 108,
      eStatus8FrameRate = 109, // TALON_Status_8_PulseWid_100ms_t
      eAllowPosOverflow = 110,
      eProfileParamSlot0_AllowableClosedLoopErr = 111,
      eNumberPotTurns = 112,
      eNumberEncoderCPR = 113,
      ePwdPosition = 114,
      eAinPosition = 115,
      eProfileParamVcompRate = 116,
      eProfileParamSlot1_AllowableClosedLoopErr = 117,
      eStatus9FrameRate = 118, // TALON_Status_9_MotProfBuffer_100ms_t
      eMotionProfileHasUnderrunErr = 119,
      eReserved120 = 120,
      eLegac
    }


    private static readonly bool s_libraryLoaded;
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        public static NativeLibraryLoader NativeLoader { get; }
        private static readonly string s_libraryLocation;
        private static readonly bool s_useCommandLineFile;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable
        private static readonly bool s_runFinalizer;

        // private constructor. Only used for our unload finalizer
        private HALCanTalonSRX() { }
        private void Ping() { } // Used to force compilation
        // static variable used only for interop purposes
        private static readonly HALCanTalonSRX finalizeInterop = new HALCanTalonSRX();
        ~HALCanTalonSRX()
        {
            // If we did not successfully get constructed, we don't need to destruct
            if (!s_runFinalizer) return;
            //Sets logger to null so no logger gets called back.

            NativeLoader.LibraryLoader.UnloadLibrary();

            try
            {
                //Don't delete file if we are using a specified file.
                if (!s_useCommandLineFile && File.Exists(s_libraryLocation))
                {
                    File.Delete(s_libraryLocation);
                }
            }
            catch
            {
                //Any errors just ignore.
            }
        }

        /// <summary>
        /// Static constructor
        /// </summary>
#if !NETSTANDARD
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
#endif
        static HALCanTalonSRX()
        {
            if (!s_libraryLoaded)
            {
                try
                {
                    //finalizeInterop.Ping();
                    string[] commandArgs = Environment.GetCommandLineArgs();
                    foreach (var commandArg in commandArgs)
                    {
                        //search for a line with the prefix "-ctre:"
                        if (commandArg.ToLower().Contains("-ctre:"))
                        {
                            //Split line to get the library.
                            int splitLoc = commandArg.IndexOf(':');
                            string file = commandArg.Substring(splitLoc + 1);

                            //If the file exists, just return it so dlopen can load it.
                            if (File.Exists(file))
                            {
                                s_libraryLocation = file;
                                s_useCommandLineFile = true;
                            }
                        }
                    }

                    const string resourceRoot = "FRC.WPILib.CTRE.Libraries.";


                    if (File.Exists("/usr/local/frc/bin/frcRunRobot.sh"))
                    {
                        NativeLoader = new NativeLibraryLoader();
                        // RoboRIO
                        if (s_useCommandLineFile)
                        {
                            NativeLoader.LoadNativeLibrary<HALCanTalonSRX>(new RoboRioLibraryLoader(), s_libraryLocation, true);
                        }
                        else
                        {
                            NativeLoader.LoadNativeLibrary<HALCanTalonSRX>(new RoboRioLibraryLoader(), resourceRoot + "libctreextern.so");
                            s_libraryLocation = NativeLoader.LibraryLocation;
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("Desktop is not supported with CAN Talon SRX");
                    }

                    NativeDelegateInitializer.SetupNativeDelegates<HALCanTalonSRX>(NativeLoader);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }
                s_runFinalizer = true;
                s_libraryLoaded = true;
            }

            // call cv to enable redirecting 
        }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate IntPtr ctre_TalonSRX_Create3Delegate(int deviceNumber, int controlPeriodMs, int enablePeriodMs);
        [NativeDelegate] internal static ctre_TalonSRX_Create3Delegate ctre_TalonSRX_Create3;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ctre_TalonSRX_DestroyDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_DestroyDelegate ctre_TalonSRX_Destroy;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ctre_TalonSRX_SetDelegate(IntPtr handle, double value);
        [NativeDelegate] internal static ctre_TalonSRX_SetDelegate ctre_TalonSRX_Set;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetParamDelegate(IntPtr handle, int paramEnum, double value);
        [NativeDelegate] internal static ctre_TalonSRX_SetParamDelegate ctre_TalonSRX_SetParam;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_RequestParamDelegate(IntPtr handle, int paramEnum);
        [NativeDelegate] internal static ctre_TalonSRX_RequestParamDelegate ctre_TalonSRX_RequestParam;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetParamResponseDelegate(IntPtr handle, int paramEnum, ref double value);
        [NativeDelegate] internal static ctre_TalonSRX_GetParamResponseDelegate ctre_TalonSRX_GetParamResponse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetParamResponseInt32Delegate(IntPtr handle, int paramEnum, ref int value);
        [NativeDelegate] internal static ctre_TalonSRX_GetParamResponseInt32Delegate ctre_TalonSRX_GetParamResponseInt32;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetPgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] internal static ctre_TalonSRX_SetPgainDelegate ctre_TalonSRX_SetPgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetIgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] internal static ctre_TalonSRX_SetIgainDelegate ctre_TalonSRX_SetIgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetDgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] internal static ctre_TalonSRX_SetDgainDelegate ctre_TalonSRX_SetDgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetFgainDelegate(IntPtr handle, int slotIdx, double gain);
        [NativeDelegate] internal static ctre_TalonSRX_SetFgainDelegate ctre_TalonSRX_SetFgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetIzoneDelegate(IntPtr handle, int slotIdx, int zone);
        [NativeDelegate] internal static ctre_TalonSRX_SetIzoneDelegate ctre_TalonSRX_SetIzone;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetCloseLoopRampRateDelegate(IntPtr handle, int slotIdx, int closeLoopRampRate);
        [NativeDelegate] internal static ctre_TalonSRX_SetCloseLoopRampRateDelegate ctre_TalonSRX_SetCloseLoopRampRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetVoltageCompensationRateDelegate(IntPtr handle, double voltagePerMs);
        [NativeDelegate] internal static ctre_TalonSRX_SetVoltageCompensationRateDelegate ctre_TalonSRX_SetVoltageCompensationRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetSensorPositionDelegate(IntPtr handle, int pos);
        [NativeDelegate] internal static ctre_TalonSRX_SetSensorPositionDelegate ctre_TalonSRX_SetSensorPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetForwardSoftLimitDelegate(IntPtr handle, int forwardLimit);
        [NativeDelegate] internal static ctre_TalonSRX_SetForwardSoftLimitDelegate ctre_TalonSRX_SetForwardSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetReverseSoftLimitDelegate(IntPtr handle, int reverseLimit);
        [NativeDelegate] internal static ctre_TalonSRX_SetReverseSoftLimitDelegate ctre_TalonSRX_SetReverseSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetForwardSoftEnableDelegate(IntPtr handle, int enable);
        [NativeDelegate] internal static ctre_TalonSRX_SetForwardSoftEnableDelegate ctre_TalonSRX_SetForwardSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetReverseSoftEnableDelegate(IntPtr handle, int enable);
        [NativeDelegate] internal static ctre_TalonSRX_SetReverseSoftEnableDelegate ctre_TalonSRX_SetReverseSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetPgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] internal static ctre_TalonSRX_GetPgainDelegate ctre_TalonSRX_GetPgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetIgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] internal static ctre_TalonSRX_GetIgainDelegate ctre_TalonSRX_GetIgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetDgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] internal static ctre_TalonSRX_GetDgainDelegate ctre_TalonSRX_GetDgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFgainDelegate(IntPtr handle, int slotIdx, ref double gain);
        [NativeDelegate] internal static ctre_TalonSRX_GetFgainDelegate ctre_TalonSRX_GetFgain;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetIzoneDelegate(IntPtr handle, int slotIdx, ref int zone);
        [NativeDelegate] internal static ctre_TalonSRX_GetIzoneDelegate ctre_TalonSRX_GetIzone;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetCloseLoopRampRateDelegate(IntPtr handle, int slotIdx, ref int closeLoopRampRate);
        [NativeDelegate] internal static ctre_TalonSRX_GetCloseLoopRampRateDelegate ctre_TalonSRX_GetCloseLoopRampRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetVoltageCompensationRateDelegate(IntPtr handle, ref double voltagePerMs);
        [NativeDelegate] internal static ctre_TalonSRX_GetVoltageCompensationRateDelegate ctre_TalonSRX_GetVoltageCompensationRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetForwardSoftLimitDelegate(IntPtr handle, ref int forwardLimit);
        [NativeDelegate] internal static ctre_TalonSRX_GetForwardSoftLimitDelegate ctre_TalonSRX_GetForwardSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetReverseSoftLimitDelegate(IntPtr handle, ref int reverseLimit);
        [NativeDelegate] internal static ctre_TalonSRX_GetReverseSoftLimitDelegate ctre_TalonSRX_GetReverseSoftLimit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetForwardSoftEnableDelegate(IntPtr handle, ref int enable);
        [NativeDelegate] internal static ctre_TalonSRX_GetForwardSoftEnableDelegate ctre_TalonSRX_GetForwardSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetReverseSoftEnableDelegate(IntPtr handle, ref int enable);
        [NativeDelegate] internal static ctre_TalonSRX_GetReverseSoftEnableDelegate ctre_TalonSRX_GetReverseSoftEnable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetPulseWidthRiseToFallUsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetPulseWidthRiseToFallUsDelegate ctre_TalonSRX_GetPulseWidthRiseToFallUs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_IsPulseWidthSensorPresentDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_IsPulseWidthSensorPresentDelegate ctre_TalonSRX_IsPulseWidthSensorPresent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetModeSelect2Delegate(IntPtr handle, int modeSelect, int demand);
        [NativeDelegate] internal static ctre_TalonSRX_SetModeSelect2Delegate ctre_TalonSRX_SetModeSelect2;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetStatusFrameRateDelegate(IntPtr handle, int frameEnum, int periodMs);
        [NativeDelegate] internal static ctre_TalonSRX_SetStatusFrameRateDelegate ctre_TalonSRX_SetStatusFrameRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_ClearStickyFaultsDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_ClearStickyFaultsDelegate ctre_TalonSRX_ClearStickyFaults;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ctre_TalonSRX_ChangeMotionControlFramePeriodDelegate(IntPtr handle, int periodMs);
        [NativeDelegate] internal static ctre_TalonSRX_ChangeMotionControlFramePeriodDelegate ctre_TalonSRX_ChangeMotionControlFramePeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ctre_TalonSRX_ClearMotionProfileTrajectoriesDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_ClearMotionProfileTrajectoriesDelegate ctre_TalonSRX_ClearMotionProfileTrajectories;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate int ctre_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_GetMotionProfileTopLevelBufferCountDelegate ctre_TalonSRX_GetMotionProfileTopLevelBufferCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate int ctre_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_IsMotionProfileTopLevelBufferFullDelegate ctre_TalonSRX_IsMotionProfileTopLevelBufferFull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_PushMotionProfileTrajectoryDelegate(IntPtr handle, int targPos, int targVel, int profileSlotSelect, int timeDurMs, int velOnly, int isLastPoint, int zeroPos);
        [NativeDelegate] internal static ctre_TalonSRX_PushMotionProfileTrajectoryDelegate ctre_TalonSRX_PushMotionProfileTrajectory;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ctre_TalonSRX_ProcessMotionProfileBufferDelegate(IntPtr handle);
        [NativeDelegate] internal static ctre_TalonSRX_ProcessMotionProfileBufferDelegate ctre_TalonSRX_ProcessMotionProfileBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetMotionProfileStatusDelegate(IntPtr handle, ref int flags, ref int profileSlotSelect, ref int targPos, ref int targVel, ref int topBufferRemaining, ref int topBufferCnt, ref int btmBufferCnt, ref int outputEnable);
        [NativeDelegate] internal static ctre_TalonSRX_GetMotionProfileStatusDelegate ctre_TalonSRX_GetMotionProfileStatus;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_OverTempDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_OverTempDelegate ctre_TalonSRX_GetFault_OverTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_UnderVoltageDelegate ctre_TalonSRX_GetFault_UnderVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_ForLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_ForLimDelegate ctre_TalonSRX_GetFault_ForLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_RevLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_RevLimDelegate ctre_TalonSRX_GetFault_RevLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_HardwareFailureDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_HardwareFailureDelegate ctre_TalonSRX_GetFault_HardwareFailure;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_ForSoftLimDelegate ctre_TalonSRX_GetFault_ForSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFault_RevSoftLimDelegate ctre_TalonSRX_GetFault_RevSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_OverTempDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_OverTempDelegate ctre_TalonSRX_GetStckyFault_OverTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_UnderVoltageDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_UnderVoltageDelegate ctre_TalonSRX_GetStckyFault_UnderVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_ForLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_ForLimDelegate ctre_TalonSRX_GetStckyFault_ForLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_RevLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_RevLimDelegate ctre_TalonSRX_GetStckyFault_RevLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_ForSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_ForSoftLimDelegate ctre_TalonSRX_GetStckyFault_ForSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetStckyFault_RevSoftLimDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetStckyFault_RevSoftLimDelegate ctre_TalonSRX_GetStckyFault_RevSoftLim;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetAppliedThrottleDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetAppliedThrottleDelegate ctre_TalonSRX_GetAppliedThrottle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetCloseLoopErrDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetCloseLoopErrDelegate ctre_TalonSRX_GetCloseLoopErr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFeedbackDeviceSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFeedbackDeviceSelectDelegate ctre_TalonSRX_GetFeedbackDeviceSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetModeSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetModeSelectDelegate ctre_TalonSRX_GetModeSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetLimitSwitchEnDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetLimitSwitchEnDelegate ctre_TalonSRX_GetLimitSwitchEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetLimitSwitchClosedForDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetLimitSwitchClosedForDelegate ctre_TalonSRX_GetLimitSwitchClosedFor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetLimitSwitchClosedRevDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetLimitSwitchClosedRevDelegate ctre_TalonSRX_GetLimitSwitchClosedRev;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetSensorPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetSensorPositionDelegate ctre_TalonSRX_GetSensorPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetSensorVelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetSensorVelocityDelegate ctre_TalonSRX_GetSensorVelocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetCurrentDelegate(IntPtr handle, ref double param);
        [NativeDelegate] internal static ctre_TalonSRX_GetCurrentDelegate ctre_TalonSRX_GetCurrent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetBrakeIsEnabledDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetBrakeIsEnabledDelegate ctre_TalonSRX_GetBrakeIsEnabled;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetEncPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetEncPositionDelegate ctre_TalonSRX_GetEncPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetEncVelDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetEncVelDelegate ctre_TalonSRX_GetEncVel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetEncIndexRiseEventsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetEncIndexRiseEventsDelegate ctre_TalonSRX_GetEncIndexRiseEvents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetQuadApinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetQuadApinDelegate ctre_TalonSRX_GetQuadApin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetQuadBpinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetQuadBpinDelegate ctre_TalonSRX_GetQuadBpin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetQuadIdxpinDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetQuadIdxpinDelegate ctre_TalonSRX_GetQuadIdxpin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetAnalogInWithOvDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetAnalogInWithOvDelegate ctre_TalonSRX_GetAnalogInWithOv;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetAnalogInVelDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetAnalogInVelDelegate ctre_TalonSRX_GetAnalogInVel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetTempDelegate(IntPtr handle, ref double param);
        [NativeDelegate] internal static ctre_TalonSRX_GetTempDelegate ctre_TalonSRX_GetTemp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetBatteryVDelegate(IntPtr handle, ref double param);
        [NativeDelegate] internal static ctre_TalonSRX_GetBatteryVDelegate ctre_TalonSRX_GetBatteryV;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetResetCountDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetResetCountDelegate ctre_TalonSRX_GetResetCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetResetFlagsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetResetFlagsDelegate ctre_TalonSRX_GetResetFlags;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetFirmVersDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetFirmVersDelegate ctre_TalonSRX_GetFirmVers;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetPulseWidthPositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetPulseWidthPositionDelegate ctre_TalonSRX_GetPulseWidthPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetPulseWidthVelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetPulseWidthVelocityDelegate ctre_TalonSRX_GetPulseWidthVelocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetPulseWidthRiseToRiseUsDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetPulseWidthRiseToRiseUsDelegate ctre_TalonSRX_GetPulseWidthRiseToRiseUs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_IsValidDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_IsValidDelegate ctre_TalonSRX_GetActTraj_IsValid;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_ProfileSlotSelectDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_ProfileSlotSelectDelegate ctre_TalonSRX_GetActTraj_ProfileSlotSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_VelOnlyDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_VelOnlyDelegate ctre_TalonSRX_GetActTraj_VelOnly;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_IsLastDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_IsLastDelegate ctre_TalonSRX_GetActTraj_IsLast;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetOutputTypeDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetOutputTypeDelegate ctre_TalonSRX_GetOutputType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetHasUnderrunDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetHasUnderrunDelegate ctre_TalonSRX_GetHasUnderrun;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetIsUnderrunDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetIsUnderrunDelegate ctre_TalonSRX_GetIsUnderrun;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetNextIDDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetNextIDDelegate ctre_TalonSRX_GetNextID;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetBufferIsFullDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetBufferIsFullDelegate ctre_TalonSRX_GetBufferIsFull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetCountDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetCountDelegate ctre_TalonSRX_GetCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_VelocityDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_VelocityDelegate ctre_TalonSRX_GetActTraj_Velocity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_GetActTraj_PositionDelegate(IntPtr handle, ref int param);
        [NativeDelegate] internal static ctre_TalonSRX_GetActTraj_PositionDelegate ctre_TalonSRX_GetActTraj_Position;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetDemandDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetDemandDelegate ctre_TalonSRX_SetDemand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetOverrideLimitSwitchEnDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetOverrideLimitSwitchEnDelegate ctre_TalonSRX_SetOverrideLimitSwitchEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetFeedbackDeviceSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetFeedbackDeviceSelectDelegate ctre_TalonSRX_SetFeedbackDeviceSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetRevMotDuringCloseLoopEnDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetRevMotDuringCloseLoopEnDelegate ctre_TalonSRX_SetRevMotDuringCloseLoopEn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetOverrideBrakeTypeDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetOverrideBrakeTypeDelegate ctre_TalonSRX_SetOverrideBrakeType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetModeSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetModeSelectDelegate ctre_TalonSRX_SetModeSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetProfileSlotSelectDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetProfileSlotSelectDelegate ctre_TalonSRX_SetProfileSlotSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetRampThrottleDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetRampThrottleDelegate ctre_TalonSRX_SetRampThrottle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate CTR_Code ctre_TalonSRX_SetRevFeedbackSensorDelegate(IntPtr handle, int param);
        [NativeDelegate] internal static ctre_TalonSRX_SetRevFeedbackSensorDelegate ctre_TalonSRX_SetRevFeedbackSensor;

  }
}