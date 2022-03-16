package com.snakat.permission.callback;

import android.content.Intent;

public interface ScreenCaptureCallback {
    void OnGranted (Intent intent);
    void OnDenied ();
}
