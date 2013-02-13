using System;
using MonoTouch.UIKit;
using BigTed;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace BTProgressHUDDemo
{
	public class MainViewController : UIViewController
	{
		public MainViewController ()
		{

		}
		UITextView text;
		float progress = -1;
		NSTimer timer;
		public override void LoadView ()
		{
			base.LoadView ();
			View.BackgroundColor = UIColor.LightGray;

			MakeButton ("Show", () => {
				BTProgressHUD.Show (); 
				KillAfter ();
			});

			MakeButton ("Show Message", () => {
				BTProgressHUD.Show (status: "Oh, Hai!"); 
				KillAfter ();
			});

			MakeButton ("Show Success", () => {
				BTProgressHUD.ShowSuccessWithStatus("Great success!") ;
			});

			MakeButton ("Show Fail", () => {
				BTProgressHUD.ShowErrorWithStatus("Oh, thats bad") ;
			});



			MakeButton ("Dismiss", () => {
				BTProgressHUD.Dismiss (); 
			});

			MakeButton ("Progress", () => {
				progress = 0;
				BTProgressHUD.Show(progress, "Hello!");
				if (timer != null) 
				{
					timer.Invalidate();
				}
				timer = NSTimer.CreateRepeatingTimer(0.3f, delegate {
					progress += 0.1f;
					if (progress > 1)
					{
						timer.Invalidate();
						timer = null;
						BTProgressHUD.Dismiss();
					} else {
						BTProgressHUD.Show (progress, "Hello!");
					}


				});
				NSRunLoop.Current.AddTimer(timer, NSRunLoopMode.Common);
			});

			MakeButton ("Dismiss", () => {
				BTProgressHUD.Dismiss (); 
			});

		}

		void KillAfter (float timeout = 1)
		{
			if (timer != null) 
			{
				timer.Invalidate();
			}
			timer = NSTimer.CreateRepeatingTimer(timeout, delegate {
				BTProgressHUD.Dismiss();
			});
			NSRunLoop.Current.AddTimer(timer, NSRunLoopMode.Common);
		}
		float y = 20;
		void MakeButton(string text, Action del)
		{
			float x = 20;

			var button = new UIButton (UIButtonType.RoundedRect);
			button.Frame = new RectangleF (x, y, 280, 40);
			button.SetTitle (text, UIControlState.Normal);
			button.TouchUpInside += (o,e) => {
				del() ;
			};
			View.Add (button);
		
			
			y += 60;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}
	}
}

