namespace WeatherApp.Helpers.Actions
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Interactivity;

    public class ChangePropertyAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(nameof(PropertyName), typeof(string), typeof(ChangePropertyAction));

        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register(nameof(PropertyValue), typeof(object), typeof(ChangePropertyAction));

        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(nameof(TargetObject), typeof(object), typeof(ChangePropertyAction));

        public string PropertyName
        {
            get => (string) this.GetValue(PropertyNameProperty);
            set => this.SetValue(PropertyNameProperty, value);
        }

        public object PropertyValue
        {
            get => this.GetValue(PropertyValueProperty);
            set => this.SetValue(PropertyValueProperty, value);
        }
        
        public object TargetObject
        {
            get => this.GetValue(TargetObjectProperty);
            set => this.SetValue(TargetObjectProperty, value);
        }
        
        protected override void Invoke(object parameter)
        {
            var target = this.TargetObject ?? this.AssociatedObject;
            var propertyInfo = target.GetType().GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

            propertyInfo.SetValue(target, this.PropertyValue);
        }
    }
}