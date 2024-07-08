namespace xmind1_project
{
    public class BaseNode
    {

        public BaseNode()
        {
            Id = Guid.NewGuid();

        }
        public BaseNode(string title)
        {
            this.Title = title;
            Position = new Position(0, 0);
            Id = Guid.NewGuid();
        }

        private Guid Id { get; set; }
        public Position Position { get; set; }
        public string Title { get; set; } = string.Empty;
        private double Width { get; set; }
        private double Height { get; set; }
        private List<Relationship> RelationshipList { get; set; } = new List<Relationship>();
        public Guid GetId()
        {
            return Id;
        }

        public List<Relationship> GetRelationship()
        {
            return RelationshipList;
        }

        public Position GetPosition()
        {
            return Position;
        }
        public string GetTitle()
        {
            return Title;
        }

        public double GetHeight()
        {
            return Height;
        }
        public double GetWidth()
        {
            return Width;
        }

        public void SetHeight(Children topic, double x)
        {
            topic.Height = x;
        }

        public void SetHeight(Root _root, double x)
        {
            _root.Height = x;
        }
        public void SetWidth(Children topic, double x)
        {
            topic.Width = x;
        }

        public void SetWidth(Root _root, double x)
        {
            _root.Width = x;
        }


    }
}