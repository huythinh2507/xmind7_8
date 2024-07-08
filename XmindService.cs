namespace xmind1_project
{
    public class XmindService
    {


        public XmindService()
        {
        }

        public Root CreateDefaultXmind()
        {
            var xmind = new Root("Central Topic");
            xmind.SetHeight(xmind, 2.0);
            xmind.SetWidth(xmind, 5.0);
            xmind.Sheet.Title = "My Mind Map Sheet";
            xmind.Sheet.Description = "This sheet contains my mind map.";
            CreateMainTopic(xmind, new Constants()._defaultTopicNumber);
            return xmind;
        }

        public Root CreateMainTopic(Root xmind, int v)
        {
            var mainTopics = GenerateTopics(xmind, v, new Constants()._MainTopic);
            foreach (var mainTopic in mainTopics)
            {
                xmind.children.Add(mainTopic);
            }
            return xmind;
        }

        public Root CreateFloatingTopic(Root xmind, int v)
        {
            var floatingTopics = GenerateTopics(xmind, v, new Constants()._FloatingTopic);
            foreach (var floatingTopic in floatingTopics)
            {
                xmind.children.Add(floatingTopic);
            }

            return xmind;
        }

        private List<Children> GenerateTopics(Root xmind, int count, string type)
        {
            var constants = new Constants();
            var existingTopicCount = xmind.children.Count;
            for (int i = 0; i < count; i++)
            {
                int topicNumber = existingTopicCount + i + 1; // Calculate the unique topic number
                constants._child = new Children($"{type} {topicNumber}");
                constants._child.SetType(type);
                constants._child.SetHeight(constants._child, 1.0);
                constants._child.SetWidth(constants._child, 3.0);
                constants._child.SetID(i);
                constants._child.SetName(type);
                constants._allChildren.Add(constants._child);
            }
            return constants._allChildren;
        }

        public Root DeleteTopic(Root xmind, List<int> idsToRemove)
        {
            var list = xmind.children;
            list.RemoveAll(child => idsToRemove.Contains(child.ID));
            return xmind;
        }

        public void Connect(Root xmind, int startID, int endID)
        {
            var startNode = xmind.children.Find(i => i.ID == startID);
            var endNode = xmind.children.Find(i => i.ID == endID);
            var relationship = new Relationship("", startNode.ID, endNode.ID);
            AddRelationship(xmind, relationship); // Add the relationship to the list
        }

        public void AddRelationship(Root xmind, Relationship relationship)
        {
            xmind.GetRelationship().Add(relationship);
        }

        public void ChangeRelationShipName(Root xmind, Guid ID, string newName)
        {
            var relationship = xmind.GetRelationship().Find(b => b.id == ID);
            if (relationship != null)
            {
                relationship.title = newName;
            }
        }

        public Children CreateSubTopic(Children main_topic_1, int v)
        {
            var constants = new Constants();
            var existingTopicCount = main_topic_1._subtopic.Count;
            for (int i = 0; i < v; i++)
            {
                int topicNumber = existingTopicCount + i + 1; // Calculate the unique topic number
                constants._child = new Children($"{constants._SubTopic} {topicNumber}");
                constants._child.SetType(constants._SubTopic);
                constants._child.SetHeight(constants._child, 1.0);
                constants._child.SetWidth(constants._child, 3.0);
                constants._child.SetID(i);
                constants._child.SetName(constants._SubTopic);
                main_topic_1._subtopic.Add(constants._child);
            }
            return main_topic_1;
        }
    }
}