
namespace xmind1_project
{
    public class Root 
    {

        public List<Children> children { get; internal set; }
        public Sheet Sheet { get; set; }
        public IEnumerable<char>? Title { get; internal set; }
        public double Height { get; internal set; }
        public double Width { get; internal set; }
        public List<Relationship> RelationshipList { get; private set; } = [];

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
        public List<Relationship> GetRelationship()
        {
            return RelationshipList;
        }
        public static void SetHeight(Children topic, double x)
        {
            topic.Height = x;
        }

        public static void SetHeight(Root _root, double x)
        {
            _root.Height = x;
        }
        public static void SetWidth(Children topic, double x)
        {
            topic.Width = x;
        }

        public static void SetWidth(Root _root, double x)
        {
            _root.Width = x;
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