using CollectionViewTest.Platforms.Android.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionViewTest;

public partial class ScrollEvent : IScrollEvent
{
    public virtual void OnScroll(ScrollEventsArg e)
    {
        System.Diagnostics.Debug.WriteLine("Inside of virtual OnScroll event");
    }
}
