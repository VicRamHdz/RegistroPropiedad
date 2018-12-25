using System;
using System.Diagnostics;
using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace RegistroPropiedad.ViewModels
{
    public class VistaModeloBase : BindableBase, INavigatedAware
    {
        private string _busyMessage = "";

        private bool _isBusy;
        internal bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                try
                {
                    if (DisplayLoading)
                    {
                        if (IsBusy)
                        {
                            UserDialogs.Instance.ShowLoading(BusyMessage, Device.RuntimePlatform == Device.iOS ? MaskType.Black : MaskType.Clear);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            _busyMessage = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Can not display or hide Loading dialog, Error {ex.Message} Stack: {ex.StackTrace}");
                }
                RaisePropertyChanged();
            }
        }

        internal bool DisplayLoading
        {
            get; set;
        }

        internal string BusyMessage
        {
            get
            {
                return _busyMessage;
            }
            set
            {
                _busyMessage = value;
                if (IsBusy && DisplayLoading)
                {
                    UserDialogs.Instance.ShowLoading(BusyMessage, Device.RuntimePlatform == Device.iOS ? MaskType.Black : MaskType.Clear);
                }
            }
        }

        internal INavigationService _navigation { get; set; }
        internal IPageDialogService _dialogService { get; set; }

        private string _Title;
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        public VistaModeloBase()
        {
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
