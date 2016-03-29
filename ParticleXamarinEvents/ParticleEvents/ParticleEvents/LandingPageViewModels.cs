using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Particle;
using ParticleTest;
using Particle.Helpers;
namespace ParticleEvents
{
	public class LandingPageViewModels : INotifyPropertyChanged
	{
		public LandingPageViewModels()
		{
			lastButtonPressed = "Last Button Pressed: ";
		}

		string lastButtonPressed;
		public string LastButtonPressed
		{
			get { return lastButtonPressed; }
			set
			{
				if (lastButtonPressed == value)
					return;
				lastButtonPressed = value;
				OnPropertyChanged("LastButtonPressed");
			}
		}

		bool isRunning;
		public bool IsRunning
		{
			get { return isRunning; }
			set
			{
				isRunning = value;
				OnPropertyChanged("IsRunning");
			}
		}

		public async Task LoginAsync()
		{
			IsRunning = true;

			await ParticleCloud.SharedInstance.CreateOAuthClientAsync(SecureInformation.Token, "Xamarin");
			await ParticleCloud.SharedInstance.LoginWithUserAsync(SecureInformation.Username, SecureInformation.Password);

			IsRunning = false;
		}

		public async Task StartSubscription()
		{
			var id = await ParticleCloud.SharedInstance.SubscribeToMyDevicesEventsWithPrefixAsync("button-press", SecureInformation.DeviceId, HandleEvents);
		}

		public void HandleEvents(object sender, ParticleEventArgs e)
		{
			LastButtonPressed = "Last Button Pressed: " + e.EventData.Data;
			System.Diagnostics.Debug.WriteLine(e.EventData);
			System.Diagnostics.Debug.WriteLine(e.EventData.Data);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}