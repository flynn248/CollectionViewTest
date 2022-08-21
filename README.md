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
If it is a scroll event, the listener will process that and send a new `ScrollEventArg` back to the view via an Interface.
The view can then handle the event accordingly.

## IOS Resolution
Although the issue wasn't tested on IOS, the problem would probably also happen.

A way to work around the issue could be to use a similar method as Android, or to add the following lines to the View for that page.
```xaml
xmlns:ios="clr-namespace:Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;assembly=Microsoft.Maui.Controls"
ios:Application.PanGestureRecognizerShouldRecognizeSimultaneously="True"
```

This should allow an added `PanGestureRecognizer` to be recognized and then allow gestures to still pass to the `CollectionView` for selection and scroll events.

**Once a test is conducted, this section will be updated to reflect the results**.

## Known Issue
Clicking in the `CollectionView` will send a new `ScrollEventArg` with a `MotionEventAction` of either `Down` or `Up`.
Sending these are useful to allow the View to handle when a scroll event is about to start or when it ends.
However, this has the potential to cause issues depending on the desired behavior since this will fire **no matter which event was originally triggered**.