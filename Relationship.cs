namespace xmind1_project
{
    public class Relationship
    {
        public Guid ID;
        public int StartID;
        public int EndID;
        public string title;

        public Relationship(string title, int startId, int endId)
        {
            ID = Guid.NewGuid();
            this.title = title;
            this.EndID = endId;
            this.StartID = startId;
        }
    }
}