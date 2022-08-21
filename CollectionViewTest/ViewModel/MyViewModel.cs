global using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
namespace CollectionViewTest.ViewModel;
public partial class MyViewModel : ObservableObject
{
    [ObservableProperty]
    int selectedNumber;
}
