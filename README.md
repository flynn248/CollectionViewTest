# CollectionViewTest
## Original Issue
Adding a `PanGestureRecognizer` or a `SwipeGestureRecognizer` to a `CollectionView` would disable the selection of elements inside of the `CollectionView`.

Adding a `TapGestureRecognizer` did not resolve the issue since there wasn't a way to recognize which item was selected in the `CollectionView`.

It seems like the added `GestureRecognizer` overrides the existing gestures that already exist on the `CollectionView`.

## Affected Platforms
### Tested
* Android
### Not Tested
* All Other Platforms

## Android Resolution

The code in the example demonstrates a method to recognize a pan gesture without disabling the existing gesture in a `CollectionView`.

An event listener is added to a specific `CollectionView` where the pan gesture is desired.
When a touch event happens, the listener will determine what kind of event it is and then process it accordingly.



## IOS Resolution
Although the issue wasn't tested on IOS, the problem would probably also happen.

A way to work around the issue could be to use a similar method as Android, or to add the following lines to the View for that page.
```xaml
xmlns:ios="clr-namespace:Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;assembly=Microsoft.Maui.Controls"
ios:Application.PanGestureRecognizerShouldRecognizeSimultaneously="True"
```

This should allow an added `PanGestureRecognizer` to be recognized and then allow gestures to still pass to the `CollectionView` for selection and scroll events.

**Once an update is conducted, this section will be updated to reflect the results.**

