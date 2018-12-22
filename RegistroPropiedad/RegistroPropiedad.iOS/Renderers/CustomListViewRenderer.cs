using System;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomListViewRenderer))]
namespace RegistroPropiedad.iOS.Renderers
{
    public class CustomListViewRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            return cell;
        }
    }
}
