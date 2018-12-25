using System;
using System.Collections.Generic;
using FFImageLoading.Svg.Forms;
using RegistroPropiedad.CustomControls;
using Xamarin.Forms;

namespace RegistroPropiedad.Vistas
{
    public partial class MenuPage : ContentPage
    {
        //public ListView ListView { get { return listView; } }

        //ListView listView;

        //private readonly FFImageLoading.Svg.Forms.SvgImageSourceConverter _imageSourceConverter = new SvgImageSourceConverter();

        public MenuPage()
        {
            InitializeComponent();
            //CreateMenu();
        }

        void CreateMenu()
        {
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Noticias",
                IconSource = "iNoticia.svg",
                TargetType = typeof(NoticiasPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "TodoList",
                IconSource = "iCamera.svg",
                //TargetType = typeof(TodoListPageCS)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Reminders",
                IconSource = "circleok.svg",
                //TargetType = typeof(ReminderPageCS)
            });

            //listView = new ListView
            //{
            //ItemsSource = masterPageItems,
            //ItemTemplate = new DataTemplate(() =>
            //{
            //var grid = new Grid { Padding = new Thickness(5, 10) };
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            //var image = new Image();
            //image.SetBinding(Image.SourceProperty, "IconSource");

            //var xfSource = _imageSourceConverter.ConvertFromInvariantString("") as ImageSource;
            //var xfSource = _imageSourceConverter.ConvertBack(null,null) as ImageSource;

            //    var img = new Image();
            //    img.SetBinding(Image.SourceProperty, "IconSource");

            //    var image = new SvgCachedImage()
            //    {
            //        WidthRequest = 20,
            //        HeightRequest = 20,
            //        HorizontalOptions = LayoutOptions.FillAndExpand,
            //        VerticalOptions = LayoutOptions.FillAndExpand,
            //        //Source = new SvgImageSource(xfSource, 0, 0, true)
            //        Source = new SvgImageSource(img.Source, 0, 0, true)
            //    };

            //    //image.SetBinding(Image.SourceProperty, "IconSource");
            //    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
            //    label.SetBinding(Label.TextProperty, "Title");

            //    grid.Children.Add(image);
            //    //grid.Children.Add(label, 1, 0);

            //    return new ViewCell { View = grid };
            //}),
            //SeparatorVisibility = SeparatorVisibility.None
            //};

            //Icon = "hamburger.png";
            //Title = "Personal Organiser";
            //Content = new StackLayout
            //{
            //    Children = { listView }
            //};
        }
    }
}
