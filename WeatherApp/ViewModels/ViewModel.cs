namespace WeatherApp.ViewModels
{
    using GalaSoft.MvvmLight;

    public class ViewModel : ViewModelBase
    {
        private int busyCount;

        protected int BusyCount
        {
            get => this.busyCount;
            set
            {
                this.busyCount = value;
                base.RaisePropertyChanged(() => this.IsBusy);
                
            }
        }

        public bool IsBusy => this.BusyCount > 0;

    }
}