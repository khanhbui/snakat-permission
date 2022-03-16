using UnityEngine.Assertions;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Snakat
{
    public class Permissions : MonoBehaviour
    {
#if UNITY_IOS
        public const int BLUETOOTH = 101;
        public const int CAMERA = 201;
        public const int MICROPHONE = 301;
        public const int CONTACTS = 401;
        public const int EVENTS = 501;
        public const int REMINDERS = 601;
        public const int LOCATION_WHEN_IN_USE = 701;
        public const int LOCATION_ALWAYS = 702;
        public const int MOTION = 801;
        public const int PHOTOS = 901;
        public const int MEDIA_LIBRARY = 1001;
        public const int SPEECH_RECOGNIZER = 1101;
        public const int SIRI = 1201;
        public const int APP_TRACKING_TRANSPARENCY = 1301;

        public static readonly int[] ALL_PERMISSIONS = new int[] {
            BLUETOOTH,
            CAMERA,
            MICROPHONE,
            CONTACTS,
            EVENTS,
            REMINDERS,
            LOCATION_WHEN_IN_USE,
            LOCATION_ALWAYS,
            MOTION,
            PHOTOS,
            MEDIA_LIBRARY,
            SPEECH_RECOGNIZER,
            SIRI,
            APP_TRACKING_TRANSPARENCY,
        };
#elif UNITY_ANDROID
        public const int CAMERA = 101;

        public const int RECORD_AUDIO = 201;

        public const int READ_CONTACTS = 301;
        public const int WRITE_CONTACTS = 302;
        public const int GET_ACCOUNTS = 303;

        public const int READ_CALENDAR = 401;
        public const int WRITE_CALENDAR = 402;

        public const int ACCESS_FINE_LOCATION = 501;
        public const int ACCESS_COARSE_LOCATION = 502;
        public const int ACCESS_BACKGROUND_LOCATION = 503;

        public const int READ_PHONE_STATE = 601;
        public const int CALL_PHONE = 602;
        public const int USE_SIP = 603;
        public const int READ_PHONE_NUMBERS = 604;
        public const int ANSWER_PHONE_CALLS = 605;
        public const int ADD_VOICEMAIL = 606;

        public const int READ_CALL_LOG = 701;
        public const int WRITE_CALL_LOG = 702;
        public const int PROCESS_OUTGOING_CALLS = 703;

        public const int BODY_SENSORS = 801;
        public const int ACTIVITY_RECOGNITION = 802;

        public const int SEND_SMS = 901;
        public const int RECEIVE_SMS = 902;
        public const int READ_SMS = 903;
        public const int RECEIVE_WAP_PUSH = 904;
        public const int RECEIVE_MMS = 905;

        public const int READ_EXTERNAL_STORAGE = 1001;
        public const int WRITE_EXTERNAL_STORAGE = 1002;

        public static readonly int[] ALL_PERMISSIONS = new int[] {
            CAMERA,
            RECORD_AUDIO,
            READ_CONTACTS,
            WRITE_CONTACTS,
            GET_ACCOUNTS,
            READ_CALENDAR,
            WRITE_CALENDAR,
            ACCESS_FINE_LOCATION,
            ACCESS_COARSE_LOCATION,
            ACCESS_BACKGROUND_LOCATION,
            READ_PHONE_STATE,
            CALL_PHONE,
            USE_SIP,
            READ_PHONE_NUMBERS,
            ANSWER_PHONE_CALLS,
            ADD_VOICEMAIL,
            READ_CALL_LOG,
            WRITE_CALL_LOG,
            PROCESS_OUTGOING_CALLS,
            BODY_SENSORS,
            ACTIVITY_RECOGNITION,
            SEND_SMS,
            RECEIVE_SMS,
            READ_SMS,
            RECEIVE_WAP_PUSH,
            RECEIVE_MMS,
            READ_EXTERNAL_STORAGE,
            WRITE_EXTERNAL_STORAGE,
    };
#endif

        #region Singleton
        private static Permissions _Instance;

        internal static Permissions Instance {
            get {
                CreateInstanceIfNeeded ();
                return _Instance;
            }
        }

        private static void CreateInstanceIfNeeded ()
        {
            if (_Instance != null) {
                return;
            }

            _Instance = FindObjectOfType<Permissions> ();
            if (_Instance != null) {
                return;
            }

            var go = new GameObject ("SnakatPermissions");
            _Instance = go.AddComponent<Permissions> ();
        }

        private void Awake ()
        {
            DontDestroyOnLoad (gameObject);
        }

        private void OnDestroy ()
        {
            _Instance = null;
        }
        #endregion


        #region 
        private static readonly Queue<Action> _ExecutionQueue = new Queue<Action> ();

        private void Update ()
        {
            lock (_ExecutionQueue) {
                while (_ExecutionQueue.Count > 0) {
                    _ExecutionQueue.Dequeue ().Invoke ();
                }
            }
        }

        /// <summary>
        /// Add an Action to the queue
        /// </summary>
        /// <param name="action">Function that will be executed from the main thread.</param>
        internal void Enqueue (Action action)
        {
            Enqueue (ActionWrapper (action));
        }

        private void Enqueue (IEnumerator action)
        {
            lock (_ExecutionQueue) {
                _ExecutionQueue.Enqueue (() =>
                {
                    StartCoroutine (action);
                });
            }
        }

        private IEnumerator ActionWrapper (Action a)
        {
            a ();
            yield return null;
        }
        #endregion


        #region Check for Permissions
        /// <summary>
        /// Check if permissions are granted.
        /// </summary>
        public static bool HasPermissions (params int[] permissions)
        {
            CreateInstanceIfNeeded ();
            return PlatformImpl.HasPermissions (permissions);
        }

        /// <summary>
        /// Check if permissions are denied by user.
        /// </summary>
        public static bool HasAlwaysDeniedPermission (params int[] permissions)
        {
            CreateInstanceIfNeeded ();
            return PlatformImpl.HasAlwaysDeniedPermission (permissions);
        }
        #endregion


        #region Request for Permissions
        /// <summary>
        /// Request for Permissions
        /// </summary>
        public static IPermissionRequest Permission (params int[] permissions)
        {
            Assert.IsTrue (Util.ContainAll (permissions, ALL_PERMISSIONS), "There are some undefined permissions.");

            CreateInstanceIfNeeded ();
            return new PermissionRequest (permissions);
        }
        #endregion


        #region Open Setting
        /// <summary>
        /// Open Setting
        /// </summary>
        public static ISettingRequest Setting ()
        {
            CreateInstanceIfNeeded ();
            return new SettingRequest ();
        }
        #endregion


        #region Notification
        /// <summary>
        /// Request for Notification
        /// </summary>
        public static INotificationRequest Notification ()
        {
            CreateInstanceIfNeeded ();
            return new NotificationRequest ();
        }

        /// <summary>
        /// Request for Notification Listener
        /// </summary>
        public static INotificationRequest NotificationListener ()
        {
            CreateInstanceIfNeeded ();
            return new NotificationListenerRequest ();
        }
        #endregion


        #region Write Setting
        /// <summary>
        /// Request for Write System Setting
        /// </summary>
        public static IWriteRequest Write ()
        {
            CreateInstanceIfNeeded ();
            return new WriteRequest ();
        }
        #endregion


        #region Screen Capture
        /// <summary>
        /// Request for Screen Capture
        /// </summary>
        public static IScreenCaptureRequest ScreenCapture ()
        {
            CreateInstanceIfNeeded ();
            return new ScreenCaptureRequest ();
        }
        #endregion
    }
}
