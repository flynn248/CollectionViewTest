using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionViewTest.ViewModel;
public partial class MyViewModel : ObservableObject
{
    [ObservableProperty]
    int selectedNumber;
}
