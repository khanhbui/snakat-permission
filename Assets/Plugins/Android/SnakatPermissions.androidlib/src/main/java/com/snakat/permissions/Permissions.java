package com.snakat.permissions;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;

import androidx.annotation.Nullable;

import com.snakat.permissions.callback.OpenSettingCallback;
import com.snakat.permissions.callback.RequestNotificationCallback;
import com.snakat.permissions.callback.RequestPermissionCallback;
import com.snakat.permissions.callback.ScreenCaptureCallback;
import com.snakat.permissions.callback.WriteSettingCallback;
import com.yanzhenjie.permission.Action;
import com.yanzhenjie.permission.AndPermission;

import java.util.List;

public class Permissions {

    private static final String TAG = Permissions.class.getSimpleName();

    private static OpenSettingCallback mOpenSettingCallback;

    public static boolean HasPermissions(Activity activity, int[] permissions) {
        return AndPermission.hasPermissions(activity, Util.ints2Permissions(permissions));
    }

    public static boolean HasAlwaysDeniedPermission(Activity activity, int[] permissions) {
        return AndPermission.hasAlwaysDeniedPermission(activity, Util.ints2Permissions(permissions));
    }

    @SuppressLint("WrongConstant")
    public static void RequestPermissions(Activity activity, int[] permissions, RequestPermissionCallback callback) {
        AndPermission.with(activity)
                .runtime()
                .permission(Util.ints2Permissions(permissions))
                .onGranted(new Action<List<String>>() {
                    @Override
                    public void onAction(List<String> permissions) {
                        callback.OnGranted();
                    }
                })
                .onDenied(new Action<List<String>>() {
                    @Override
                    public void onAction(List<String> permissions) {
                        callback.OnDenied(Util.permissions2Ints(permissions));
                    }
                })
                .start();
    }

    public static void OpenSetting(Activity activity, OpenSettingCallback callback) {
        if (mOpenSettingCallback != null) {
            return;
        }
        mOpenSettingCallback = callback;

        AndPermission.with(activity)
                .runtime()
                .setting()
                .start(1000);
    }

    public static void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        if (requestCode != 1000) {
            return;
        }
        if (mOpenSettingCallback == null) {
            return;
        }

        mOpenSettingCallback.OnClosed();
        mOpenSettingCallback = null;
    }

    public static void RequestNotification(Activity activity, RequestNotificationCallback callback) {
        AndPermission.with(activity)
                .notification()
                .permission()
                .onGranted(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnGranted();
                    }
                })
                .onDenied(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnDenied();
                    }
                })
                .start();
    }

    public static void RequestNotificationListener(Activity activity, RequestNotificationCallback callback) {
        AndPermission.with(activity)
                .notification()
                .listener()
                .onGranted(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnGranted();
                    }
                })
                .onDenied(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnDenied();
                    }
                })
                .start();
    }

    public static void RequestWriteSystemSetting(Activity activity, WriteSettingCallback callback) {
        AndPermission.with(activity)
                .setting()
                .write()
                .onGranted(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnGranted();
                    }
                })
                .onDenied(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnDenied();
                    }
                })
                .start();
    }

    public static void RequestScreenCapture(Activity activity, ScreenCaptureCallback callback) {
        AndPermission.with(activity)
                .media()
                .capture()
                .onGranted(new Action<Intent>() {
                    @Override
                    public void onAction(Intent intent) {
                        callback.OnGranted(intent);
                    }
                })
                .onDenied(new Action<Void>() {
                    @Override
                    public void onAction(Void data) {
                        callback.OnDenied();
                    }
                })
                .start();
    }
}
