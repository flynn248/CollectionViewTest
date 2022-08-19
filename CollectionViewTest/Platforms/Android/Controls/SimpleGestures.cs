using Android.Content;
using Android.Views;
using Java.Interop;
using View = Android.Views.View;

namespace CollectionViewTest.Platforms.Android.Controls;

public interface IScrollEvent
{
    //float TotalX { get; set; }
    //float TotalY { get; set; }
    //MotionEventActions MotionEventActions { get; set; }
    void OnScroll(ScrollEventsArg e);
}

public interface IClickListener
{
    void OnClick(View view, int position);
    void OnLongClick(View view, int position);
}

public class ScrollEventsArg
{
    public float TotalX { get; }
    public float TotalY { get; }
    public MotionEventActions MotionEventActions { get; }

    public ScrollEventsArg(MotionEventActions MotionEventActions)
    {
        TotalX = 0;
        TotalY = 0;
        this.MotionEventActions = MotionEventActions;
    }

    public ScrollEventsArg(float TotalX, float TotalY, MotionEventActions MotionEventActions)
    {
        this.TotalX = TotalX;
        this.TotalY = TotalY;
        this.MotionEventActions = MotionEventActions;
    }
}

public class ScrollEvent : IScrollEvent
{
    public void OnScroll(ScrollEventsArg e)
    {
        System.Diagnostics.Debug.WriteLine(e.MotionEventActions);
    }

    //public float TotalX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //public float TotalY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //public MotionEventActions MotionEventActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}


public class Simple : GestureDetector.SimpleOnGestureListener
{
    RecyclerView recyclerView = null;
    IClickListener clickListener = null;
    IScrollEvent scrollEvent = null;

    private bool onDown = false;
    public Simple(RecyclerView recyclerView)
    {
        this.recyclerView = recyclerView;
        this.clickListener = null;
    }
    public Simple(RecyclerView recyclerView, IScrollEvent scrollEvent)
    {
        this.recyclerView = recyclerView;
        this.scrollEvent = scrollEvent;
    }
    
    public Simple(RecyclerView recyclerView, IClickListener clickListener)
    {
        this.recyclerView = recyclerView;
        this.clickListener = clickListener;
    }
    public override bool OnSingleTapUp(MotionEvent e)
    {
        onDown = false;
        return true;
    }
    public override void OnLongPress(MotionEvent e)
    {
        View child = recyclerView.FindChildViewUnder(e.GetX(), e.GetY());
        if (child is not null && clickListener is not null)
        {
            clickListener.OnLongClick(child, recyclerView.GetChildAdapterPosition(child));
        }

    }


    public override void OnShowPress(MotionEvent e)
    {
        base.OnShowPress(e);
    }

    public override bool OnSingleTapConfirmed(MotionEvent e)
    {
        return base.OnSingleTapConfirmed(e);
    }

