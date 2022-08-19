
#if ANDROID
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionViewTest;

public class RecyclerTouchListenerHandler : IClickListener
{
    public void OnClick(Android.Views.View view, int position)
    {
        //view.NestedScrollingEnabled = false;
        //System.Diagnostics.Debug.WriteLine($"I have clicked {view} at {position}");

        //var list = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.ViewMapper.GetKeys();
        //var list2 = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.GetKeys();


        //foreach (var key in list2)
        //{
        //    System.Diagnostics.Debug.WriteLine(key);

        //    var action = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.GetProperty(key);
        //    System.Diagnostics.Debug.WriteLine($"{nameof(action.Target)}: {action.Target}\n{nameof(action.Method)}: {action.Method}\n{action}");
        //}
    }

    public void OnLongClick(Android.Views.View view, int position)
    {
        //System.Diagnostics.Debug.WriteLine($"I have clicked {view} at {position}");
    }
}

#endif