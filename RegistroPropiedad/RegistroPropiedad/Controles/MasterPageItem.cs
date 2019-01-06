using System;
namespace RegistroPropiedad.CustomControls
{
    public class MasterPageItem
    {
        public MasterPageItem()
        {
            Action = ActionType.OpenPage;
        }

        public string Title { get; set; }

        public string IconSource { get; set; }

        public ActionType Action { get; set; }

        public Type TargetType { get; set; }

        public string CommandName { get; set; }
    }

    public enum ActionType
    {
        OpenPage = 1,
        Command = 2,
        CommandView = 3
    }
}
