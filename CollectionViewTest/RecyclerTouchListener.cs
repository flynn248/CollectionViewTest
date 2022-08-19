#if ANDROID

using Android.Views;
using Java.Interop;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Android.Content.Context;
using AndroidX.RecyclerView.Widget;
using Android.Content;
using static Android.Views.View;

namespace CollectionViewTest;
public interface IClickListener
{
    void OnClick(Android.Views.View view, int position);
    void OnLongClick(Android.Views.View view, int position);
}
public class SimpleOld : GestureDetector.SimpleOnGestureListener
{
    private RecyclerView recyclerView;
    private IClickListener clickListener;
    private bool onDown = false;
    public SimpleOld(RecyclerView recyclerView, IClickListener clickListener)
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
        Android.Views.View child = recyclerView.FindChildViewUnder(e.GetX(), e.GetY());
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
        if(e2?.Action == MotionEventActions.Down)
        {
            System.Diagnostics.Debug.WriteLine("Down");
        }
        else if (e2?.Action == MotionEventActions.Up)
        {
            System.Diagnostics.Debug.WriteLine("Up");

        }

        MessagingCenter.Send<SimpleOld, float[]>(this, "ScrollEvent", new float[] { (float)((e2.GetX() - e1.GetX()) / DeviceDisplay.Current.MainDisplayInfo.Density), distanceX }); //Divide by pixel density.
        //System.Diagnostics.Debug.WriteLine($"{e1.GetX():f2} {e2.GetX():f2} : : {(e2.GetX() - e1.GetX())/2.7:f2} : : {e2.XPrecision:f2} :: {e1.XPrecision:f2}");
        //RectF rect = new RectF();

        //float viewportOffsetX = distanceX * 
        
        return base.OnScroll(e1, e2, distanceX, distanceY);
        //return true;
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
        onDown = true;
        return base.OnDown(e);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}

public class RecyclerTouchListenerOld : Java.Lang.Object, RecyclerView.IOnItemTouchListener
{
    private GestureDetector gestureDetector;
    private IClickListener clickListener;

    public RecyclerTouchListenerOld(Context context, RecyclerView recyclerView, IClickListener clickListener)
    {
        this.clickListener = clickListener;
        gestureDetector = new GestureDetector(context, new SimpleOld(recyclerView, clickListener)) ;

    }




    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
    {
        Android.Views.View child = rv.FindChildViewUnder(e.GetX(), e.GetY());

        if(child is not null && clickListener is not null && gestureDetector.OnTouchEvent(e))
        {
            clickListener.OnClick(child, rv.GetChildAdapterPosition(child));
        }

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

#endif