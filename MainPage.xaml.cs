using Plugin.Maui.Audio;

namespace AudioRecord;

public partial class MainPage : ContentPage
{

	readonly IAudioManager _audioManager;
	readonly IAudioRecorder _audioRecorder;

	public MainPage(IAudioManager audioManager)
	{
		InitializeComponent();

		_audioManager = audioManager;
		_audioRecorder = audioManager.CreateRecorder();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
		{
			// informar usuario
			return;
		}

		if( !_audioRecorder.IsRecording ) 
		{
			await _audioRecorder.StartAsync();
		}

		else
		{
			var recordedAudio = await _audioRecorder.StopAsync();

			var player = AudioManager.Current.CreatePlayer(recordedAudio.GetAudioStream());
			player.Play();
		}
	}
}

