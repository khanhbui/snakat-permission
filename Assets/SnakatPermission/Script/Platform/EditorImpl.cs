#if UNITY_EDITOR
using System.Collections.Generic;

namespace Snakat
{
    internal static class PlatformImpl
    {
        private static readonly List<int> _Permissions = new List<int> ();

        internal static bool HasPermissions (int[] permissions)
        {
            for (var i = 0; i < permissions.Length; i++) {
                if (!_Permissions.Contains (permissions[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool HasAlwaysDeniedPermission (int[] permissions)
        {
            return false;
        }

        internal static void RequestPermissions (IPermissionRequestInvoke permissionRequest)
        {
            var permissions = permissionRequest.Permissions;
            for (var i = 0; i < permissions.Length; i++) {
                var permission = permissions[i];
                if (!_Permissions.Contains (permission)) {
                    _Permissions.Add (permission);
                }
            }

            Permission.Instance.Enqueue (() =>
            {
                permissionRequest.InvokeOnGranted ();
            });
        }

        internal static void OpenSetting (ISettingRequestInvoke settingRequest)
        {
            Permission.Instance.Enqueue (() =>
            {
                settingRequest.InvokeOnClosed ();
            });
        }

        internal static void RequestNotification (INotificationRequestInvoke notificationRequest)
        {
            Permission.Instance.Enqueue (() =>
            {
                notificationRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestNotificationListener (INotificationRequestInvoke notificationRequest)
        {
            Permission.Instance.Enqueue (() =>
            {
                notificationRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestWriteSystemSetting (IWriteRequestInvoke writeRequest)
        {
            Permission.Instance.Enqueue (() =>
            {
                writeRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestScreenCapture (IScreenCaptureRequestInvoke screenCaptureRequest)
        {
            Permission.Instance.Enqueue (() =>
            {
#if UNITY_ANDROID
                screenCaptureRequest.InvokeOnGranted (null);
#else
                screenCaptureRequest.InvokeOnGranted ();
#endif
            });
        }
    }
}
#endif
