update-back-android:
	cp -rf Build/Android/unityLibrary/SnakatPermission.androidlib/* Assets/Plugins/Android/SnakatPermission.androidlib/
	rm -rf Assets/Plugins/Android/SnakatPermission.androidlib/build

update-back-ios:
	cp Build/iOS/Libraries/Plugins/iOS/SnakatPermission/* Assets/Plugins/iOS/SnakatPermission/
