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

