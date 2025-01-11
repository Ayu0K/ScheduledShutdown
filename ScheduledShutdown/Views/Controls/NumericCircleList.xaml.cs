using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduledShutdown.Views.Controls
{
    /// <summary>
    /// NumericCircleList.xaml 的交互逻辑
    /// </summary>
    public partial class NumericCircleList : UserControl
    {

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericCircleList), new PropertyMetadata(0));



        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericCircleList), new PropertyMetadata(60));



        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(NumericCircleList), new PropertyMetadata(1));



        public int Value
        {
            get 
            {
                return (int)GetValue(ValueProperty);
            }
            set 
            {
                _itemIndex = (_items.FindIndex((x) => x == value) - 2 + _items.Count) % _items.Count;
                UpdateItems();
            }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericCircleList), new PropertyMetadata(0));



        public int Format
        {
            get { return (int)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(int), typeof(NumericCircleList), new PropertyMetadata(2));


        private readonly List<int> _items = new List<int>();

        private int _itemIndex = 0;

        public NumericCircleList()
        {
            InitializeComponent();
        }

        private void UpdateItems(bool direction)
        {
            if (direction)
            {
                // 向上滚动
                _itemIndex = (_itemIndex + 1) % _items.Count;
            }
            else
            {
                // 向下滚动
                _itemIndex = (_itemIndex - 1 + _items.Count) % _items.Count;
            }
            UpdateItems();
        }

        private void UpdateItems()
        {
            int itemPtr = _itemIndex;
            foreach (var item in NumberPresenter.Children)
            {
                if (item is TextBlock tb)
                {
                    tb.Text = string.Format($"{{0:D{Format}}}", _items[itemPtr % _items.Count]);
                    itemPtr++;
                }
            }
            SetValue(ValueProperty, _items[(_itemIndex + 2) % _items.Count]);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                UpdateItems(false);
            }
            else if (e.Delta < 0)
            {
                UpdateItems(true);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = MinValue; i < MaxValue; i += Step)
            {
                _items.Add(i);
            }
            _itemIndex = (_items.FindIndex((x) => x == Value) - 2 + _items.Count) % _items.Count;
            UpdateItems();
        }
    }
}
