using UnityEngine;
using UnityEngine.UI;

namespace Snakat.Sample
{
    public class SampleScene : MonoBehaviour
    {
        [SerializeField]
        private Text _ResultText;


        #region MonoBehaviour Callbacks
        private void Start ()
        {
#if UNITY_IOS
            _AndroidContainer.SetActive (false);
#elif UNITY_ANDROID
            _IOSContainer.SetActive (false);
#endif
        }
        #endregion


        #region iOS
        [SerializeField]
        private GameObject _IOSContainer;

        public void OnClickIOSButtonBluetooth ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.BLUETOOTH);
#endif
        }

        public void OnClickIOSButtonCamera ()
        {
            RequestPermissions (Permission.CAMERA);
        }

        public void OnClickIOSButtonMicrophone ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.MICROPHONE);
#endif
        }

        public void OnClickIOSButtonContacts ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.CONTACTS);
#endif
        }

        public void OnClickIOSButtonEvents ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.EVENTS);
#endif
        }

        public void OnClickIOSButtonReminders ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.REMINDERS);
#endif
        }

        public void OnClickIOSButtonLocationWhenInUse ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.LOCATION_WHEN_IN_USE);
#endif
        }

        public void OnClickIOSButtonLocationAlways ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.LOCATION_ALWAYS);
#endif
        }

        public void OnClickIOSButtonMotion ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.MOTION);
#endif
        }

        public void OnClickIOSButtonPhotos ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.PHOTOS);
#endif
        }

        public void OnClickIOSButtonMediaLibrary ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.MEDIA_LIBRARY);
#endif
        }

        public void OnClickIOSButtonSpeechRecognizer ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.SPEECH_RECOGNIZER);
#endif
        }

        public void OnClickIOSButtonSiri ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.SIRI);
#endif
        }

        public void OnClickIOSButtonAppTrackingTransparency ()
        {
#if UNITY_IOS
            RequestPermissions (Permission.APP_TRACKING_TRANSPARENCY);
#endif
        }

        public void OnClickIOSButtonAllPermissions ()
        {
            RequestPermissions (Permission.ALL_PERMISSIONS);
        }

        public void OnClickIOSButtonOpenSetting ()
        {
            OpenSetting ();
        }

        public void OnClickIOSButtonNotification ()
        {
            RequestNotification ();
        }
        #endregion


        #region Android
        [SerializeField]
        private GameObject _AndroidContainer;

        public void OnClickAndroidButtonCamera ()
        {
            RequestPermissions (Permission.CAMERA);
        }

        public void OnClickAndroidButtonRecordAudio ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.RECORD_AUDIO);
#endif
        }

        public void OnClickAndroidButtonContacts ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.READ_CONTACTS, Permission.WRITE_CONTACTS, Permission.GET_ACCOUNTS);
#endif
        }

        public void OnClickAndroidButtonCalendar ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.READ_CALENDAR, Permission.WRITE_CALENDAR);
#endif
        }

        public void OnClickAndroidButtonLocation ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.ACCESS_FINE_LOCATION, Permission.ACCESS_COARSE_LOCATION, Permission.ACCESS_BACKGROUND_LOCATION);
#endif
        }

        public void OnClickAndroidButtonPhoneState ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.READ_PHONE_STATE, Permission.CALL_PHONE, Permission.USE_SIP, Permission.READ_PHONE_NUMBERS, Permission.ANSWER_PHONE_CALLS, Permission.ADD_VOICEMAIL);
#endif
        }

        public void OnClickAndroidButtonCallLog ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.READ_CALL_LOG, Permission.WRITE_CALL_LOG, Permission.PROCESS_OUTGOING_CALLS);
#endif
        }

        public void OnClickAndroidButtonBodySensors ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.BODY_SENSORS);
#endif
        }

        public void OnClickAndroidButtonActivityRecognition ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.ACTIVITY_RECOGNITION);
#endif
        }

        public void OnClickAndroidButtonSms ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.SEND_SMS, Permission.RECEIVE_SMS, Permission.READ_SMS, Permission.RECEIVE_WAP_PUSH, Permission.RECEIVE_MMS);
#endif
        }

        public void OnClickAndroidButtonExternalStorage ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permission.READ_EXTERNAL_STORAGE, Permission.WRITE_EXTERNAL_STORAGE);
#endif
        }

        public void OnClickAndroidButtonAllPermissions ()
        {
            RequestPermissions (Permission.ALL_PERMISSIONS);
        }

        public void OnClickAndroidButtonOpenSetting ()
        {
            OpenSetting ();
        }

        public void OnClickAndroidButtonNotification ()
        {
            RequestNotification ();
        }

        public void OnClickAndroidButtonNotificationListener ()
        {
            RequestNotificationListener ();
        }

        public void OnClickAndroidButtonWriteSetting ()
        {
            RequestWriteSetting ();
        }

        public void OnClickAndroidButtonScreenCapture ()
        {
            RequestScreenCapture ();
        }
        #endregion


        #region Helpers
        private void RequestPermissions (params int[] permissions)
        {
            var str = string.Format ("Requesting permissions: [{0}", permissions[0]);
            for (var i = 1; i < permissions.Length; i++) {
                str += string.Format (", {0}", permissions[i]);
            }
            str += "]";
            _ResultText.text = str;

            Permission.Permissions (permissions)
                .OnGranted (() =>
                {
                    _ResultText.text += "\n=> Granted";
                })
                .OnDenied ((int[] denied) =>
                {
                    var str = string.Format ("\n=> Denied [{0}", denied[0]);
                    for (var i = 1; i < denied.Length; i++) {
                        str += string.Format (", {0}", denied[i]);
                    }
                    str += "]";
                    _ResultText.text += str;
                })
                .Start ();
        }

        private void OpenSetting ()
        {
            _ResultText.text = "Opening Setting";

            Permission.Setting ()
                .OnClosed (() =>
                {
                    _ResultText.text += "\n=> Closed";
                })
                .Start ();
        }

        private void RequestNotification ()
        {
            _ResultText.text = "Requesting notification";

            Permission.Notification ()
                .OnGranted (() =>
                {
                    _ResultText.text += "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text += "\n=> Denied";
                })
                .Start ();
        }

        private void RequestNotificationListener ()
        {
            _ResultText.text = "Requesting NotificationListener";

            Permission.NotificationListener ()
                .OnGranted (() =>
                {
                    _ResultText.text += "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text += "\n=> Denied";
                })
                .Start ();
        }

        private void RequestWriteSetting ()
        {
            _ResultText.text = "Requesting Write System Setting";

            Permission.Write ()
                .OnGranted (() =>
                {
                    _ResultText.text += "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text += "\n=> Denied";
                })
                .Start ();
        }

        private void RequestScreenCapture ()
        {
            _ResultText.text = "Requesting Screen Capture";

            Permission.ScreenCapture ()
#if UNITY_ANDROID
            .OnGranted ((AndroidJavaObject intent) =>
            {
                _ResultText.text += string.Format ("\n=> Granted with Intent: {0}", intent);
            })
#else
            .OnGranted (() =>
            {
                _ResultText.text += "\n=> Granted";
            })
#endif
            .OnDenied (() =>
            {
                _ResultText.text += "\n=> Denied";
            })
            .Start ();
        }
        #endregion
    }
}
