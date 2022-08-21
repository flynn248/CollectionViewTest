#if ANDROID
using Android.Views;
using CollectionViewTest.Platforms.Android.Controls;
using CollectionViewTest.Platforms.Android.Interfaces;
#endif

namespace CollectionViewTest;

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
                break;
            case MotionEventActions.Up:
                x = SlipNSlide.TranslationX;
                SlipNSlide.Text = "Not Panning";
                break;
        }
    }
#endif
}

