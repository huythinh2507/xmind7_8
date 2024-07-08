namespace xmind1_project
{
    public class Root : BaseNode
    {

        public List<Children> children { get; internal set; }
        public Sheet Sheet { get; set; }


        public BaseNode basenode;

        public Root(string title)
        {
            children = new List<Children> { };
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

        public bool Export(Sheet sheet)
        {
            sheet.IsExported = true;
            return true;
        }
        public bool Save(Sheet sheet)
        {
            sheet.IsSaved = true;
            return true;
        }

    }
}