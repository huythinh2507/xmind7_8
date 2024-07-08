namespace xmind1_project
{
    public class Root : BaseNode
    {

        public List<Children> children { get; internal set; }
        public Sheet Sheet { get; set; }


        public BaseNode basenode { get; internal set; }

        public Root(string title)
        {
            children = [];
            Sheet = new Sheet();
        }

        public List<Children> GetChildren()
        {
            return children;
        }

        public Sheet GetSheet()
        {
            return Sheet;
        }

        public void ChangeSheetTitle(string newsheetname)
        {
            var sheet = GetSheet();
            sheet.Title = newsheetname;
        }

        public static bool Export(Sheet sheet)
        {
            sheet.IsExported = true;
            return true;
        }
        public static bool Save(Sheet sheet)
        {
            sheet.IsSaved = true;
            return true;
        }

    }
}