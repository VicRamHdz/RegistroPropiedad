using System;
using FFImageLoading.Svg.Forms;
using Prism.Unity;
using RegistroPropiedad.Models;
using RegistroPropiedad.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RegistroPropiedad
{
    public partial class App : PrismApplication
    {
        public static PerfilUsuarioModelo UserProfileInfo { get; set; }

        public App() : this(null) { }

        public App(IPlatformInitializer plataInitializer = null) : base(plataInitializer)
        {

        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var ignore = new SvgCachedImage();

            NavigationService.NavigateAsync("InicialPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<InicialPage>();
            Container.RegisterTypeForNavigation<NoticiasPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
