using System;

using Xamarin.Forms;

namespace ParticleEvents
{
	public class LandingPage : ContentPage
	{
		LandingPageViewModels ViewModel;

		public LandingPage()
		{
			ViewModel = new LandingPageViewModels();
			BindingContext = ViewModel;

			var indicator = new ActivityIndicator();
			var subscribeButton = new Button { Text = "Subscribe" };
			var lastButtonPressedLabel = new Label();

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(10, 0, 10, 0),
				Children = {
					indicator,
					subscribeButton,
					lastButtonPressedLabel
				}
			};

			subscribeButton.Clicked += async (object sender, EventArgs e) =>
			{
				await ViewModel.StartSubscription();
			};

			lastButtonPressedLabel.SetBinding(Label.TextProperty, "LastButtonPressed");
			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsRunning");
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await ViewModel.LoginAsync();
		}
	}
}


