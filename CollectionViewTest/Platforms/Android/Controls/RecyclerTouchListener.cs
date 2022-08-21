using Android.Content;
using Android.Views;
using CollectionViewTest.Platforms.Android.Interfaces;

namespace CollectionViewTest.Platforms.Android.Controls;

public class RecyclerTouchListener : RecyclerView.OnScrollListener, RecyclerView.IOnItemTouchListener
{
    GestureDetector gestureDetector;
    IScrollEvent scrollEvent;

    public RecyclerTouchListener(Context context, IScrollEvent scrollEvent)
    {
        this.scrollEvent = scrollEvent;
        gestureDetector = new GestureDetector(context, new ScrollGestureListener(scrollEvent));
    }

    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
    {
        if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up) //To pass a Down or Up event.
            scrollEvent?.OnScroll(new ScrollEventArg(e.Action));

        gestureDetector.OnTouchEvent(e);
        
        return false;
    }

    public void OnRequestDisallowInterceptTouchEvent(bool disallow)
    {
        throw new NotImplementedException();
    }

    public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
    {
        throw new NotImplementedException();
    }
}









/* Attempt at simplifying it and preventing the pan gesture from "starting" before it actually starts.


using Android.Content;
using Android.Views;
using Java.Interop;
using View = Android.Views.View;

namespace CollectionViewTest.Platforms.Android.Controls;

public interface IScrollEvent
{
    void OnScroll(ScrollEventArg e);
}

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

    public ScrollEventArg(float TotalX, float TotalY)
    {
        this.TotalX = TotalX;
        this.TotalY = TotalY;
    }
    public ScrollEventArg(float TotalX, float TotalY, MotionEventActions MotionEventActions)
    {
        this.TotalX = TotalX;
        this.TotalY = TotalY;
        this.MotionEventActions = MotionEventActions;
    }
    public ScrollEventArg(ScrollEventArg scrollEventArg, MotionEventActions MotionEventActions)
    {
        TotalX = scrollEventArg.TotalX;
        TotalY = scrollEventArg.TotalY;
        this.MotionEventActions = MotionEventActions;
    }
}

public partial class Simple : GestureDetector.SimpleOnGestureListener
{
    IScrollEvent scrollEvent = null;
    protected ScrollEventArg scrollEventArg = null;
    protected bool isScrolling = false;
    public Simple(IScrollEvent scrollEvent)
    {
        this.scrollEvent = scrollEvent;
    }
    public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
    {
        float totalX = (float)((e2.GetX() - e1.GetX()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        float totalY = (float)((e2.GetY() - e1.GetY()) / DeviceDisplay.Current.MainDisplayInfo.Density);
        //scrollEvent?.OnScroll(new ScrollEventArg(totalX, totalY, e2.Action));
        scrollEventArg = new ScrollEventArg(totalX, totalY, e2.Action);
        isScrolling = true;
        return true;
    }
    public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
    {
        return base.OnFling(e1, e2, velocityX, velocityY);
    }
}

public partial class RecyclerTouchListener : Simple, RecyclerView.IOnItemTouchListener
{
    GestureDetector gestureDetector;
    IScrollEvent scrollEvent;
    public RecyclerTouchListener(Context context, IScrollEvent scrollEvent) : base(scrollEvent)
    {
        this.scrollEvent = scrollEvent;
        gestureDetector = new GestureDetector(context, this);
    }

    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
    {
        View child = rv.FindChildViewUnder(e.GetX(), e.GetY());
        //System.Diagnostics.Debug.WriteLine($"e Action: {e?.Action} :: e ActionMasked: {e.ActionMasked} :: e ActionIndex: {e.ActionIndex} :: e2 ActionButton {e.ActionButton}");
        ///* The Up action can be grabbed here.
        // * An interface to share the value won't work partly because the drag event does not fire when the action is up.
        // * A way to get around this would be to add another Messenger.
        // *  To prevent it from sending too many, can add some logic to not post if the action is the same as before.
        // * 
        // * If this were to be shared with other views in the future, a unique post node would be best to avoid crossing streams.
         
        

        
        if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up)
            scrollEvent?.OnScroll(new ScrollEventArg(e.Action));

        if (!gestureDetector.OnTouchEvent(e))
        {
            return false;
        }

        if (isScrolling)
{
    switch (e.Action)
    {
        case MotionEventActions.Down:
            scrollEvent?.OnScroll(new ScrollEventArg(MotionEventActions.Down));
            scrollEvent?.OnScroll(scrollEventArg);
            break;
        case MotionEventActions.Move:
            scrollEvent?.OnScroll(scrollEventArg);
            break;
        case MotionEventActions.Up:
            scrollEvent?.OnScroll(new ScrollEventArg(MotionEventActions.Up));
            isScrolling = false;
            break;
    }
}

//if (child is not null && clickListener is not null && gestureDetector.OnTouchEvent(e))
//{
//    clickListener.OnClick(child, rv.GetChildAdapterPosition(child));
//}
//else

return false;
    }
    public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
{
    return base.OnScroll(e1, e2, distanceX, distanceY);
}
public void OnRequestDisallowInterceptTouchEvent(bool disallow)
{
    throw new NotImplementedException();
}

public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
{
    throw new NotImplementedException();
}
}
 */




