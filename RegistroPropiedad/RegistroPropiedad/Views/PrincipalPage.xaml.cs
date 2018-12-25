using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.ViewModels;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class PrincipalPage : MasterDetailPage
    {
        private INavigationService _navigation { get; set; }
        private IPageDialogService _dialogService { get; set; }

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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
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
        }
    }
}
