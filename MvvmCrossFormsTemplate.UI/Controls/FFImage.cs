using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Transformations;
using Xamarin.Forms;
using FFImageLoading.Work;

namespace MvvmCrossFormsTemplate.UI.Controls
{
    public enum TransformationType
    {
        Default = 0,
        RoundedCorners = 1,
        Circle = 2,
        Zoom = 3,
        Blur = 4,
        CutCorners = 5,
        FlipHorizontal = 6,
        FlipVertical = 7,
        RoundedCorners20Radius = 8,
        RoundedCorners30Radius = 9,
        RoundedCorners40Radius = 10,
        RoundedCorners60Radius = 11,
        RoundedCorners80Radius = 12,
        Zoom2x = 13
    }

    public class FFImage : CachedImage
    {
        public FFImage()
        {
            DownsampleToViewSize = true;
        }

        public static readonly BindableProperty TransformationTypeProperty = BindableProperty.Create(nameof(TransformationType), typeof(TransformationType), typeof(FFImage), null, BindingMode.OneWay, null, TransformationChanged);

        public TransformationType TransformationType
        {
            get { return (TransformationType)GetValue(TransformationTypeProperty); }
            set { SetValue(TransformationTypeProperty, value); }
        }

        private static void TransformationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FFImage)bindable).ChangeTransformation((TransformationType)oldValue, (TransformationType)newValue);
        }

        private void ChangeTransformation(TransformationType oldValue, TransformationType newValue)
        {
            if (newValue == oldValue)
                return;

            switch (newValue)
            {
                case TransformationType.Circle:
                    Transformations = new List<ITransformation>() { new CircleTransformation() };
                    break;
                case TransformationType.RoundedCorners:
                    Transformations = new List<ITransformation>() { new CornersTransformation() { CornersTransformType = CornerTransformType.AllRounded } };
                    break;
                case TransformationType.CutCorners:
                    Transformations = new List<ITransformation>() { new CornersTransformation() { CornersTransformType = CornerTransformType.AllCut } };
                    break;
                case TransformationType.Zoom:
                    Transformations = new List<ITransformation>() { new CropTransformation(1.5, 0, 0) };
                    break;
                case TransformationType.Zoom2x:
                    Transformations = new List<ITransformation>() { new CropTransformation(2, 0, 0) };
                    break;
                case TransformationType.Blur:
                    double blurRadius = 50;
                    Transformations = new List<ITransformation>() { new BlurredTransformation(blurRadius) };
                    break;
                case TransformationType.FlipHorizontal:
                    Transformations = new List<ITransformation>() { new FlipTransformation(FlipType.Horizontal) };
                    break;
                case TransformationType.FlipVertical:
                    Transformations = new List<ITransformation>() { new FlipTransformation(FlipType.Vertical) };
                    break;
                case TransformationType.RoundedCorners20Radius: 
                    Transformations = new List<ITransformation>() { new RoundedTransformation() { Radius = 20 } };
                    break;
                case TransformationType.RoundedCorners30Radius: 
                    Transformations = new List<ITransformation>() { new RoundedTransformation() { Radius = 30 } };
                    break;
                case TransformationType.RoundedCorners40Radius: 
                    Transformations = new List<ITransformation>() { new RoundedTransformation() { Radius = 40 } };
                    break;
                case TransformationType.RoundedCorners60Radius: 
                    Transformations = new List<ITransformation>() { new RoundedTransformation() { Radius = 60 } };
                    break;
                case TransformationType.RoundedCorners80Radius: 
                    Transformations = new List<ITransformation>() { new RoundedTransformation() { Radius = 80 } };
                    break;
                default:
                    break;
            }
        }
    }
}