    public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
    {
        if (e2?.Action == MotionEventActions.Down)
        {
            System.Diagnostics.Debug.WriteLine("Down");
        }
        else if (e2?.Action == MotionEventActions.Up)
        {
            System.Diagnostics.Debug.WriteLine("Up");

        }
        //System.Diagnostics.Debug.WriteLine($"e2 Action: {e2?.Action} :: e2 ActionMasked: {e2.ActionMasked} :: e2 ActionIndex: {e2.ActionIndex} :: e2 ActionButton {e2.ActionButton}");
        //System.Diagnostics.Debug.WriteLine($"e1 Action: {e1?.Action} :: e1 ActionMasked: {e1.ActionMasked} :: e1 ActionIndex: {e1.ActionIndex} :: e1 ActionButton {e1.ActionButton}");


        //System.Diagnostics.Debug.WriteLine($"e1 ButtonState: {e1?.ButtonState} :: e2 ButtonState {e2.ButtonState}");
        float totalX = (float)((e2.GetX() - e1.GetX()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        float totalY = (float)((e2.GetY() - e1.GetY()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        scrollEvent?.OnScroll(new ScrollEventsArg(totalX, totalY, e2.Action));

        MessagingCenter.Send<Simple, float[]>(this, "ScrollEvent", new float[] { (float)((e2.GetX() - e1.GetX()) / DeviceDisplay.Current.MainDisplayInfo.Density), distanceX }); //Divide by pixel density.
                                                                                                                                                                                 //System.Diagnostics.Debug.WriteLine($"{e1.GetX():f2} {e2.GetX():f2} : : {(e2.GetX() - e1.GetX())/2.7:f2} : : {e2.XPrecision:f2} :: {e1.XPrecision:f2}");
                                                                                                                                                                                 //RectF rect = new RectF();


        return base.OnScroll(e1, e2, distanceX, distanceY);
    }
    public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
    {
        return base.OnFling(e1, e2, velocityX, velocityY);
    }

    public override JniPeerMembers JniPeerMembers => base.JniPeerMembers;
    public override bool OnContextClick(MotionEvent e)
    {
        return base.OnContextClick(e);
    }

    public override bool OnDoubleTap(MotionEvent e)
    {
        return base.OnDoubleTap(e);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool OnDoubleTapEvent(MotionEvent e)
    {
        return base.OnDoubleTapEvent(e);
    }

    public override bool OnDown(MotionEvent e)
    {
        return base.OnDown(e);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}
public partial class SimpleGesture : GestureDetector
{
    public SimpleGesture(Context context, IOnGestureListener listener) : base(context, listener)
    {
    }

    public override bool OnTouchEvent(MotionEvent ev)
    {
        return base.OnTouchEvent(ev);
    }

}
public class RecyclerTouchListener : Java.Lang.Object, RecyclerView.IOnItemTouchListener
{
    GestureDetector gestureDetector;
    IClickListener clickListener;
    IScrollEvent scrollEvent;
    public RecyclerTouchListener(Context context, RecyclerView recyclerView)
    {
        clickListener = null;
        scrollEvent = null;
        gestureDetector = new GestureDetector(context, new Simple(recyclerView));
    }
    public RecyclerTouchListener(Context context, RecyclerView recyclerView, IScrollEvent scrollEvent)
    {
        clickListener = null;
        this.scrollEvent = scrollEvent;
        gestureDetector = new GestureDetector(context, new Simple(recyclerView, scrollEvent));
    }
    public RecyclerTouchListener(Context context, RecyclerView recyclerView, IClickListener clickListener)
    {
        this.clickListener = clickListener;
        scrollEvent = null;
        gestureDetector = new GestureDetector(context, new Simple(recyclerView, clickListener));

    }




    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
    {
        View child = rv.FindChildViewUnder(e.GetX(), e.GetY());
        //System.Diagnostics.Debug.WriteLine($"e Action: {e?.Action} :: e ActionMasked: {e.ActionMasked} :: e ActionIndex: {e.ActionIndex} :: e2 ActionButton {e.ActionButton}");
        /* The Up action can be grabbed here.
         * An interface to share the value won't work partly because the drag event does not fire when the action is up.
         * A way to get around this would be to add another Messenger.
         *  To prevent it from sending too many, can add some logic to not post if the action is the same as before.
         * 
         * If this were to be shared with other views in the future, a unique post node would be best to avoid crossing streams.
         
        */


        if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up)
            scrollEvent?.OnScroll(new ScrollEventsArg(e.Action));


        if (child is not null && clickListener is not null && gestureDetector.OnTouchEvent(e))
        {
            clickListener.OnClick(child, rv.GetChildAdapterPosition(child));
        }
        else
            gestureDetector.OnTouchEvent(e);

        return false;
    }

    public void OnRequestDisallowInterceptTouchEvent(bool disallow)
    {
        System.Diagnostics.Debug.WriteLine("OnTouchEvent Fired!");

    }

    public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
    {

        System.Diagnostics.Debug.WriteLine("OnTouchEvent Fired!");

    }


}
