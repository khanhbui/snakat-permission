//
//  SmakatPermissions.m
//  SnakatPermission
//
//  Created by Khanh Hoang Bui on 3/3/22.
//

#import <Foundation/Foundation.h>
#import "UnityFramework/UnityFramework-Swift.h"

extern "C" {

#pragma mark Check for Permissions

bool HasPermissions(const int32_t* permissions, const int32_t length) {
    SnakatPermissions *snakatPermissions = [SnakatPermissions shared];

    for (int32_t i = 0; i < length; i++) {
        if ([snakatPermissions hasPermission:permissions[i]] == NO) {
            return false;
        }
    }
    return true;
}


#pragma mark Request for Permissions

typedef void (*GrantedPermissionCallback)(const int32_t);
typedef void (*DeniedPermissionCallback)(const int32_t, const void*, const int32_t);

typedef void (^Callback)();
void _RequestPermissions(const int32_t*, const int32_t, const int32_t, Callback);

void RequestPermissions(const int32_t* permissions, const int32_t length, const int requestId, GrantedPermissionCallback onGranted, DeniedPermissionCallback onDenied) {
    _RequestPermissions(permissions, length, 0, ^(){
        SnakatPermissions *snakatPermissions = [SnakatPermissions shared];

        int32_t deniedCount = 0;
        for (int32_t i = 0; i < length; i++) {
            if ([snakatPermissions hasPermission:permissions[i]] == NO) {
                deniedCount++;
            }
        }
        
        if (deniedCount == 0) {
            onGranted(requestId);
        }
        else {
            int deniedPermissions[deniedCount];
            deniedCount = 0;
            for (int32_t i = 0; i < length; i++) {
                if ([snakatPermissions hasPermission:permissions[i]] == NO) {
                    deniedPermissions[deniedCount] = permissions[i];
                    deniedCount++;
                }
            }
            onDenied(requestId, deniedPermissions, deniedCount);
        }
    });
}

void _RequestPermissions(const int32_t* permissions, const int32_t length, const int32_t index, Callback onDone) {
    if (index >= length) {
        onDone();
        return;
    }

    int32_t permission = permissions[index];
    SnakatPermissions *snakatPermissions = [SnakatPermissions shared];
    [snakatPermissions requestPermission:permission callback:^{
        _RequestPermissions(permissions, length, index + 1, onDone);
    }];
}


#pragma mark Open setting

typedef void (*OpenSettingCallback)();

void OpenSetting(OpenSettingCallback onClose) {
    SnakatPermissions *snakatPermissions = [SnakatPermissions shared];
    [snakatPermissions openSettingWithCallback:^{
        onClose();
    }];
}


#pragma mark Notification

typedef void (*NotificationCallback)(const bool granted);

void RequestNotification(NotificationCallback callback) {
    SnakatPermissions *snakatPermissions = [SnakatPermissions shared];
    [snakatPermissions requestNotificationWithCallback:^(BOOL granted) {
        callback(granted == YES);
    }];
}


}
