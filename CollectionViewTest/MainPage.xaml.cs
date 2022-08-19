#if ANDROID
using Android.Views;
using CollectionViewTest.Platforms.Android.Controls;
using MauiApp1.CustomView;
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
    int count = 0;
    double x = 0;

    public MainPage(VM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
#if ANDROID

        MessagingCenter.Subscribe<Simple, float[]>(this, "ScrollEvent", ScrollTo);
#endif
    }

    ~MainPage()
    {
#if ANDROID
        MessagingCenter.Unsubscribe<Simple, float[]>(this, "ScrollEvent");
#endif
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                //SlipNSlide.Text = "Panning";
                break;
            case GestureStatus.Running:
                SlipNSlide.TranslationX = x + e.TotalX;
                Debug.WriteLine($"TrX: {SlipNSlide.TranslationX:f2} :: TotX: {e.TotalX} :: x{x}");

                break;
            case GestureStatus.Completed:
                x = SlipNSlide.TranslationX;
                //SlipNSlide.Text = "Not Panning";
                break;
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedItem.Text = $"Selected: {e.CurrentSelection.First()}";
        Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("Custom", (handler, view) =>
        {


        });

    }


    private void test()
    {
        Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("Custom", (handler, view) =>
        {

            //handler.PlatformView.DispatchTouchEvent()
        });


    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        //CollectionView cv = (CollectionView)sender;

        foreach (var a in Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.GetKeys())
        {
            System.Diagnostics.Debug.WriteLine(a);
        }
        //Label s = (Label)sender;
        var mainDisp = Microsoft.Maui.Devices.DeviceDisplay.Current.MainDisplayInfo;
        Debug.WriteLine($"{mainDisp.Width} {mainDisp.Height} {mainDisp.Density}");
    }

#if ANDROID

    private void ScrollTo(object sender, float[] args)
    {


        float xDiff = args[0];
        float xDiff2 = -args[1];

        //Debug.WriteLine($"{xDiff} {yDiff}");
        //SlipNSlide.TranslationX =  xDiff;
        //SlipNSlide.TranslationX = xDiff;

        //System.Diagnostics.Debug.WriteLine($"TransX: {SlipNSlide.TranslationX:f3} :: {xDiff:f3} :: {SlipNSlide.X}");
        //x = SlipNSlide.TranslationX;

    }

#endif

    private void a_HandlerChanging(object sender, HandlerChangingEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Changing");
        //((sender as CollectionView).Handler.PlatformView as Android.Views.View).
        //Microsoft.Maui.Handlers.ElementHandler.ElementMapper.
        //Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler;
        //Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer.Mapper;
        //Microsoft.Maui.PropertyMapper<Microsoft.Maui.Controls.CollectionView,Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler>

        Debug.WriteLine(Microsoft.Maui.Networking.Connectivity.ConnectionProfiles);
        //Microsoft.Maui.ApplicationModel.AppActions.Current.AppActionActivated;
        Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.ViewMapper.AppendToMapping("MyLabelMapping", (handler, view) =>
        {
            //(handler.ContainerView as CollectionView);
        });
#if ANDROID
        Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.PrependToMapping("abc", (handler, view) =>
        {
            if (view == MyCollectionView)
            {
                handler.PlatformView.AddOnItemTouchListener(new RecyclerTouchListener(handler.Context, (RecyclerView)view.ToPlatform(handler.MauiContext), this));
            }
        });
#endif
#if ANDROID

        //Microsoft.Maui.Handlers.LabelHandler.Mapper.PrependToMapping("abc", (handler, view) =>
        //{
        //    handler.PlatformView.SetBackgroundColor(Colors.Red.ToPlatform());
        //    handler.PlatformView.
        //    //view.Background.BackgroundColor = Microsoft.Maui.Graphics.Colors.Green;
        //});

        //Microsoft.Maui.Controls.Handlers.Items.



        Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.ElementCommandMapper.AppendToMapping("abc", (handler, view, obj) =>
        {
            System.Diagnostics.Debug.WriteLine("Pineapple on pizza.");
        });


#endif
    }

    private void a_HandlerChanged(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Changed");

    }

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MainPage(new VM()));
        
    }

    public void OnClick(Android.Views.View view, int position)
    {
        throw new NotImplementedException();
    }

    public void OnLongClick(Android.Views.View view, int position)
    {
        throw new NotImplementedException();
    }
#if ANDROID
    public void OnScroll(ScrollEventsArg e)
    {
        //System.Diagnostics.Debug.WriteLine(e.MotionEventActions);
        switch (e.MotionEventActions)
        {
            case MotionEventActions.Down:
                //SlipNSlide.Text = "Panning";
                break;
            case MotionEventActions.Move:
                SlipNSlide.TranslationX = x + e.TotalX;
                Debug.WriteLine($"TrX: {SlipNSlide.TranslationX:f2} :: TotX: {e.TotalX} :: x{x}");

                break;
            case MotionEventActions.Up:
                x = SlipNSlide.TranslationX;
                //SlipNSlide.Text = "Not Panning";
                break;
        }
    }
#endif
}

