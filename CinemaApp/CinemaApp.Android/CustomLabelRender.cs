using Android.App;
using Android.Content;
using Android.Graphics.Text;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CinemaApp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CinemaApp.CustomLabel), typeof(CustomLabelRender))]
namespace CinemaApp.Droid
{

    public class CustomLabelRender : LabelRenderer
    {
        public CustomLabelRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.JustificationMode = (Android.Text.JustificationMode)JustificationMode.InterWord;
            }
        }
    }
}