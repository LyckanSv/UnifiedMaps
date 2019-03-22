# Change Log
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [2.0.2] - 2019-03-22
## Updated
- Android: Map inside ScrollView shouldn't move the scrollView #98

## [2.0.1] - 2018-10-01
## Updated
- iOS 12 MapPinClicked and MapPinLongClicked Events are not fired #93
- iOS PinInfoViewClicked Event not fired #91


## [2.0.0] - 2018-09-14
## Added
- Drag Pins and Long Click Events #83
- Move to region with circle boundary #85
- Changed naming of `PinCallout` Events and Methods to `PinCalloutView`. Old events are marked as Obsolete and will be removed in the future.

## Changed
- Migrate to netstandard2.0 #86

## [1.12.0] - 2018-07-20
## Added
- Clearing binded ObservableCollections with markers now remove all Markers from the Map.

## Updated
- Changed dependencies in nuspec, the NuGet now requires Xamarin.Forms `3.0.0.561731` and Xamarin.GooglePlayServices.Maps `60.1142.1`
- Fix GitHub URL in nuspec

## [1.11.0] - 2018-02-20
## Added
- Added new Property `ExcludeUserLocationFromFitAllAnnotations` to disable automatic inclusion of user location in fitting visible region to map annotations on iOS

## [1.10.0] - 2018-02-20
## Updated
- Changed dependencies in nuspec, the NuGet now requires Xamarin.Forms `2.4.18342` and Xamarin.GooglePlayServices.Maps `42.1021.1`

## [1.9.1] - 2018-02-19
## Fixed
- Fixes initial visible region not correctly set on GoogleMaps on Android

## [1.9.0] - 2018-01-16
## Updated
- Updated GooglePlayServices Dependencies on Android Projects

## [1.8.1] - 2017-12-24
## Fixed
- Fixes MoveToUserLocation disabled user annotation, set animated camera to default

## [1.8.0] - 2017-12-18
## Added
- `IosSingleAnnotationZoom` added to set the visible size of the map if only one annotation is visible on iOS

## [1.7.4] - 2017-12-18
## Fixed
- Fixes view region off center if only one annotation is set on iOS

## [1.7.3] - 2017-12-13
## Fixed
- Fixes view region off center if only one annotation is set on iOS

## [1.7.2] - 2017-11-24
## Fixed
- Map controls interfering with each other with MoveToRegion is invoked

## [1.7.1] - 2017-11-17
## Fixed
- Fix GoogleMaps on Android not considering Zoom Level Property

## [1.7] - 2017-11-16
## Framework updates:
- Xamarin Forms 2.3.4.224 -> 2.4.0.18342

## Added
- Add property to enable/disable camera animation
- Move to users current location method

## [1.6] - 2017-10-04
## Framework updates:
- Google services 29.0.0.2 -> 32.961.0
- Xamarin Forms 2.3.2.127 -> 2.3.4.224
- Android support packages 23.3.0 -> 24.2.1
## Added
Z-index:
- User location icon will be at the top most (this align iOS and Google maps' behavior)
- Map pin now has a (non-bindable) ZIndex property (of integer type, default to 0)

## [1.5.1] - 2017-10-03
## Fixed
 - #39 Fix IllegalArgumentException on Android sometimes when a pin is deselected

## [1.5] - 2017-07-17
## Added
 - #36 Ability to set initial Zoom Level for Google Maps on Android

## [1.4.5] - 2017-06-29
## Fixes
 - #34 Fix null reference exception when getting map region for all annotations, thx @renfred

## [1.4.4] - 2017-06-28
## Fixes
 - #33 Fix issue where Overlays were not drawn during init, thx @renfred

## [1.4.3] - 2017-06-06
## Fixes
 - #32 (IntPtr, JniHandleOwnership) constructor to avoid leaky abstraction, thx @shmoogems

## [1.4.2] - 2017-05-15
### Added
 - #30 Enhance map with ability to choose whether to show callout on tap or not, thx @renfred

## [1.4.1] - 2017-04-25
### Fixes
 - #29 Fix an issue where native controls are still shown when disabled. thx @renfred

## [1.4.0] - 2017-04-19
### Fixes
 - #26 iOS Remove Overlay does not function. thx @Steve-Himself

### Added
 - Add conditional flags to enable/disable touch map to dismiss and show/hide native zoom and location buttons. Thx @renfred

## [1.3.9] - 2017-04-18
### Fixes
 - Fix item deselection issue and inconsistency between two versions #25, thx @renfred

## [1.3.8] - 2017-03-22
### Fixes
- #23 Android maps not showing user location, thx @shmoogems

## [1.3.7] - 2017-03-06
### Fixes
- Fixes NullReferenceException when the selected item is null

## [1.3.6] - 2017-02-27
### Fixes
- #21 Android MapRegion incorrectly constructed in OnCameraChange

## [1.3.5] - 2017-02-22
### Fixes
- #18 Android does not update VisibleRegion when map position changed
- #20 Update CI to automatically build nuget package with correct version

## [1.3.3] - 2017-02-05
### Fixes
- #16 - Fix for the android ShowInfoWindow glitch when marker clicked
- #14 - iOS MoveToRegion bug fix calculating the counterpoint of the MapRegion
- fix possible NullReference Exception if SelectedItem is null and PropertyChanged Event is handled.

## [1.3.2] - 2017-01-17
### Fixes
- Fix for Android OnInfoWindowClick Error

## [1.3.1] - 2016-11-22
### Added
- Linker support: UnifiedMaps is now linker safe
- Selection test to sample app

## [1.3.0] - 2016-11-06
### Added
- support for pin images and selection
- circle map overlay
- bindable visible-region property
