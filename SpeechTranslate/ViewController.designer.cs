// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SpeechTranslate
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AudioUrlTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PerformSpeechRecognitionButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider pitchSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Speech { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView SpeechToTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider volumeSlider { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AudioUrlTextField != null) {
                AudioUrlTextField.Dispose ();
                AudioUrlTextField = null;
            }

            if (PerformSpeechRecognitionButton != null) {
                PerformSpeechRecognitionButton.Dispose ();
                PerformSpeechRecognitionButton = null;
            }

            if (pitchSlider != null) {
                pitchSlider.Dispose ();
                pitchSlider = null;
            }

            if (Speech != null) {
                Speech.Dispose ();
                Speech = null;
            }

            if (SpeechToTextView != null) {
                SpeechToTextView.Dispose ();
                SpeechToTextView = null;
            }

            if (volumeSlider != null) {
                volumeSlider.Dispose ();
                volumeSlider = null;
            }
        }
    }
}