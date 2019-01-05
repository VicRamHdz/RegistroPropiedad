using System;
using System.Collections.Generic;
using Prism.Navigation;
using Prism.Services;
using RegistroPropiedad.Modelos;
using RegistroPropiedad.Views;

namespace RegistroPropiedad.ViewModels
{
    public class UbicacionPageViewModel : VistaModeloBase
    {
        private List<LocationModel> _items;

        public List<LocationModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }

        public UbicacionPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
            Init();
        }

        private void Init()
        {
            Items = new List<LocationModel>
            {
                new LocationModel
                {
                    Id = 1,
                    Name = "Portoviejo",
                    Description = "Avenida América y Reales Tamarindos ",
                    Latitude    = -1.0520576,
                    Longitude   = -80.4598898,
                    Rate = 5
                },
            };
        }
    }
}
