using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace stitoff_rx_subscription
{
    public partial class App : Application
    {
        private IDisposable _subscription;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            _subscription = Observable.Create<Unit>(observable =>
            {
                MessagingCenter.Subscribe<App, string>(this, "OnAction", (sender, message) =>
                {
                    Console.WriteLine($"App goes to: {message}");
                    observable.OnNext(Unit.Default);
                });

                return Disposable.Create(() => MessagingCenter.Unsubscribe<App, string>(this, "OnAction"));
            }).Subscribe();
        }

        protected override void OnStart()
        {
            MessagingCenter.Send(this, "OnAction", nameof(OnStart));
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send(this, "OnAction", nameof(OnSleep));
        }

        protected override void OnResume()
        {
            MessagingCenter.Send(this, "OnAction", nameof(OnResume));
        }
    }
}
