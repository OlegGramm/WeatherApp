namespace WeatherApp.Helpers.Behaviors
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    public class EnterPressedHandlerBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EnterPressedHandlerBehavior), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get => (ICommand) this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewKeyDown += this.OnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewKeyDown -= this.OnPreviewKeyDown;
            base.OnDetaching();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Command?.Execute(null);
            }
        }
    }
}