/*Simple method that works
 using Android.Content;
using Android.Views;
using Java.Interop;
using View = Android.Views.View;

namespace CollectionViewTest.Platforms.Android.Controls;

public interface IScrollEvent
{
    void OnScroll(ScrollEventArg e);
}

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

public class Simple : GestureDetector.SimpleOnGestureListener
{
    RecyclerView recyclerView = null;
    IScrollEvent scrollEvent = null;

    public Simple(RecyclerView recyclerView)
    {
        this.recyclerView = recyclerView;
    }
    public Simple(RecyclerView recyclerView, IScrollEvent scrollEvent)
    {
        this.recyclerView = recyclerView;
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

public class RecyclerTouchListener : RecyclerView.OnScrollListener, RecyclerView.IOnItemTouchListener 
{
    GestureDetector gestureDetector;
    IScrollEvent scrollEvent;
    public RecyclerTouchListener(Context context, RecyclerView recyclerView)
    {
        scrollEvent = null;
        gestureDetector = new GestureDetector(context, new Simple(recyclerView));
    }
    public RecyclerTouchListener(Context context, RecyclerView recyclerView, IScrollEvent scrollEvent)
    {
        this.scrollEvent = scrollEvent;
        gestureDetector = new GestureDetector(context, new Simple(recyclerView, scrollEvent));
    }

    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
    {
        View child = rv.FindChildViewUnder(e.GetX(), e.GetY());
        //System.Diagnostics.Debug.WriteLine($"e Action: {e?.Action} :: e ActionMasked: {e.ActionMasked} :: e ActionIndex: {e.ActionIndex} :: e2 ActionButton {e.ActionButton}");
        ///* The Up action can be grabbed here.
        // * An interface to share the value won't work partly because the drag event does not fire when the action is up.
        // * A way to get around this would be to add another Messenger.
        // *  To prevent it from sending too many, can add some logic to not post if the action is the same as before.
        // * 
        // * If this were to be shared with other views in the future, a unique post node would be best to avoid crossing streams.
         
        


        if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up)
            scrollEvent?.OnScroll(new ScrollEventArg(e.Action));

        if (gestureDetector.OnTouchEvent(e))
        {
            System.Diagnostics.Debug.WriteLine("Touch event detected");
        }

        //if (child is not null && clickListener is not null && gestureDetector.OnTouchEvent(e))
        //{
        //    clickListener.OnClick(child, rv.GetChildAdapterPosition(child));
        //}
        //else

        return false;
    }

    public void OnRequestDisallowInterceptTouchEvent(bool disallow)
{
    throw new NotImplementedException();
}

public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
{
    throw new NotImplementedException();
}
}


 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */