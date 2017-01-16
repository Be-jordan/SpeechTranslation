using System;

using UIKit;
using Speech;
using Foundation;
using AVFoundation;

namespace SpeechTranslate
{
	public partial class ViewController : UIViewController
	{
		public float volume;

		public float pitch;

		protected ViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			PerformSpeechRecognitionButton.TouchUpInside += (sender, e) =>
			{
				var url = NSBundle.MainBundle.GetUrlForResource(AudioUrlTextField.Text, "mp3");
				RecognizeSpeech(url);
			};

			Speech.TouchUpInside += (sender, e) =>
			{
				Voice();
			};
		}

		public void Voice()
		{
			var speechSynthesizer = new AVSpeechSynthesizer();
			var speechUtterance =
			  new AVSpeechUtterance(SpeechToTextView.Text);
			speechSynthesizer.SpeakUtterance(speechUtterance);
		}

		void Speak(string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer();

			var speechUtterance = new AVSpeechUtterance(text)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
				Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
				Volume = volume,
				PitchMultiplier = pitch
			};

			speechSynthesizer.SpeakUtterance(speechUtterance);
		}

		void InitPitchAndVolume()
		{
			volumeSlider.MinValue = 0;
			volumeSlider.MaxValue = 1.0f;
			volumeSlider.SetValue(volume, false);

			pitchSlider.MinValue = 0.5f;
			pitchSlider.MaxValue = 2.0f;
			pitchSlider.SetValue(pitch, false);

			volumeSlider.ValueChanged += (sender, e) =>
			{
				volume = volumeSlider.Value;
			};

			pitchSlider.ValueChanged += (sender, e) =>
			{
				pitch = volumeSlider.Value;
			};
		}
		public void RecognizeSpeech(NSUrl url)
		{
			var recognizer = new SFSpeechRecognizer(new NSLocale("en-US"));

			if (recognizer == null)
				return;
			if (!recognizer.Available)
				return;


			var request = new SFSpeechUrlRecognitionRequest(url);
			recognizer.GetRecognitionTask(request, (SFSpeechRecognitionResult result, NSError err) =>
			{
				if (err != null)
				{
					var alertViewController = UIAlertController.Create("Error",
																   $"An error recognizing speech ocurred: {err.LocalizedDescription}",
																   UIAlertControllerStyle.Alert);
					PresentViewController(alertViewController, true, null);
				}
				else
				{
					if (result.Final)
					{
						InvokeOnMainThread(() =>
						{
							SpeechToTextView.Text = result.BestTranscription.FormattedString;
						});
					}
				}
			});

		}
	}
}
