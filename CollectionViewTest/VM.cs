global using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
namespace CollectionViewTest;

public partial class VM : ObservableObject
{
    public partial class TestText : ObservableObject
    {
        [ObservableProperty]
        string sampleText = "Here!";
    }

    [ObservableProperty]
    TestText testTxt = new();

    [RelayCommand]
    async Task Tap(TestText o)
    {
        Debug.WriteLine($"Tapped {o.SampleText}");
    }
}
