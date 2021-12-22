using Android.Content;
using Android.Graphics.Drawables;
using MvvmCrossFormsTemplate.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EmptyEntry), typeof(MvvmCrossFormsTemplate.Droid.Renderers.EmptyEntryRenderer))]
namespace MvvmCrossFormsTemplate.Droid.Renderers
{
    public class EmptyEntryRenderer : EntryRenderer
    {
        public EmptyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                Control.SetTextColor(Element.TextColor.ToAndroid());
            }
        }
    }
}