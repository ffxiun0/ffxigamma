using System;
using System.Runtime.InteropServices;

namespace ffxigamma {
    enum EDataFlow {
        eRender = 0,
        eCapture = 1,
        eAll = 2,
    }

    enum ERole {
        eConsole = 0,
        eMultimedia = 1,
        eCommunications = 2,
    }

    enum CLSCTX : uint {
        INPROC_SERVER = 0x1,
        INPROC_HANDLER = 0x2,
        LOCAL_SERVER = 0x4,
        INPROC_SERVER16 = 0x8,
        REMOTE_SERVER = 0x10,
        INPROC_HANDLER16 = 0x20,
        RESERVED1 = 0x40,
        RESERVED2 = 0x80,
        RESERVED3 = 0x100,
        RESERVED4 = 0x200,
        NO_CODE_DOWNLOAD = 0x400,
        RESERVED5 = 0x800,
        NO_CUSTOM_MARSHAL = 0x1000,
        ENABLE_CODE_DOWNLOAD = 0x2000,
        NO_FAILURE_LOG = 0x4000,
        DISABLE_AAA = 0x8000,
        ENABLE_AAA = 0x10000,
        FROM_DEFAULT_CONTEXT = 0x20000,
        ACTIVATE_32_BIT_SERVER = 0x40000,
        ACTIVATE_64_BIT_SERVER = 0x80000,
        ENABLE_CLOAKING = 0x100000,
        APPCONTAINER = 0x400000,
        ACTIVATE_AAA_AS_IU = 0x800000,
        PS_DLL = 0x80000000
    }

    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E"), ComImport]
    class MMDeviceEnumerator { }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDeviceEnumerator {
        int DONOTUSE_EnumAudioEndpoints();
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
        int DONOTUSE_GetDevice();
        int DONOTUSE_RegisterEndpointNotificationCallback();
        int DONOTUSE_UnregisterEndpointNotificationCallback();
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDevice {
        int Activate(
            ref Guid iid,
            CLSCTX dwClsCtx,
            IntPtr pActivationParams,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        int DONOTUSE_OpenPropertyStore();
        int DONOTUSE_GetId();
        int DONOTUSE_GetState();
    }

    [Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionManager {
        int DONOTUSE_GetAudioSessionControl();
        int DONOTUSE_GetSimpleAudioVolume();
    }

    [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionManager2 {
        int DONOTUSE_GetAudioSessionControl();
        int DONOTUSE_GetSimpleAudioVolume();

        int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);
        int DONOTUSE_RegisterSessionNotification();
        int DONOTUSE_UnregisterSessionNotification();
        int DONOTUSE_RegisterDuckNotification();
        int DONOTUSE_UnregisterDuckNotification();
    }

    [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionEnumerator {
        int GetCount(out int SessionCount);
        int GetSession(int SessionCount, out IAudioSessionControl Session);
    }

    [Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionControl {
        int DONOTUSE_GetState();
        int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int DONOTUSE_SetDisplayName();
        int DONOTUSE_GetIconPath();
        int DONOTUSE_SetIconPath();
        int DONOTUSE_GetGroupingParam();
        int DONOTUSE_SetGroupingParam();
        int DONOTUSE_RegisterAudioSessionNotification();
        int DONOTUSE_UnregisterAudioSessionNotification();
    }

    [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionControl2 {
        int DONOTUSE_GetState();
        int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int DONOTUSE_SetDisplayName();
        int DONOTUSE_GetIconPath();
        int DONOTUSE_SetIconPath();
        int DONOTUSE_GetGroupingParam();
        int DONOTUSE_SetGroupingParam();
        int DONOTUSE_RegisterAudioSessionNotification();
        int DONOTUSE_UnregisterAudioSessionNotification();

        int DONOTUSE_GetSessionIdentifier();
        int DONOTUSE_GetSessionInstanceIdentifier();
        int GetProcessId(out uint pRetVal);
        int DONOTUSE_IsSystemSoundsSession();
        int DONOTUSE_SetDuckingPreference();
    };

    [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface ISimpleAudioVolume {
        int SetMasterVolume(float fLevel, ref Guid EventContext);
        int GetMasterVolume(out float pfLevel);
        int SetMute(bool bMute, ref Guid EventContext);
        int GetMute(out bool bMute);
    }

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioEndpointVolume {
        int DONOTUSE_RegisterControlChangeNotify();
        int DONOTUSE_UnregisterControlChangeNotify();
        int GetChannelCount(out uint pnChannelCount);
        int SetMasterVolumeLevel(float fLevelDB, ref Guid pguidEventContext);
        int SetMasterVolumeLevelScalar(float fLevel, ref Guid pguidEventContext);
        int GetMasterVolumeLevel(out float pfLevelDB);
        int GetMasterVolumeLevelScalar(out float pfLevel);
        int SetChannelVolumeLevel(uint nChannel, float fLevelDB, ref Guid pguidEventContext);
        int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, ref Guid pguidEventContext);
        int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
        int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);
        int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, ref Guid pguidEventContext);
        int GetMute(out bool pbMute);
        int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);
        int VolumeStepUp(ref Guid pguidEventContext);
        int VolumeStepDown(ref Guid pguidEventContext);
        int QueryHardwareSupport(out uint pdwHardwareSupportMask);
        int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
    };
}
