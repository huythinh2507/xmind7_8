using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using System.Net.WebSockets;

namespace xmind1_project
{
    public class XmindService
    {


        public XmindService()
        {
        }

        public static Root CreateDefaultXmind()
        {
            var xmind = new Root("Central Topic");
            Root.SetHeight(xmind, 2.0);
            Root.SetWidth(xmind, 5.0);
            xmind.Sheet.Title = "My Mind Map Sheet";
            xmind.Sheet.Description = "This sheet contains my mind map.";
            CreateMainTopic(xmind, new Constants()._defaultTopicNumber);
            return xmind;
        }

        public static Root CreateMainTopic(Root xmind, int v)
        {
            var mainTopics = GenerateTopics(xmind, v, new Constants()._MainTopic);
            foreach (var mainTopic in mainTopics)
            {
                xmind.children.Add(mainTopic);
            }
            return xmind;
        }

        public static Root CreateFloatingTopic(Root xmind, int v)
        {
            var floatingTopics = GenerateTopics(xmind, v, new Constants()._FloatingTopic);
            foreach (var floatingTopic in floatingTopics)
            {
                xmind.children.Add(floatingTopic);
            }

            return xmind;
        }

        private static List<Children> GenerateTopics(Root xmind, int count, string type)
        {
            var constants = new Constants();
            var existingTopicCount = xmind.children.Count;

            for (int i = 0; i < count; i++)
            {
                int topicNumber = existingTopicCount + i + 1; 
                var name = $"{type} {topicNumber}"; 

                var topic = new Children();
                topic.SetType(type);
                Children.SetHeight(topic, 1.0);
                Children.SetWidth(topic, 3.0);
                topic.SetID(i);
                topic.SetName(name);

                constants._allChildren.Add(topic);
            }

            return constants._allChildren;
        }


        public static Root DeleteTopic(Root xmind, List<int> idsToRemove)
        {
            var list = xmind.children;
            list.RemoveAll(child => idsToRemove.Contains(child.ID));
            return xmind;
        }

        public static void Connect(Root xmind, int startID, int endID)
        {
            // Find the start and end nodes by their IDs
            var startNode = xmind.children.Find(i => i.ID == startID);
            var endNode = xmind.children.Find(i => i.ID == endID);

            // Create a relationship between the nodes (if both nodes are found)
            var relationship = startNode?.ID != null && endNode?.ID != null
                ? new Relationship("", startNode.ID, endNode.ID)
                : null;
            if (relationship != null)
            {
                AddRelationship(xmind, relationship);
            }
        }

        public static void AddRelationship(Root xmind, Relationship relationship)
        {
            xmind.RelationshipList.Add(relationship);
        }

        public static void ChangeRelationShipName(Root xmind, Guid ID, string newName)
        {
            var relationship = xmind.GetRelationship().Find(b => b.ID == ID);
            if (relationship != null)
            {
                relationship.title = newName;
            }
        }

        public static Children CreateSubTopic(Children main_topic_1, int v)
        {
            var constants = new Constants();
            var existingTopicCount = main_topic_1.subtopic.Count;
            for (int i = 0; i < v; i++)
            {
                int topicNumber = existingTopicCount + i + 1; // Calculate the unique topic number
                constants._child = new Children();
                var name = $"{constants._SubTopic} {topicNumber}";
                constants._child.SetType(constants._SubTopic);
                Children.SetHeight(constants._child, 1.0);
                Children.SetWidth(constants._child, 3.0);
                constants._child.SetID(i);
                constants._child.SetName(name);
                main_topic_1.subtopic.Add(constants._child);
            }
            return main_topic_1;
        }

        public static void ChangeRootName(Root xmind, string newname)
        {
            xmind.Title = newname;
        }

        public static void ChangeTopicName(Children children, string newname)
        {
            children.Title = newname;
        }

        public static void DeleteRelationship(Root xmind, Guid relationship_id1)
        {
            var List_of_rela = xmind.RelationshipList;

            var rela_to_be_deleted = List_of_rela.Find(b => b.ID.Equals(relationship_id1));

            if (rela_to_be_deleted != null)
            {
                List_of_rela.Remove(rela_to_be_deleted);
            }
        }

    }
}