namespace CollectionViewTest;

public partial class MainPage : ContentPage
{
	int count = 0;
    double x = 0;
	public MainPage()
	{
		InitializeComponent();
	}

	private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
	{
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                SlipNSlide.Text = "Panning";
                break;
            case GestureStatus.Running:
                SlipNSlide.TranslationX = x + e.TotalX;
                break;
            case GestureStatus.Completed:
                x = SlipNSlide.TranslationX;
                SlipNSlide.Text = "Not Panning";
                break;
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedItem.Text = $"Selected: {e.CurrentSelection.First()}";
    }
}

