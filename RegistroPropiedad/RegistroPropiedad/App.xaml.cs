using System;
using Akavache;
using FFImageLoading.Svg.Forms;
using Prism.Unity;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RegistroPropiedad
{
    public partial class App : PrismApplication
    {
        public static PerfilUsuarioModelo PerfilUsuarioInfo { get; set; }

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
            Container.RegisterTypeForNavigation<IniciarSesionPage>();
            Container.RegisterTypeForNavigation<RegistroPage>();
            Container.RegisterTypeForNavigation<VerificarDatosPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //Make sure you set the application name before doing any inserts or gets
            BlobCache.ApplicationName = "RegistroPropiedadBlob";
            BlobCache.EnsureInitialized();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            BlobCache.Shutdown().Wait();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            BlobCache.EnsureInitialized();
        }
    }
}
