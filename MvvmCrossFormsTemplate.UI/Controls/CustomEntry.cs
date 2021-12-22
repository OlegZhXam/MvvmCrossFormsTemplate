using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MvvmCrossFormsTemplate.UI.Controls
{
    public partial class CustomEntry : Frame
    {
        public const string REGEX_LETTERONLY = "^[A-Za-z]*$";
        public const string REGEX_NONDIGITS = "^[^0-9]*$";
        public const string REGEX_DIGITSONLY = "^[0-9]*$";
        public const string REGEX_DECIMAL = "^\\d+((\\.|,)\\d+)?$";
        public const string REGEX_EMAIL = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string REGEX_PASSWORD = "^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})";
        public const string REGEX_PHONE = "^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$";

        readonly ImageButton _leftIconImage =
            new ImageButton
            {
                InputTransparent = true,
                IsVisible = false,
                Margin = new Thickness(0, 0, 16, 0),
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 24
            };

        private readonly ImageButton _rightIconImage =
            new ImageButton
            {
                Margin = new Thickness(16,0,0,0),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                HeightRequest = 24
            };

        private readonly Entry _txtInput;

        public CustomEntry()
        {
            _txtInput = GetInputEntry();
            BackgroundColor = Color.White;
            CornerRadius = 20;
            BorderColor = Color.White;
            Padding = new Thickness(16,0,16,0);
            HasShadow = false;

            var content = new Grid
            {
                BackgroundColor = Color.Transparent,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        BackgroundColor = Color.Transparent,
                        Children =
                        {
                            _leftIconImage,
                            _txtInput
                        }
                    },
                    _rightIconImage
                }
            };

            _txtInput.TextChanged += TxtInput_TextChanged;
            _txtInput.Completed += (s, args) => { ExecuteCommand(); Completed?.Invoke(this, new EventArgs()); FocusNext(); };
            _txtInput.Focused += (s, args) => { var arg = new FocusEventArgs(this, true); FocusedCommand?.Execute(arg); Focused?.Invoke(this, arg); };
            _txtInput.Unfocused += (s, args) => { var arg = new FocusEventArgs(this, false); UnfocusedCommand?.Execute(arg); Unfocused?.Invoke(this, arg); };
            Reset();
            Content = content;
        }

        private bool _isDisabled;
        private int _minLength;
        public event EventHandler Completed;
        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler Clicked;
        public event EventHandler ValidationChanged;
        public new event EventHandler<FocusEventArgs> Unfocused;
        public new event EventHandler<FocusEventArgs> Focused;

        #region Properties
        
        public string Text
        {
            get => _txtInput.Text;
            set
            {
                _txtInput.Text = value;
                OnPropertyChanged();
            }
        }
        
        public string LeftIconImage
        {
            get => _leftIconImage.Source.ToString();
            set
            {
                _leftIconImage.IsVisible = !string.IsNullOrEmpty(value);
                _leftIconImage.Source = value;
            }
        }

        public string RightIconImage
        {
            get => _rightIconImage.Source.ToString();
            set
            {
                _rightIconImage.IsVisible = !string.IsNullOrEmpty(value);
                _rightIconImage.Source = value;
            }
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }
        
        public Color PlaceholderColor
        {
            get => _txtInput.PlaceholderColor;
            set => _txtInput.PlaceholderColor = value;
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
       
        public int MaxLength
        {
            get => _txtInput.MaxLength;
            set => _txtInput.MaxLength = value;
        }
        
        public int MinLength
        {
            get => _minLength;
            set => _minLength = value;
        }
        
        public string FontFamily
        {
            get => _txtInput.FontFamily;
            set
            {
                _txtInput.FontFamily = value;
            }
        }

        public bool IsDisabled
        {
            get => _isDisabled;
            set
            {
                _isDisabled = value;
                Opacity = value ? 0.6 : 1;
                _txtInput.IsEnabled = !value;
            }
        }
        
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        
        public ICommand CompletedCommand { get; set; }
        
        public Command<FocusEventArgs> FocusedCommand { get; set; }
        
        public Command<FocusEventArgs> UnfocusedCommand { get; set; }
        
        public object CommandParameter {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ICommand RightIconCommand
        {
            get => _rightIconImage.Command;
            set => _rightIconImage.Command = value;
        }
        
        [TypeConverter(typeof(FontSizeConverter))]
        public double TextFontSize
        {
            get => (double)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        public Keyboard Keyboard
        {
            get => _txtInput.Keyboard;
            set => _txtInput.Keyboard = value;
        }
        
        public int CursorPosition
        {
            get => (int)GetValue(CursorPositionProperty);
            set => SetValue(CursorPositionProperty, value);
        }
        #endregion

        #region BindableProperties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry), null, BindingMode.TwoWay, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).Text = (string)nv);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntry), false, propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.IsPassword = (bool)nv);
        public static readonly BindableProperty LeftIconImageProperty = BindableProperty.Create(nameof(LeftIconImage), typeof(string), typeof(CustomEntry), null, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).LeftIconImage = (string)nv);
        public static readonly BindableProperty RightIconImageProperty = BindableProperty.Create(nameof(RightIconImage), typeof(string), typeof(CustomEntry), null, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).RightIconImage = (string)nv);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomEntry), Entry.TextColorProperty.DefaultValue, propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.TextColor = (Color)nv);
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomEntry), Color.LightGray, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).PlaceholderColor = (Color)nv);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry), default(string), propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.Placeholder = (string)nv);
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(CustomEntry), int.MaxValue, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).MaxLength = (int)nv);
        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create(nameof(MinLength), typeof(int), typeof(CustomEntry), 0, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).MinLength = (int)nv);
        public static readonly BindableProperty CompletedCommandProperty = BindableProperty.Create(nameof(CompletedCommand), typeof(ICommand), typeof(CustomEntry), null, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).CompletedCommand = (ICommand)nv);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomEntry), propertyChanged: (bo, ov, nv) => (bo as CustomEntry).CommandParameter = nv);
        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(CustomEntry), Device.GetNamedSize(NamedSize.Default, typeof(Label)), propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.FontSize = (double)nv);
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomEntry), TextAlignment.Start, propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.HorizontalTextAlignment = (TextAlignment)nv);
        public static readonly BindableProperty CursorPositionProperty = BindableProperty.Create(nameof(CursorPosition), typeof(int), typeof(CustomEntry), 0, propertyChanged: (bo, ov, nv) => (bo as CustomEntry)._txtInput.CursorPosition = (int)nv);
        public static readonly BindableProperty RightIconCommandProperty = BindableProperty.Create(nameof(RightIconCommand), typeof(ICommand), typeof(CustomEntry), null, propertyChanged: (bo, ov, nv) => (bo as CustomEntry).RightIconCommand = (ICommand)nv);
        #endregion

        #region Methods
        void ExecuteCommand()
        {
            if (CompletedCommand?.CanExecute(CommandParameter ?? this) ?? false)
                CompletedCommand?.Execute(CommandParameter ?? this);
        }

        public virtual new void Focus()
        {
            Focused?.Invoke(this, new FocusEventArgs(this, true));
            _txtInput.Focus();
        }
        
        public virtual new void Unfocus()
        {
            Focused?.Invoke(this, new FocusEventArgs(this, true));
            _txtInput.Unfocus();
        }

        public virtual void FocusNext()
        {
            if (Parent is Layout<View> parent)
            {
                int index = parent.Children.IndexOf(this);
                for (int i = index + 1; i < (index + 4).Clamp(0, parent.Children.Count); i++)
                {
                    if (parent.Children[i] is CustomEntry)
                    {
                        (parent.Children[i] as CustomEntry).Focus();
                        break;
                    }
                }
            }
        }
        
        public void Reset()
        {
            _txtInput.Text = null;
        }
        private void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(TextProperty, _txtInput.Text);

            TextChanged?.Invoke(this, e);
        }

        private protected virtual Entry GetInputEntry()
        {
            return new EmptyEntry
            {
                TextColor = Color.Black,
                PlaceholderColor = Color.LightGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
            };
        }
        #endregion
    }

    public class EmptyEntry : Entry
    {

    }
}