using Android.Content;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using CollectionViewTest.Platforms.Android.Interfaces;

namespace CollectionViewTest.Platforms.Android.Controls;

public class RecyclerTouchListener : Java.Lang.Object, RecyclerView.IOnItemTouchListener
{
    readonly GestureDetector gestureDetector;
    readonly IScrollEvent scrollEvent;

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