package com.snakat.permission;

import com.yanzhenjie.permission.runtime.Permission;

import java.util.List;

class Util {

    private static final int CAMERA = 101;

    private static final int RECORD_AUDIO = 201;

    private static final int READ_CONTACTS = 301;
    private static final int WRITE_CONTACTS = 302;
    private static final int GET_ACCOUNTS = 303;

    private static final int READ_CALENDAR = 401;
    private static final int WRITE_CALENDAR = 402;

    private static final int ACCESS_FINE_LOCATION = 501;
    private static final int ACCESS_COARSE_LOCATION = 502;
    private static final int ACCESS_BACKGROUND_LOCATION = 503;

    private static final int READ_PHONE_STATE = 601;
    private static final int CALL_PHONE = 602;
    private static final int USE_SIP = 603;
    private static final int READ_PHONE_NUMBERS = 604;
    private static final int ANSWER_PHONE_CALLS = 605;
    private static final int ADD_VOICEMAIL = 606;

    private static final int READ_CALL_LOG = 701;
    private static final int WRITE_CALL_LOG = 702;
    private static final int PROCESS_OUTGOING_CALLS = 703;

    private static final int BODY_SENSORS = 801;
    private static final int ACTIVITY_RECOGNITION = 802;

    private static final int SEND_SMS = 901;
    private static final int RECEIVE_SMS = 902;
    private static final int READ_SMS = 903;
    private static final int RECEIVE_WAP_PUSH = 904;
    private static final int RECEIVE_MMS = 905;

    private static final int READ_EXTERNAL_STORAGE = 1001;
    private static final int WRITE_EXTERNAL_STORAGE = 1002;

    public static String[] ints2Permissions(int[] src) {
        int length = src.length;
        String[] ret = new String[length];

        for (int i = 0; i < length; i++) {
            switch (src[i]) {
                case CAMERA:
                    ret[i] = Permission.CAMERA;
                    break;

                case RECORD_AUDIO:
                    ret[i] = Permission.RECORD_AUDIO;
                    break;

                case READ_CONTACTS:
                    ret[i] = Permission.READ_CONTACTS;
                    break;

                case WRITE_CONTACTS:
                    ret[i] = Permission.WRITE_CONTACTS;
                    break;

                case GET_ACCOUNTS:
                    ret[i] = Permission.GET_ACCOUNTS;
                    break;

                case READ_CALENDAR:
                    ret[i] = Permission.READ_CALENDAR;
                    break;

                case WRITE_CALENDAR:
                    ret[i] = Permission.WRITE_CALENDAR;
                    break;

                case ACCESS_FINE_LOCATION:
                    ret[i] = Permission.ACCESS_FINE_LOCATION;
                    break;

                case ACCESS_COARSE_LOCATION:
                    ret[i] = Permission.ACCESS_COARSE_LOCATION;
                    break;

                case ACCESS_BACKGROUND_LOCATION:
                    ret[i] = Permission.ACCESS_BACKGROUND_LOCATION;
                    break;

                case READ_PHONE_STATE:
                    ret[i] = Permission.READ_PHONE_STATE;
                    break;

                case CALL_PHONE:
                    ret[i] = Permission.CALL_PHONE;
                    break;

                case USE_SIP:
                    ret[i] = Permission.USE_SIP;
                    break;

                case READ_PHONE_NUMBERS:
                    ret[i] = Permission.READ_PHONE_NUMBERS;
                    break;

                case ANSWER_PHONE_CALLS:
                    ret[i] = Permission.ANSWER_PHONE_CALLS;
                    break;

                case ADD_VOICEMAIL:
                    ret[i] = Permission.ADD_VOICEMAIL;
                    break;

                case READ_CALL_LOG:
                    ret[i] = Permission.READ_CALL_LOG;
                    break;

                case WRITE_CALL_LOG:
                    ret[i] = Permission.WRITE_CALL_LOG;
                    break;

                case PROCESS_OUTGOING_CALLS:
                    ret[i] = Permission.PROCESS_OUTGOING_CALLS;
                    break;

                case BODY_SENSORS:
                    ret[i] = Permission.BODY_SENSORS;
                    break;

                case ACTIVITY_RECOGNITION:
                    ret[i] = Permission.ACTIVITY_RECOGNITION;
                    break;

                case SEND_SMS:
                    ret[i] = Permission.SEND_SMS;
                    break;

                case RECEIVE_SMS:
                    ret[i] = Permission.RECEIVE_SMS;
                    break;

                case READ_SMS:
                    ret[i] = Permission.READ_SMS;
                    break;

                case RECEIVE_WAP_PUSH:
                    ret[i] = Permission.RECEIVE_WAP_PUSH;
                    break;

                case RECEIVE_MMS:
                    ret[i] = Permission.RECEIVE_MMS;
                    break;

                case READ_EXTERNAL_STORAGE:
                    ret[i] = Permission.READ_EXTERNAL_STORAGE;
                    break;

                case WRITE_EXTERNAL_STORAGE:
                    ret[i] = Permission.WRITE_EXTERNAL_STORAGE;
                    break;
            }
        }

        return ret;
    }

