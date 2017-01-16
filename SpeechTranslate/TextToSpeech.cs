using AVFoundation;
using SpeechTranslate;

public class TextToSpeech : ITextToSpeech
{
	public TextToSpeech() { }

	public void Speak(string text)
	{
		var read =

		var speechSynthesizer = new AVSpeechSynthesizer();
		var speechUtterance =
		  new AVSpeechUtterance(SpeechToTextView);
		speechSynthesizer.SpeakUtterance(speechUtterance);
	}
}
