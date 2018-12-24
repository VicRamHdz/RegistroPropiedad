using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroPropiedad.CustomControls;
using Xamarin.Forms;

namespace RegistroPropiedad.Views
{
    public partial class PrincipalPage : MasterDetailPage
    {
        public PrincipalPage()
        {
            InitializeComponent();
            masterPage.listViewMenu.ItemSelected += OnItemSelected;
            //Master = menu = new MenuPage();
            //Detail = new NavigationPage(new NoticiasPage());
            //menu.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listViewMenu.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
