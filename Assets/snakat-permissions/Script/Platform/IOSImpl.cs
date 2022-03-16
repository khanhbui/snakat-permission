#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace Snakat
{
    internal static class PlatformImpl
    {
#region Check for Permissions
        [DllImport ("__Internal")]
        private extern static bool HasPermissions (int[] permissions, int count);
        internal static bool HasPermissions (int[] permissions)
        {
            return HasPermissions (permissions, permissions.Length);
        }

        [DllImport ("__Internal")]
        private extern static bool HasAlwaysDeniedPermission (int[] permissions, int count);
        internal static bool HasAlwaysDeniedPermission (int[] permissions)
        {
            return HasAlwaysDeniedPermission (permissions, permissions.Length);
        }
#endregion


#region Request for Permissions
        private static readonly Dictionary<int, IPermissionRequestInvoke> _PermissionRequestDict = new Dictionary<int, IPermissionRequestInvoke> ();
        private static int _PermissionRequestCount = 0;

        private delegate void GrantedPermissionCallback (int requestId);
        private delegate void DeniedPermissionCallback (int requestId, IntPtr permissions, int length);

        [MonoPInvokeCallback (typeof (GrantedPermissionCallback))]
        public static void RequestPermissionOnGranted (int requestId)
        {
            if (!_PermissionRequestDict.ContainsKey (requestId)) {
                return;
            }

            var permissionRequest = _PermissionRequestDict[requestId];
            if (permissionRequest == null) {
                return;
            }

            permissionRequest.InvokeOnGranted ();

            _PermissionRequestDict.Remove (requestId);
        }

        [MonoPInvokeCallback (typeof (DeniedPermissionCallback))]
        public static void RequestPermissionOnDenied (int requestId, IntPtr permissions, int length)
        {
            if (!_PermissionRequestDict.ContainsKey (requestId)) {
                return;
            }

            var permissionRequest = _PermissionRequestDict[requestId];
            if (permissionRequest == null) {
                return;
            }

            var deniedPermissions = Util.CreateArray (permissions, length);
            permissionRequest.InvokeOnDenied (deniedPermissions);

            _PermissionRequestDict.Remove (requestId);
        }

        [DllImport ("__Internal")]
        private extern static void RequestPermissions (int[] permissions, int length, int requestId, GrantedPermissionCallback onGranted, DeniedPermissionCallback onDenied);
        internal static void RequestPermissions (IPermissionRequestInvoke permissionRequest)
        {
            _PermissionRequestDict[++_PermissionRequestCount] = permissionRequest;

            var permissions = permissionRequest.Permissions;
            RequestPermissions (permissions, permissions.Length, _PermissionRequestCount, RequestPermissionOnGranted, RequestPermissionOnDenied);
        }
#endregion


#region Open Setting
        private static ISettingRequestInvoke _SettingRequest;

        private delegate void OpenSettingCallback ();

        [MonoPInvokeCallback (typeof (OpenSettingCallback))]
        public static void SettingOnClosed ()
        {
            if (_SettingRequest == null) {
                return;
            }

            _SettingRequest.InvokeOnClosed ();
            _SettingRequest = null;
        }

        [DllImport ("__Internal")]
        private extern static void OpenSetting (OpenSettingCallback onClosed);
        internal static void OpenSetting (ISettingRequestInvoke settingRequest)
        {
            if (_SettingRequest != null) {
                return;
            }
            _SettingRequest = settingRequest;

            OpenSetting (SettingOnClosed);
        }
#endregion

#region Notification
        private static INotificationRequestInvoke _NotificationRequest;

        private delegate void NotificationCallback (bool granted);

        [MonoPInvokeCallback (typeof (NotificationCallback))]
        public static void NotificationOnCallback (bool granted)
        {
            if (_NotificationRequest == null) {
                return;
            }

            if (granted) {
                _NotificationRequest.InvokeOnGranted ();
            } else {
                _NotificationRequest.InvokeOnDenied ();
            }
            _NotificationRequest = null;
        }

        [DllImport ("__Internal")]
        private extern static void RequestNotification (NotificationCallback callback);
        internal static void RequestNotification (INotificationRequestInvoke notificationRequest)
        {
            if (_NotificationRequest != null) {
                return;
            }
            _NotificationRequest = notificationRequest;

            RequestNotification (NotificationOnCallback);
        }
#endregion


#region Not implement
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
#endregion
    }
}
#endif
