namespace xmind1_project
{
    public class Children : BaseNode
    {
        public List<Children> _subtopic { get; internal set; }
        public Children()
        { }

        public Children(string title) : base(title)
        {
            _subtopic = new List<Children> { };
        }

        public int ID { get; internal set; }
        public string Type { get; internal set; }
        public string Name { get; private set; }

        public void SetID(int i)
        {
            ID = i + 1;
        }

        public void SetName(string type)
        {
            Name = type;
        }

        public void SetType(string v)
        {
            Type = v;
        }

        public List<Children> GetChildren()
        {
            return _subtopic;
        }
    }
}