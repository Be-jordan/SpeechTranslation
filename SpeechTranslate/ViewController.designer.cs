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
        UIKit.UIButton Dictate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DictationResults { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Dictate != null) {
                Dictate.Dispose ();
                Dictate = null;
            }

            if (DictationResults != null) {
                DictationResults.Dispose ();
                DictationResults = null;
            }
        }
    }
}