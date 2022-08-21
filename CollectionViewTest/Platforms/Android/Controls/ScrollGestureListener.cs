using Android.Views;
using CollectionViewTest.Platforms.Android.Interfaces;

namespace CollectionViewTest.Platforms.Android.Controls;
public class ScrollGestureListener : GestureDetector.SimpleOnGestureListener
{
    IScrollEvent scrollEvent = null;

    public ScrollGestureListener(IScrollEvent scrollEvent)
    {
        this.scrollEvent = scrollEvent;
    }
    public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
    {
        float totalX = (float)((e2.GetX() - e1.GetX()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        float totalY = (float)((e2.GetY() - e1.GetY()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        scrollEvent?.OnScroll(new ScrollEventArg(totalX, totalY, e2.Action));

        return base.OnScroll(e1, e2, distanceX, distanceY);
    }
    public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
    {
        return base.OnFling(e1, e2, velocityX, velocityY);
    }
}