    public static int[] permissions2Ints(List<String> src) {
        int length = src.size();
        int[] ret = new int[length];

        for (int i = 0; i < length; i++) {
            switch (src.get(i)) {
                case Permission.CAMERA:
                    ret[i] = CAMERA;
                    break;

                case Permission.RECORD_AUDIO:
                    ret[i] = RECORD_AUDIO;
                    break;

                case Permission.READ_CONTACTS:
                    ret[i] = READ_CONTACTS;
                    break;

                case Permission.WRITE_CONTACTS:
                    ret[i] = WRITE_CONTACTS;
                    break;

                case Permission.GET_ACCOUNTS:
                    ret[i] = GET_ACCOUNTS;
                    break;

                case Permission.READ_CALENDAR:
                    ret[i] = READ_CALENDAR;
                    break;

                case Permission.WRITE_CALENDAR:
                    ret[i] = WRITE_CALENDAR;
                    break;

                case Permission.ACCESS_FINE_LOCATION:
                    ret[i] = ACCESS_FINE_LOCATION;
                    break;

                case Permission.ACCESS_COARSE_LOCATION:
                    ret[i] = ACCESS_COARSE_LOCATION;
                    break;

                case Permission.ACCESS_BACKGROUND_LOCATION:
                    ret[i] = ACCESS_BACKGROUND_LOCATION;
                    break;

                case Permission.READ_PHONE_STATE:
                    ret[i] = READ_PHONE_STATE;
                    break;

                case Permission.CALL_PHONE:
                    ret[i] = CALL_PHONE;
                    break;

                case Permission.USE_SIP:
                    ret[i] = USE_SIP;
                    break;

                case Permission.READ_PHONE_NUMBERS:
                    ret[i] = READ_PHONE_NUMBERS;
                    break;

                case Permission.ANSWER_PHONE_CALLS:
                    ret[i] = ANSWER_PHONE_CALLS;
                    break;

                case Permission.ADD_VOICEMAIL:
                    ret[i] = ADD_VOICEMAIL;
                    break;

                case Permission.READ_CALL_LOG:
                    ret[i] = READ_CALL_LOG;
                    break;

                case Permission.WRITE_CALL_LOG:
                    ret[i] = WRITE_CALL_LOG;
                    break;

                case Permission.PROCESS_OUTGOING_CALLS:
                    ret[i] = PROCESS_OUTGOING_CALLS;
                    break;

                case Permission.BODY_SENSORS:
                    ret[i] = BODY_SENSORS;
                    break;

                case Permission.ACTIVITY_RECOGNITION:
                    ret[i] = ACTIVITY_RECOGNITION;
                    break;

                case Permission.SEND_SMS:
                    ret[i] = SEND_SMS;
                    break;

                case Permission.RECEIVE_SMS:
                    ret[i] = RECEIVE_SMS;
                    break;

                case Permission.READ_SMS:
                    ret[i] = READ_SMS;
                    break;

                case Permission.RECEIVE_WAP_PUSH:
                    ret[i] = RECEIVE_WAP_PUSH;
                    break;

                case Permission.RECEIVE_MMS:
                    ret[i] = RECEIVE_MMS;
                    break;

                case Permission.READ_EXTERNAL_STORAGE:
                    ret[i] = READ_EXTERNAL_STORAGE;
                    break;

                case Permission.WRITE_EXTERNAL_STORAGE:
                    ret[i] = WRITE_EXTERNAL_STORAGE;
                    break;
            }
        }

        return ret;
    }
}
