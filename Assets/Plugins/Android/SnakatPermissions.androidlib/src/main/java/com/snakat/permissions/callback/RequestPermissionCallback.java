package com.snakat.permissions.callback;

public interface RequestPermissionCallback {
    void OnGranted ();
    void OnDenied (int[] permissions);
}
