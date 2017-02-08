using System;
using UIKit;
using Speech;
using Foundation;
using AVFoundation;
using System.Web;
using System.Net;
using System.Text;

namespace SpeechTranslate
//Test Commit
{
    public partial class ViewController : UIViewController
    {

        public float volume;

        public float pitch;


        private readonly AVAudioEngine _audioEngine = new AVAudioEngine();
        private readonly SFSpeechRecognizer _speechRecognizer = new SFSpeechRecognizer();
        private SFSpeechAudioBufferRecognitionRequest _speechRequest;
        private SFSpeechRecognitionTask _currentSpeechTask;

        protected ViewController(IntPtr handle) : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Dictate.Enabled = false;

            ReadButton.TouchUpInside += (sender, e) =>
                {
                    Voice();
                };

            SFSpeechRecognizer.RequestAuthorization(status =>
            {
                if (status != SFSpeechRecognizerAuthorizationStatus.Authorized)
                    return;

                _audioEngine.InputNode.InstallTapOnBus(
                    bus: 0,
                    bufferSize: 1024,
                    format: _audioEngine.InputNode.GetBusOutputFormat(0),
                    tapBlock: (buffer, when) => _speechRequest?.Append(buffer));
                _audioEngine.Prepare();

                InvokeOnMainThread(() =>
                {
                    Dictate.Enabled = true;
                    Dictate.TouchUpInside += OnDictate;
                });


            });
        }
        public void OnDictate(object sender, EventArgs e)
        {
            InvokeOnMainThread(() =>
            {
                if (_currentSpeechTask?.State == SFSpeechRecognitionTaskState.Running)
                {
                    InvokeOnMainThread(() =>
                       Dictate.SetTitle("Start Dictating", UIControlState.Normal));

                    stopDictating();
                }
                else if (_currentSpeechTask == null || _currentSpeechTask.State == SFSpeechRecognitionTaskState.Completed)
                {
                    InvokeOnMainThread(() =>
                    {
                        Dictate.SetTitle("Stop Dictating", UIControlState.Normal);
                        DictationResults.Text = "Waiting for dictation...";
                    });

                    startDictating();
                }
            });
        }

        public void Voice()
        {
            var speechSynthesizer = new AVSpeechSynthesizer();
            var speechUtterance =
                new AVSpeechUtterance(DictationResults.Text);
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



        private void startDictating()
        {
            NSError error;
            _audioEngine.StartAndReturnError(out error);

            _speechRequest = new SFSpeechAudioBufferRecognitionRequest();
            _currentSpeechTask = _speechRecognizer.GetRecognitionTask(_speechRequest, (result, err) => InvokeOnMainThread(() =>
            {
                if (result == null)
                {
                    return;
                }

                DictationResults.Text = result.BestTranscription.FormattedString;
                DictationResults.BackgroundColor = result.Final ? UIColor.Black : UIColor.Green;
                DictationResults.TextColor = UIColor.White;
            }));
        }

        private void stopDictating()
        {
            _audioEngine.Stop();
            _speechRequest?.EndAudio();

            var wordsToTranslate = DictationResults.Text;
            // var googleApi = new GoogleApiObject();
            // var translation = googleApi.Translate(wordsToTranslate);
        }
    }
}

