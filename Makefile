update-back-android:
	cp -rf Build/Android/unityLibrary/SnakatPermissions.androidlib/* Assets/Plugins/Android/SnakatPermissions.androidlib/
	rm -rf Assets/Plugins/Android/SnakatPermissions.androidlib/build

update-back-ios:
	cp Build/iOS/Libraries/Plugins/iOS/SnakatPermissions/* Assets/Plugins/iOS/SnakatPermissions/
