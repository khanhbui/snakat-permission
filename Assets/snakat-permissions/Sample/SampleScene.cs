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
            RequestPermissions (Permissions.BLUETOOTH);
#endif
        }

        public void OnClickIOSButtonCamera ()
        {
            RequestPermissions (Permissions.CAMERA);
        }

        public void OnClickIOSButtonMicrophone ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.MICROPHONE);
#endif
        }

        public void OnClickIOSButtonContacts ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.CONTACTS);
#endif
        }

        public void OnClickIOSButtonEvents ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.EVENTS);
#endif
        }

        public void OnClickIOSButtonReminders ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.REMINDERS);
#endif
        }

        public void OnClickIOSButtonLocationWhenInUse ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.LOCATION_WHEN_IN_USE);
#endif
        }

        public void OnClickIOSButtonLocationAlways ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.LOCATION_ALWAYS);
#endif
        }

        public void OnClickIOSButtonMotion ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.MOTION);
#endif
        }

        public void OnClickIOSButtonPhotos ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.PHOTOS);
#endif
        }

        public void OnClickIOSButtonMediaLibrary ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.MEDIA_LIBRARY);
#endif
        }

        public void OnClickIOSButtonSpeechRecognizer ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.SPEECH_RECOGNIZER);
#endif
        }

        public void OnClickIOSButtonSiri ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.SIRI);
#endif
        }

        public void OnClickIOSButtonAppTrackingTransparency ()
        {
#if UNITY_IOS
            RequestPermissions (Permissions.APP_TRACKING_TRANSPARENCY);
#endif
        }

        public void OnClickIOSButtonAllPermissions ()
        {
            RequestPermissions (Permissions.ALL_PERMISSIONS);
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
            RequestPermissions (Permissions.CAMERA);
        }

        public void OnClickAndroidButtonRecordAudio ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.RECORD_AUDIO);
#endif
        }

        public void OnClickAndroidButtonContacts ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.READ_CONTACTS, Permissions.WRITE_CONTACTS, Permissions.GET_ACCOUNTS);
#endif
        }

        public void OnClickAndroidButtonCalendar ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.READ_CALENDAR, Permissions.WRITE_CALENDAR);
#endif
        }

        public void OnClickAndroidButtonLocation ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.ACCESS_FINE_LOCATION, Permissions.ACCESS_COARSE_LOCATION, Permissions.ACCESS_BACKGROUND_LOCATION);
#endif
        }

        public void OnClickAndroidButtonPhoneState ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.READ_PHONE_STATE, Permissions.CALL_PHONE, Permissions.USE_SIP, Permissions.READ_PHONE_NUMBERS, Permissions.ANSWER_PHONE_CALLS, Permissions.ADD_VOICEMAIL);
#endif
        }

        public void OnClickAndroidButtonCallLog ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.READ_CALL_LOG, Permissions.WRITE_CALL_LOG, Permissions.PROCESS_OUTGOING_CALLS);
#endif
        }

        public void OnClickAndroidButtonBodySensors ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.BODY_SENSORS);
#endif
        }

        public void OnClickAndroidButtonActivityRecognition ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.ACTIVITY_RECOGNITION);
#endif
        }

        public void OnClickAndroidButtonSms ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.SEND_SMS, Permissions.RECEIVE_SMS, Permissions.READ_SMS, Permissions.RECEIVE_WAP_PUSH, Permissions.RECEIVE_MMS);
#endif
        }

        public void OnClickAndroidButtonExternalStorage ()
        {
#if UNITY_ANDROID
            RequestPermissions (Permissions.READ_EXTERNAL_STORAGE, Permissions.WRITE_EXTERNAL_STORAGE);
#endif
        }

        public void OnClickAndroidButtonAllPermissions ()
        {
            RequestPermissions (Permissions.ALL_PERMISSIONS);
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

            Permissions.Permission (permissions)
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
            _ResultText.text = "OpenSetting";

            Permissions.Setting ()
                .OnClosed (() =>
                {
                    _ResultText.text = "OpenSetting.Closed";
                })
                .Start ();
        }

        private void RequestNotification ()
        {
            _ResultText.text = "Requesting notification";

            Permissions.Notification ()
                .OnGranted (() =>
                {
                    _ResultText.text = "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text = "\n=> Denied";
                })
                .Start ();
        }

        private void RequestNotificationListener ()
        {
            _ResultText.text = "Requesting NotificationListener";

            Permissions.NotificationListener ()
                .OnGranted (() =>
                {
                    _ResultText.text = "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text = "\n=> Denied";
                })
                .Start ();
        }

        private void RequestWriteSetting ()
        {
            _ResultText.text = "Requesting Write System Setting";

            Permissions.Write ()
                .OnGranted (() =>
                {
                    _ResultText.text = "\n=> Granted";
                })
                .OnDenied (() =>
                {
                    _ResultText.text = "\n=> Denied";
                })
                .Start ();
        }

        private void RequestScreenCapture ()
        {
            _ResultText.text = "Requesting Screen Capture";

            Permissions.ScreenCapture ()
#if !UNITY_EDITOR && UNITY_ANDROID
            .OnGranted ((AndroidJavaObject intent) =>
            {
                _ResultText.text = string.Format ("\n=> Granted with Intent: {0}", intent);
            })
#else
            .OnGranted (() =>
            {
                _ResultText.text = "\n=> Granted";
            })
#endif
            .OnDenied (() =>
            {
                _ResultText.text = "\n=> Denied";
            })
            .Start ();
        }
        #endregion
    }
}
