//
//  Permissions.swift
//  SnakatPermission
//
//  Created by Khanh Hoang Bui on 3/3/22.
//

import Foundation
#if PERMISSION_BLUETOOTH || PERMISSION_CAMERA || PERMISSION_MICROPHONE || PERMISSION_CONTACTS || PERMISSION_EVENTS || PERMISSION_REMINDERS || PERMISSION_LOCATION || PERMISSION_MOTION || PERMISSION_NOTIFICATIONS || PERMISSION_PHOTOS || PERMISSION_MEDIA_LIBRARY || PERMISSION_SPEECH_RECOGNIZER || PERMISSION_SIRI || PERMISSION_APP_TRACKING_TRANSPARENCY
import Permission
#endif

@objc public class SnakatPermissions: NSObject {

    @objc public static let shared = SnakatPermissions()

    // MARK: Request for Permissions

    private enum SPPermissionType: Int32 {
        case bluetooth = 101
        
        case camera = 201
        
        case microphone = 301
        
        case contacts = 401
        
        case events = 501
        
        case reminders = 601

        case locationAlways = 701
        case locationWhenInUse = 702
        
        case motion = 801

        case photos = 901

        case mediaLibrary = 1001

        case speechRecognizer = 1101

        case siri = 1201

        case appTrackingTransparency = 1301
    }

    @objc public func hasPermission(_ permissionType: Int32) -> Bool {
#if PERMISSION_BLUETOOTH || PERMISSION_CAMERA || PERMISSION_MICROPHONE || PERMISSION_CONTACTS || PERMISSION_EVENTS || PERMISSION_REMINDERS || PERMISSION_LOCATION || PERMISSION_MOTION || PERMISSION_PHOTOS || PERMISSION_MEDIA_LIBRARY || PERMISSION_SPEECH_RECOGNIZER || PERMISSION_SIRI || PERMISSION_APP_TRACKING_TRANSPARENCY
        let permission = permission(from: permissionType)
        return permission.status == .authorized
#else
        return false
#endif
    }

    @objc public func requestPermission(_ permissionType: Int32, callback: @escaping ()->Void) -> Void {
#if PERMISSION_BLUETOOTH || PERMISSION_CAMERA || PERMISSION_MICROPHONE || PERMISSION_CONTACTS || PERMISSION_EVENTS || PERMISSION_REMINDERS || PERMISSION_LOCATION || PERMISSION_MOTION || PERMISSION_PHOTOS || PERMISSION_MEDIA_LIBRARY || PERMISSION_SPEECH_RECOGNIZER || PERMISSION_SIRI || PERMISSION_APP_TRACKING_TRANSPARENCY
        let permission = permission(from: permissionType)
        permission.request({ _ in
            callback()
        })
#else
        callback(false)
#endif
    }

#if PERMISSION_BLUETOOTH || PERMISSION_CAMERA || PERMISSION_MICROPHONE || PERMISSION_CONTACTS || PERMISSION_EVENTS || PERMISSION_REMINDERS || PERMISSION_LOCATION || PERMISSION_MOTION || PERMISSION_PHOTOS || PERMISSION_MEDIA_LIBRARY || PERMISSION_SPEECH_RECOGNIZER || PERMISSION_SIRI || PERMISSION_APP_TRACKING_TRANSPARENCY
    private func permission(from id: Int32) -> Permission {
        guard let type = SPPermissionType(rawValue: id) else {
            fatalError("Not found permission \(id).")
        }
        
        switch type {
        case .bluetooth:
#if PERMISSION_BLUETOOTH
            return .bluetooth
#else
            fatalError("Add PERMISSION_BLUETOOTH to PERMISSION_FLAGS.")
#endif
            
        case .camera:
#if PERMISSION_CAMERA
            return .camera
#else
            fatalError("Add PERMISSION_CAMERA to PERMISSION_FLAGS.")
#endif

        case .microphone:
#if PERMISSION_MICROPHONE
            return .microphone
#else
            fatalError("Add PERMISSION_MICROPHONE to PERMISSION_FLAGS.")
#endif

        case .contacts:
#if PERMISSION_CONTACTS
            return .contacts
#else
            fatalError("Add PERMISSION_CONTACTS to PERMISSION_FLAGS.")
#endif

        case .events:
#if PERMISSION_EVENTS
            return .events
#else
            fatalError("Add PERMISSION_EVENTS to PERMISSION_FLAGS.")
#endif

        case .reminders:
#if PERMISSION_REMINDERS
            return .reminders
#else
            fatalError("Add PERMISSION_REMINDERS to PERMISSION_FLAGS.")
#endif

        case .locationAlways:
#if PERMISSION_LOCATION
            return .locationAlways
#else
            fatalError("Add PERMISSION_LOCATION to PERMISSION_FLAGS.")
#endif

        case .locationWhenInUse:
#if PERMISSION_LOCATION
            return .locationWhenInUse
#else
            fatalError("Add PERMISSION_LOCATION to PERMISSION_FLAGS.")
#endif

        case .motion:
#if PERMISSION_MOTION
            return .motion
#else
            fatalError("Add PERMISSION_MOTION to PERMISSION_FLAGS.")
#endif

        case .photos:
#if PERMISSION_PHOTOS
            return .photos
#else
            fatalError("Add PERMISSION_PHOTOS to PERMISSION_FLAGS.")
#endif

        case .mediaLibrary:
#if PERMISSION_MEDIA_LIBRARY
            return .mediaLibrary
#else
            fatalError("Add PERMISSION_MEDIA_LIBRARY to PERMISSION_FLAGS.")
#endif

        case .speechRecognizer:
#if PERMISSION_SPEECH_RECOGNIZER
            return .speechRecognizer
#else
            fatalError("Add PERMISSION_SPEECH_RECOGNIZER to PERMISSION_FLAGS.")
#endif

        case .siri:
#if PERMISSION_SIRI
            return .siri
#else
            fatalError("Add PERMISSION_SIRI to PERMISSION_FLAGS.")
#endif

        case .appTrackingTransparency:
#if PERMISSION_APP_TRACKING_TRANSPARENCY
            return .appTrackingTransparency
#else
            fatalError("Add PERMISSION_APP_TRACKING_TRANSPARENCY to PERMISSION_FLAGS.")
#endif
        }
    }
#endif

    
    // MARK: Open Setting

    public typealias OpenSettingCallack = ()->Void
    private var openSettingCallback: OpenSettingCallack?
    
    @objc public func openSetting(callback: @escaping OpenSettingCallack) -> Void {
        if (openSettingCallback != nil) {
            return;
        }
        openSettingCallback = callback

        NotificationCenter.default.addObserver(self, selector: #selector(didBecomeActiveHandler), name: UIApplication.didBecomeActiveNotification, object: nil)

        if let URL = URL(string: UIApplication.openSettingsURLString) {
            UIApplication.shared.open(URL)
        }
    }
    
    @objc private func didBecomeActiveHandler() {
        NotificationCenter.default.removeObserver(self, name: UIApplication.didBecomeActiveNotification, object: nil)

        openSettingCallback?()
        openSettingCallback = nil
    }
    
    
    // MARK: Notification
    
    @objc public func requestNotification(callback: @escaping (Bool)->Void) -> Void {
#if PERMISSION_NOTIFICATIONS
        let permission = Permission.notifications
        permission.request({ status in
            callback(status == .authorized)
        })
#else
        fatalError("Add PERMISSION_NOTIFICATIONS to PERMISSION_FLAGS.")
#endif
    }
}
