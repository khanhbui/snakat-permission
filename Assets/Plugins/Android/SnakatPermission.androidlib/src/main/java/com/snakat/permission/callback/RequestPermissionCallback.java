package com.snakat.permission.callback;

public interface RequestPermissionCallback {
    void OnGranted ();
    void OnDenied (int[] permissions);
}
