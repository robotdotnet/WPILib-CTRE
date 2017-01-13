#include "ExternTalonSRX.h"

extern "C" {
void *ctre_TalonSRX_Create3(int deviceNumber, int controlPeriodMs, int enablePeriodMs)
{
  return new CanTalonSRX(deviceNumber, controlPeriodMs, enablePeriodMs);
}
void *ctre_TalonSRX_Create2(int deviceNumber, int controlPeriodMs)
{
  return new CanTalonSRX(deviceNumber, controlPeriodMs);
}
void *ctre_TalonSRX_Create1(int deviceNumber)
{
  return new CanTalonSRX(deviceNumber);
}
void ctre_TalonSRX_Destroy(void *handle)
{
  delete (CanTalonSRX*)handle;
}
void ctre_TalonSRX_Set(void *handle, double value)
{
  return ((CanTalonSRX*)handle)->Set(value);
}
CTR_Code ctre_TalonSRX_SetParam(void *handle, int paramEnum, double value)
{
  return ((CanTalonSRX*)handle)->SetParam((CanTalonSRX::param_t)paramEnum, value);
}
CTR_Code ctre_TalonSRX_RequestParam(void *handle, int paramEnum)
{
  return ((CanTalonSRX*)handle)->RequestParam((CanTalonSRX::param_t)paramEnum);
}
CTR_Code ctre_TalonSRX_GetParamResponse(void *handle, int paramEnum, double *value)
{
  return ((CanTalonSRX*)handle)->GetParamResponse((CanTalonSRX::param_t)paramEnum, *value);
}
CTR_Code ctre_TalonSRX_GetParamResponseInt32(void *handle, int paramEnum, int *value)
{
  return ((CanTalonSRX*)handle)->GetParamResponseInt32((CanTalonSRX::param_t)paramEnum, *value);
}
CTR_Code ctre_TalonSRX_SetPgain(void *handle, int slotIdx, double gain)
{
  return ((CanTalonSRX*)handle)->SetPgain((unsigned)slotIdx, gain);
}
CTR_Code ctre_TalonSRX_SetIgain(void *handle, int slotIdx, double gain)
{
  return ((CanTalonSRX*)handle)->SetIgain((unsigned)slotIdx, gain);
}
CTR_Code ctre_TalonSRX_SetDgain(void *handle, int slotIdx, double gain)
{
  return ((CanTalonSRX*)handle)->SetDgain((unsigned)slotIdx, gain);
}
CTR_Code ctre_TalonSRX_SetFgain(void *handle, int slotIdx, double gain)
{
  return ((CanTalonSRX*)handle)->SetFgain((unsigned)slotIdx, gain);
}
CTR_Code ctre_TalonSRX_SetIzone(void *handle, int slotIdx, int zone)
{
  return ((CanTalonSRX*)handle)->SetIzone((unsigned)slotIdx, zone);
}
CTR_Code ctre_TalonSRX_SetCloseLoopRampRate(void *handle, int slotIdx, int closeLoopRampRate)
{
  return ((CanTalonSRX*)handle)->SetCloseLoopRampRate((unsigned)slotIdx, closeLoopRampRate);
}
CTR_Code ctre_TalonSRX_SetVoltageCompensationRate(void *handle, double voltagePerMs)
{
  return ((CanTalonSRX*)handle)->SetVoltageCompensationRate(voltagePerMs);
}
CTR_Code ctre_TalonSRX_SetSensorPosition(void *handle, int pos)
{
  return ((CanTalonSRX*)handle)->SetSensorPosition(pos);
}
CTR_Code ctre_TalonSRX_SetForwardSoftLimit(void *handle, int forwardLimit)
{
  return ((CanTalonSRX*)handle)->SetForwardSoftLimit(forwardLimit);
}
CTR_Code ctre_TalonSRX_SetReverseSoftLimit(void *handle, int reverseLimit)
{
  return ((CanTalonSRX*)handle)->SetReverseSoftLimit(reverseLimit);
}
CTR_Code ctre_TalonSRX_SetForwardSoftEnable(void *handle, int enable)
{
  return ((CanTalonSRX*)handle)->SetForwardSoftEnable(enable);
}
CTR_Code ctre_TalonSRX_SetReverseSoftEnable(void *handle, int enable)
{
  return ((CanTalonSRX*)handle)->SetReverseSoftEnable(enable);
}
CTR_Code ctre_TalonSRX_GetPgain(void *handle, int slotIdx, double *gain)
{
  return ((CanTalonSRX*)handle)->GetPgain((unsigned)slotIdx, *gain);
}
CTR_Code ctre_TalonSRX_GetIgain(void *handle, int slotIdx, double *gain)
{
  return ((CanTalonSRX*)handle)->GetIgain((unsigned)slotIdx, *gain);
}
CTR_Code ctre_TalonSRX_GetDgain(void *handle, int slotIdx, double *gain)
{
  return ((CanTalonSRX*)handle)->GetDgain((unsigned)slotIdx, *gain);
}
CTR_Code ctre_TalonSRX_GetFgain(void *handle, int slotIdx, double *gain)
{
  return ((CanTalonSRX*)handle)->GetFgain((unsigned)slotIdx, *gain);
}
CTR_Code ctre_TalonSRX_GetIzone(void *handle, int slotIdx, int *zone)
{
  return ((CanTalonSRX*)handle)->GetIzone((unsigned)slotIdx, *zone);
}
CTR_Code ctre_TalonSRX_GetCloseLoopRampRate(void *handle, int slotIdx, int *closeLoopRampRate)
{
  return ((CanTalonSRX*)handle)->GetCloseLoopRampRate((unsigned)slotIdx, *closeLoopRampRate);
}
CTR_Code ctre_TalonSRX_GetVoltageCompensationRate(void *handle, double *voltagePerMs)
{
  return ((CanTalonSRX*)handle)->GetVoltageCompensationRate(*voltagePerMs);
}
CTR_Code ctre_TalonSRX_GetForwardSoftLimit(void *handle, int *forwardLimit)
{
  return ((CanTalonSRX*)handle)->GetForwardSoftLimit(*forwardLimit);
}
CTR_Code ctre_TalonSRX_GetReverseSoftLimit(void *handle, int *reverseLimit)
{
  return ((CanTalonSRX*)handle)->GetReverseSoftLimit(*reverseLimit);
}
CTR_Code ctre_TalonSRX_GetForwardSoftEnable(void *handle, int *enable)
{
  return ((CanTalonSRX*)handle)->GetForwardSoftEnable(*enable);
}
CTR_Code ctre_TalonSRX_GetReverseSoftEnable(void *handle, int *enable)
{
  return ((CanTalonSRX*)handle)->GetReverseSoftEnable(*enable);
}
CTR_Code ctre_TalonSRX_GetPulseWidthRiseToFallUs(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetPulseWidthRiseToFallUs(*param);
}
CTR_Code ctre_TalonSRX_IsPulseWidthSensorPresent(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->IsPulseWidthSensorPresent(*param);
}
CTR_Code ctre_TalonSRX_SetModeSelect2(void *handle, int modeSelect, int demand)
{
  return ((CanTalonSRX*)handle)->SetModeSelect(modeSelect, demand);
}
CTR_Code ctre_TalonSRX_SetStatusFrameRate(void *handle, int frameEnum, int periodMs)
{
  return ((CanTalonSRX*)handle)->SetStatusFrameRate((unsigned)frameEnum, (unsigned)periodMs);
}
CTR_Code ctre_TalonSRX_ClearStickyFaults(void *handle)
{
  return ((CanTalonSRX*)handle)->ClearStickyFaults();
}
void ctre_TalonSRX_ChangeMotionControlFramePeriod(void *handle, int periodMs)
{
  return ((CanTalonSRX*)handle)->ChangeMotionControlFramePeriod((uint32_t)periodMs);
}
void ctre_TalonSRX_ClearMotionProfileTrajectories(void *handle)
{
  return ((CanTalonSRX*)handle)->ClearMotionProfileTrajectories();
}
int ctre_TalonSRX_GetMotionProfileTopLevelBufferCount(void *handle)
{
  return ((CanTalonSRX*)handle)->GetMotionProfileTopLevelBufferCount();
}
int ctre_TalonSRX_IsMotionProfileTopLevelBufferFull(void *handle)
{
  return ((CanTalonSRX*)handle)->IsMotionProfileTopLevelBufferFull();
}
CTR_Code ctre_TalonSRX_PushMotionProfileTrajectory(void *handle, int targPos, int targVel, int profileSlotSelect, int timeDurMs, int velOnly, int isLastPoint, int zeroPos)
{
  return ((CanTalonSRX*)handle)->PushMotionProfileTrajectory(targPos, targVel, profileSlotSelect, timeDurMs, velOnly, isLastPoint, zeroPos);
}
void ctre_TalonSRX_ProcessMotionProfileBuffer(void *handle)
{
  return ((CanTalonSRX*)handle)->ProcessMotionProfileBuffer();
}
CTR_Code ctre_TalonSRX_GetMotionProfileStatus(void *handle, int *flags, int *profileSlotSelect, int *targPos, int *targVel, int *topBufferRemaining, int *topBufferCnt, int *btmBufferCnt, int *outputEnable)
{
  uint32_t flags_val;
  uint32_t profileSlotSelect_val;
  int32_t targPos_val;
  int32_t targVel_val;
  uint32_t topBufferRemaining_val;
  uint32_t topBufferCnt_val;
  uint32_t btmBufferCnt_val;
  uint32_t outputEnable_val;
  CTR_Code retval = ((CanTalonSRX*)handle)->GetMotionProfileStatus(flags_val, profileSlotSelect_val, targPos_val, targVel_val, topBufferRemaining_val, topBufferCnt_val, btmBufferCnt_val, outputEnable_val);
  *flags = (int)flags_val;
  *profileSlotSelect = (int)profileSlotSelect_val;
  *targPos = (int)targPos_val;
  *targVel = (int)targVel_val;
  *topBufferRemaining = (int)topBufferRemaining_val;
  *topBufferCnt = (int)topBufferCnt_val;
  *btmBufferCnt = (int)btmBufferCnt_val;
  *outputEnable = (int)outputEnable_val;
  return retval;
}
CTR_Code ctre_TalonSRX_GetFault_OverTemp(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_OverTemp(*param);
}
CTR_Code ctre_TalonSRX_GetFault_UnderVoltage(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_UnderVoltage(*param);
}
CTR_Code ctre_TalonSRX_GetFault_ForLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_ForLim(*param);
}
CTR_Code ctre_TalonSRX_GetFault_RevLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_RevLim(*param);
}
CTR_Code ctre_TalonSRX_GetFault_HardwareFailure(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_HardwareFailure(*param);
}
CTR_Code ctre_TalonSRX_GetFault_ForSoftLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_ForSoftLim(*param);
}
CTR_Code ctre_TalonSRX_GetFault_RevSoftLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFault_RevSoftLim(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_OverTemp(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_OverTemp(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_UnderVoltage(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_UnderVoltage(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_ForLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_ForLim(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_RevLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_RevLim(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_ForSoftLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_ForSoftLim(*param);
}
CTR_Code ctre_TalonSRX_GetStckyFault_RevSoftLim(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetStckyFault_RevSoftLim(*param);
}
CTR_Code ctre_TalonSRX_GetAppliedThrottle(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetAppliedThrottle(*param);
}
CTR_Code ctre_TalonSRX_GetCloseLoopErr(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetCloseLoopErr(*param);
}
CTR_Code ctre_TalonSRX_GetFeedbackDeviceSelect(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFeedbackDeviceSelect(*param);
}
CTR_Code ctre_TalonSRX_GetModeSelect(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetModeSelect(*param);
}
CTR_Code ctre_TalonSRX_GetLimitSwitchEn(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetLimitSwitchEn(*param);
}
CTR_Code ctre_TalonSRX_GetLimitSwitchClosedFor(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetLimitSwitchClosedFor(*param);
}
CTR_Code ctre_TalonSRX_GetLimitSwitchClosedRev(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetLimitSwitchClosedRev(*param);
}
CTR_Code ctre_TalonSRX_GetSensorPosition(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetSensorPosition(*param);
}
CTR_Code ctre_TalonSRX_GetSensorVelocity(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetSensorVelocity(*param);
}
CTR_Code ctre_TalonSRX_GetCurrent(void *handle, double *param)
{
  return ((CanTalonSRX*)handle)->GetCurrent(*param);
}
CTR_Code ctre_TalonSRX_GetBrakeIsEnabled(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetBrakeIsEnabled(*param);
}
CTR_Code ctre_TalonSRX_GetEncPosition(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetEncPosition(*param);
}
CTR_Code ctre_TalonSRX_GetEncVel(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetEncVel(*param);
}
CTR_Code ctre_TalonSRX_GetEncIndexRiseEvents(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetEncIndexRiseEvents(*param);
}
CTR_Code ctre_TalonSRX_GetQuadApin(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetQuadApin(*param);
}
CTR_Code ctre_TalonSRX_GetQuadBpin(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetQuadBpin(*param);
}
CTR_Code ctre_TalonSRX_GetQuadIdxpin(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetQuadIdxpin(*param);
}
CTR_Code ctre_TalonSRX_GetAnalogInWithOv(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetAnalogInWithOv(*param);
}
CTR_Code ctre_TalonSRX_GetAnalogInVel(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetAnalogInVel(*param);
}
CTR_Code ctre_TalonSRX_GetTemp(void *handle, double *param)
{
  return ((CanTalonSRX*)handle)->GetTemp(*param);
}
CTR_Code ctre_TalonSRX_GetBatteryV(void *handle, double *param)
{
  return ((CanTalonSRX*)handle)->GetBatteryV(*param);
}
CTR_Code ctre_TalonSRX_GetResetCount(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetResetCount(*param);
}
CTR_Code ctre_TalonSRX_GetResetFlags(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetResetFlags(*param);
}
CTR_Code ctre_TalonSRX_GetFirmVers(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetFirmVers(*param);
}
CTR_Code ctre_TalonSRX_GetPulseWidthPosition(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetPulseWidthPosition(*param);
}
CTR_Code ctre_TalonSRX_GetPulseWidthVelocity(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetPulseWidthVelocity(*param);
}
CTR_Code ctre_TalonSRX_GetPulseWidthRiseToRiseUs(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetPulseWidthRiseToRiseUs(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_IsValid(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_IsValid(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_ProfileSlotSelect(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_ProfileSlotSelect(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_VelOnly(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_VelOnly(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_IsLast(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_IsLast(*param);
}
CTR_Code ctre_TalonSRX_GetOutputType(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetOutputType(*param);
}
CTR_Code ctre_TalonSRX_GetHasUnderrun(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetHasUnderrun(*param);
}
CTR_Code ctre_TalonSRX_GetIsUnderrun(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetIsUnderrun(*param);
}
CTR_Code ctre_TalonSRX_GetNextID(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetNextID(*param);
}
CTR_Code ctre_TalonSRX_GetBufferIsFull(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetBufferIsFull(*param);
}
CTR_Code ctre_TalonSRX_GetCount(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetCount(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_Velocity(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_Velocity(*param);
}
CTR_Code ctre_TalonSRX_GetActTraj_Position(void *handle, int *param)
{
  return ((CanTalonSRX*)handle)->GetActTraj_Position(*param);
}
CTR_Code ctre_TalonSRX_SetDemand(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetDemand(param);
}
CTR_Code ctre_TalonSRX_SetOverrideLimitSwitchEn(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetOverrideLimitSwitchEn(param);
}
CTR_Code ctre_TalonSRX_SetFeedbackDeviceSelect(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetFeedbackDeviceSelect(param);
}
CTR_Code ctre_TalonSRX_SetRevMotDuringCloseLoopEn(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetRevMotDuringCloseLoopEn(param);
}
CTR_Code ctre_TalonSRX_SetOverrideBrakeType(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetOverrideBrakeType(param);
}
CTR_Code ctre_TalonSRX_SetModeSelect(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetModeSelect(param);
}
CTR_Code ctre_TalonSRX_SetProfileSlotSelect(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetProfileSlotSelect(param);
}
CTR_Code ctre_TalonSRX_SetRampThrottle(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetRampThrottle(param);
}
CTR_Code ctre_TalonSRX_SetRevFeedbackSensor(void *handle, int param)
{
  return ((CanTalonSRX*)handle)->SetRevFeedbackSensor(param);
}
}