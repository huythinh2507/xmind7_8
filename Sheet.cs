namespace xmind1_project
{
    public class Sheet
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsExported { get; internal set; }

        public bool IsSaved { get; internal set; }
    }
}