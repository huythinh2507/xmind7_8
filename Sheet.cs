namespace xmind1_project
{
    public class Sheet
    {
        public string Title { get; set; } = "NULL";
        public string Description { get; set; } = "NULL";
        public bool IsExported { get; internal set; }

        public bool IsSaved { get; internal set; }
    }
}