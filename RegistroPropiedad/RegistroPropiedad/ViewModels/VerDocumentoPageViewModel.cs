using System;
using Prism.Navigation;
using Prism.Services;

namespace RegistroPropiedad.ViewModels
{
    public class VerDocumentoPageViewModel : VistaModeloBase
    {
        private string _UrlDocumento;
        public string UrlDocumento
        {
            get { return _UrlDocumento; }
            set
            {
                SetProperty(ref _UrlDocumento, value);
            }
        }

        public VerDocumentoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigation = navigationService;
            _dialogService = dialogService;
        }
    }
}
