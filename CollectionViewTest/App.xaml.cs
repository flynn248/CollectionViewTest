#if ANDROID
global using Microsoft.Maui.Platform;
global using AndroidX.RecyclerView.Widget;
#endif

namespace CollectionViewTest;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
