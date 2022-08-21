#if ANDROID
using Android.Views;
using CollectionViewTest.Platforms.Android.Controls;
#endif
using System.Diagnostics;
namespace CollectionViewTest;
/* 08/18/2022
 * Having the interface is the best option. Just need to clean up the code a bit and then go from there.
 * It is working exactly how it should be and opens the door for other future ideas and use cases.
 * Need to consider renaming different aspects when implementing it.
 * 
 */
public partial class MainPage : ContentPage
#if ANDROID
    , IScrollEvent
#endif
{
#if ANDROID
    double x = 0;
#endif
    public MainPage(ViewModel.MyViewModel viewModel)
    {
#if ANDROID
        Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.PrependToMapping("PanGesture", (handler, view) =>
        {
            if (view == MyCollectionView)
            {
                //handler.PlatformView.AddOnItemTouchListener(new RecyclerTouchListener(handler.Context, (RecyclerView)view.ToPlatform(handler.MauiContext), this));
                handler.PlatformView.AddOnItemTouchListener(new RecyclerTouchListener(handler.Context, this));
            }
        });
#endif
        InitializeComponent();
        BindingContext = viewModel;
    }
   
#if ANDROID
    public void OnScroll(ScrollEventArg e)
    {
        switch (e.MotionEventActions)
        {
            case MotionEventActions.Down:
                SlipNSlide.Text = "Panning";
                break;
            case MotionEventActions.Move:
                SlipNSlide.TranslationX = x + e.TotalX;
                //Debug.WriteLine($"TrX: {SlipNSlide.TranslationX:f2} :: TotX: {e.TotalX} :: x{x}");
                break;
            case MotionEventActions.Up:
                x = SlipNSlide.TranslationX;
                SlipNSlide.Text = "Not Panning";
                break;
        }
    }
#endif
}

