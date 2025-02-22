using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TickTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            //if (DataContext == null || DataContext is not MainViewModel model)
            //{
            //    return;
            //}

            //model.Initialize(_fieldCanvas.Bounds);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            if (DataContext == null || DataContext is not MainViewModel model)
            {
                return;
            }

            model.Initialize(_fieldCanvas.Bounds);
        }

        private void OnClickedStart(object? sender, RoutedEventArgs e)
        {
            if (DataContext == null || DataContext is not MainViewModel model)
            {
                return;
            }

            model.Start();
        }

        private void OnClickedStop(object? sender, RoutedEventArgs e)
        {
            if (DataContext == null || DataContext is not MainViewModel model)
            {
                return;
            }

            model.Stop();
        }
    }
}