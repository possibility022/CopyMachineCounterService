using System.Windows;
using System.Windows.Controls;

namespace CopyinfoWPF.UControls
{
    /// <summary>
    /// Interaction logic for UcAddress.xaml
    /// </summary>
    public partial class UcAddress : UserControl
    {
        public UcAddress()
        {
            InitializeComponent();
        }

        public string Street
        {
            get
            {
                return (string)GetValue(StreetProperty);
            }
            set
            {
                SetValue(StreetProperty, value);
            }
        }

        public static readonly DependencyProperty StreetProperty =
            DependencyProperty.Register("Street", typeof(string),
            typeof(UcAddress), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(StreetChange)));

        private static void StreetChange(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            ((UcAddress)property).StreetL.Text = (string)args.NewValue;
        }



        public string HouseNumber
        {
            get
            {
                return (string)GetValue(HouseNumberProperty);
            }
            set
            {
                SetValue(HouseNumberProperty, value);
            }
        }

        public static readonly DependencyProperty HouseNumberProperty =
            DependencyProperty.Register("HouseNumber", typeof(string),
            typeof(UcAddress), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HouseNumberChange)));

        private static void HouseNumberChange(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            ((UcAddress)property).HouseNumberL.Text = (string)args.NewValue;
        }





        public string City
        {
            get
            {
                return (string)GetValue(CityProperty);
            }
            set
            {
                SetValue(CityProperty, value);
            }
        }

        public static readonly DependencyProperty CityProperty =
            DependencyProperty.Register("City", typeof(string),
            typeof(UcAddress), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(CityChange)));

        private static void CityChange(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            ((UcAddress)property).CityL.Text = (string)args.NewValue;
        }

        public string PostNumber
        {
            get
            {
                return (string)GetValue(PostNumberProperty);
            }
            set
            {
                SetValue(PostNumberProperty, value);
            }
        }

        public static readonly DependencyProperty PostNumberProperty =
            DependencyProperty.Register("PostNumber", typeof(string),
            typeof(UcAddress), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(PostNumberChange)));

        private static void PostNumberChange(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            ((UcAddress)property).PostNumberL.Text = (string)args.NewValue;
        }



        public string PostCity
        {
            get
            {
                return (string)GetValue(PostCityProperty);
            }
            set
            {
                SetValue(PostCityProperty, value);
            }
        }

        public static readonly DependencyProperty PostCityProperty =
            DependencyProperty.Register("PostCity", typeof(string),
            typeof(UcAddress), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(PostCityChange)));

        private static void PostCityChange(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            ((UcAddress)property).PostCityL.Text = (string)args.NewValue;
        }
    }
}
