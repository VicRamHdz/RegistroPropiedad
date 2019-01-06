using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading.Svg.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace RegistroPropiedad.Views
{
    public partial class PrincipalPage : MasterDetailPage
    {
        private INavigationService _navigation { get; set; }
        private IPageDialogService _dialogService { get; set; }
        private readonly FFImageLoading.Svg.Forms.SvgImageSourceConverter _imageSourceConverter = new SvgImageSourceConverter();

        public PrincipalPage(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            InitializeComponent();
            masterPage.listViewMenu.ItemSelected += OnItemSelected;
            (masterPage.BindingContext as MenuPageViewModel)._navigation = _navigation;
            (masterPage.BindingContext as MenuPageViewModel)._dialogService = _dialogService;
            //Master = menu = new MenuPage();
            //Detail = new NavigationPage(new NoticiasPage());
            //menu.ListView.ItemSelected += OnItemSelected;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null && item.Action == ActionType.OpenPage)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listViewMenu.SelectedItem = null;
                IsPresented = false;
            }
            else if (item != null && item.Action == ActionType.Command)
            {
                if (item.CommandName == "CerrarSesionCommand")
                {
                    (masterPage.BindingContext as MenuPageViewModel).CerrarSesionCommand.Execute(null);
                    masterPage.listViewMenu.SelectedItem = null;
                }
            }
            else if (item != null && item.Action == ActionType.CommandView)
            {
                if (item.CommandName == "QRCommand")
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                        {
                            await DisplayAlert("Movil Registro", "Necesita acceso a su camara", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                        //Best practice to always check that the key exists
                        if (results.ContainsKey(Permission.Camera))
                            status = results[Permission.Camera];
                    }

                    if (status != PermissionStatus.Granted)
                    {
                        await DisplayAlert("Info", "El permiso a la camara está deshabilitado para la aplicación, vaya a configuración y active dicho permiso", "OK");
                        return;
                    }

                    //creando vista scanner 
                    var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                    options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                    ZXing.BarcodeFormat.QR_CODE
                    };

                    var customOverlay = new Grid
                    {
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions = new RowDefinitionCollection(){
                        new RowDefinition(){ Height = 60 },

                        new RowDefinition(){ Height = new GridLength(50, GridUnitType.Star) },
                        new RowDefinition(){ Height = new GridLength(290) },
                        new RowDefinition(){ Height = new GridLength(50, GridUnitType.Star) },

                        new RowDefinition(){ Height = 50 },
                    },
                        ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition(){ Width = new GridLength(50, GridUnitType.Star)},
                        new ColumnDefinition(){ Width = new GridLength(300)},
                        new ColumnDefinition(){ Width = new GridLength(50, GridUnitType.Star)}
                    }
                    };

                    var titlelayout = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.LightGray,
                        Padding = new Thickness(0, 20, 0, 10)
                    };

                    var title = new Label
                    {
                        Text = "Lector de Documentos",
                        TextColor = Color.Black,
                        Style = (Style)Application.Current.Resources["TitleLabelStyle"],
                        FontSize = 18,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };

                    titlelayout.Children.Add(title);

                    var topShadow = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#80000000")
                    };

                    var leftShadow = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#80000000")
                    };

                    var xfSource = _imageSourceConverter.ConvertFromInvariantString("ScanQR.svg") as ImageSource;
                    var qrCode = new SvgCachedImage()
                    {
                        Aspect = Aspect.AspectFill,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Source = new SvgImageSource(xfSource, 0, 0, true)
                    };

                    var rightShadow = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#80000000")
                    };

                    var bottomShadow = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex("#80000000")
                    };

                    var abort = new CustomGradientButton
                    {
                        Text = "Cancelar",
                        HeightRequest = 50,
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        StartColor = (Color)Application.Current.Resources["ButtonStartBackColor"],
                        EndColor = (Color)Application.Current.Resources["ButtonEndBackColor"],
                        Style = (Style)Application.Current.Resources["BoldButtonStyle"],
                        TextColor = (Color)Application.Current.Resources["LightTextColor"]
                    };

                    customOverlay.Children.Add(titlelayout, 0, 0);
                    Grid.SetColumnSpan(titlelayout, 3);

                    customOverlay.Children.Add(topShadow, 0, 1);
                    Grid.SetColumnSpan(topShadow, 3);

                    customOverlay.Children.Add(leftShadow, 0, 2);
                    customOverlay.Children.Add(rightShadow, 2, 2);

                    customOverlay.Children.Add(qrCode, 1, 2);

                    customOverlay.Children.Add(bottomShadow, 0, 3);
                    Grid.SetColumnSpan(bottomShadow, 3);

                    customOverlay.Children.Add(abort, 0, 4);
                    Grid.SetColumnSpan(abort, 3);

                    var scanPage = new ZXingScannerPage(options, customOverlay);

                    abort.Clicked += (obj, sender1) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            scanPage.IsScanning = false;
                            await Navigation.PopModalAsync();
                        });
                    };

                    scanPage.OnScanResult += (result) =>
                    {
                        scanPage.IsScanning = false;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopModalAsync();
                            if (string.IsNullOrEmpty(result.Text))
                            {
                                DisplayAlert("Info", "Ningún Código Válido se  Escaneado", "OK");
                            }
                            else
                            {
                                result.Text.Replace("https://pagos.registropropiedadportoviejo.gob.ec/ventanilla/validar.xhtml?codigo=",
                                    "https://pagos.registropropiedadportoviejo.gob.ec/ventanilla/pdfjs/web/viewer.html?file=/ventanilla/api/certificado/file/");
                                Detail = new NavigationPage(new VerDocumentoPage(result.Text));
                            }
                        });
                    };

                    await Navigation.PushModalAsync(scanPage);
                }
            }
        }
    }
}
