using Android.Views;

namespace CollectionViewTest.Platforms.Android.Controls;

public class ScrollEventArg
{
    public float TotalX { get; }
    public float TotalY { get; }
    public MotionEventActions MotionEventActions { get; }

    public ScrollEventArg(MotionEventActions MotionEventActions)
    {
        TotalX = 0;
        TotalY = 0;
        this.MotionEventActions = MotionEventActions;
    }

    public ScrollEventArg(float TotalX, float TotalY, MotionEventActions MotionEventActions)
    {
        this.TotalX = TotalX;
        this.TotalY = TotalY;
        this.MotionEventActions = MotionEventActions;
    }
}
