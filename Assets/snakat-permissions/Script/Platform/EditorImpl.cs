#if UNITY_EDITOR
using System.Collections.Generic;

namespace Snakat
{
    internal static class PlatformImpl
    {
        private static readonly List<int> _permissions = new List<int> ();

        internal static bool HasPermissions (int[] permissions)
        {
            for (var i = 0; i < permissions.Length; i++) {
                if (!_permissions.Contains (permissions[i])) {
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
                if (!_permissions.Contains (permission)) {
                    _permissions.Add (permission);
                }
            }

            Permissions.Instance.Enqueue (() =>
            {
                permissionRequest.InvokeOnGranted ();
            });
        }

        internal static void OpenSetting (ISettingRequestInvoke settingRequest)
        {
            Permissions.Instance.Enqueue (() =>
            {
                settingRequest.InvokeOnClosed ();
            });
        }

        internal static void RequestNotification (INotificationRequestInvoke notificationRequest)
        {
            Permissions.Instance.Enqueue (() =>
            {
                notificationRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestNotificationListener (INotificationRequestInvoke notificationRequest)
        {
            Permissions.Instance.Enqueue (() =>
            {
                notificationRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestWriteSystemSetting (IWriteRequestInvoke writeRequest)
        {
            Permissions.Instance.Enqueue (() =>
            {
                writeRequest.InvokeOnGranted ();
            });
        }

        internal static void RequestScreenCapture (IScreenCaptureRequestInvoke screenCaptureRequest)
        {
            Permissions.Instance.Enqueue (() =>
            {
                screenCaptureRequest.InvokeOnGranted ();
            });
        }
    }
}
#endif
