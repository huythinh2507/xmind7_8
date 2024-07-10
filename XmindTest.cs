using Xmind_Library;

namespace xmind1_project
{
    public class XmindTest
    {
        //init default xmind
        public static Root GetDefaultXmind()
        {
            return XmindService.InitDefaultXmind();
        }

        [Fact]
        public void Test_IniXmind()
        {
            var xmind = GetDefaultXmind();
            Assert.NotNull(xmind);
        }

        [Fact]
        public void Test_XmindDefaultChildren()
        {
            var xmind = GetDefaultXmind();
            var number_of_default_children = xmind.GetChildren().Count;
            Assert.Equal(4, number_of_default_children); //should be 4 children
        }

        [Fact]
        public void Test_CreateFloatingTopics()
        {
            var xmind = GetDefaultXmind();
            var parentID = xmind.ID;
            var numberOfNewFloatingTopics = 10;
            var child = XmindService.GenerateTopics(xmind, parentID, "Floating Topic", numberOfNewFloatingTopics);
            XmindService.SetChildForRoot(xmind, child);
            // Assert that there is one floating topic
            Assert.Equal(numberOfNewFloatingTopics, xmind.Topics.Count(topic => topic.Type == XmindConstants.FloatingTopic));
        }

        [Fact]
        public void Test_Create3MainTopics()
        {
            //create 4 default topics + 3 more main topics
            var numberOfTopicsToBeCreated = 3;
            var xmind = GetDefaultXmind();
            var children = XmindService.GenerateTopics(xmind, xmind.ID, "Main Topic", numberOfTopicsToBeCreated);
            XmindService.SetChildForRoot(xmind, children);
            //count number of total topics
            var totalTopics = xmind.GetChildren().Count;
            //shoudl be 4 + 3
            Assert.Equal(numberOfTopicsToBeCreated + XmindConstants.DefaultTopicNumber, totalTopics);
        }

        [Fact]
        public void Test_TitleOfASpecificTopic()
        {
            //create 4 default topics + 3 more main topics
            var number_of_topics_to_be_created = 3;
            var xmind = GetDefaultXmind();
            var children = XmindService.GenerateTopics(xmind, xmind.ID, "Main Topic", number_of_topics_to_be_created);
            XmindService.SetChildForRoot(xmind, children);

            //get name of the 7th topic
            var nameof_7th_topic = xmind.GetChildren()[6].Title;

            Assert.Equal($"{XmindConstants.MainTopic} {7}", nameof_7th_topic);
        }

        [Fact]
        public void Test_Delete1Topic()
        {
            var xmind = GetDefaultXmind();
            var childID = xmind.Topics[0].ID;
            List<Guid> topic_ids_to_be_removed = [childID];

            XmindService.DeleteTopic(xmind, topic_ids_to_be_removed);

            var deleted_topic = xmind.Topics.Find(i => i.ID == childID);
            Assert.Null(deleted_topic);
        }

        [Fact]
        public void Test_DeleteMultiTopics()
        {
            var xmind = GetDefaultXmind();
            var child1ID = xmind.Topics[0].ID;
            var child2ID = xmind.Topics[1].ID;
            List<Guid> topic_ids_to_be_removed = [child1ID, child2ID]; // Corrected the list initialization

            XmindService.DeleteTopic(xmind, topic_ids_to_be_removed);

            var remainingTopics = xmind.Topics.FindAll(topic => topic_ids_to_be_removed.Contains(topic.ID));
            Assert.Empty(remainingTopics);
        }

        [Fact]
        public void Test_ChangeRootName()
        {
            var newname = "new name";
            var xmind = GetDefaultXmind();
            XmindService.ChangeRootName(xmind, newname);


            Assert.Equal(xmind.Title, newname);
        }

        [Fact]
        public void Test_ChangeTopicName()
        {
            var newname = "new name";
            var xmind = GetDefaultXmind();
            XmindService.ChangeTopicName(xmind.GetChildren()[0], newname);

            Assert.Equal(xmind.GetChildren()[0].Title, newname);
        }

        [Fact]
        public void Test_CreateRelationships()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            var endNode2 = xmind.Topics[2];
            //create 1st rel
            XmindService.CreateRelationship(xmind, startNode, endNode);
            //create 2nd rel
            XmindService.CreateRelationship(xmind, startNode, endNode2);
            //assert there are 2 relationship
            Assert.Equal(2, xmind.Relationships.Count);
            //1st rel
            Assert.Equal(startNode.ID, xmind.GetRelationship()[0].StartID);
            Assert.Equal(endNode.ID, xmind.GetRelationship()[0].EndID);
            //2nd rel
            Assert.Equal(startNode.ID, xmind.GetRelationship()[1].StartID);
            Assert.Equal(endNode2.ID, xmind.GetRelationship()[1].EndID);
        }

        [Fact]
        public void DeleteRelationship()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            XmindService.CreateRelationship(xmind, startNode, endNode);
            //get the first relationship
            var rel_id = xmind.Relationships[0].ID;
            //delete it
            XmindService.DeleteRelationship(xmind, rel_id);

            var deletedRelationship = xmind.Relationships.Find(b => b.ID == rel_id);
            Assert.Null(deletedRelationship);
        }

        [Fact]
        public void DoesRelationshipTitleExist()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            XmindService.CreateRelationship(xmind, startNode, endNode);

            Assert.Equal("", xmind.GetRelationship()[0].Title);
        }

        [Fact]
        public void IsTitlechangeable()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            XmindService.CreateRelationship(xmind, startNode, endNode);
            //set new name
            var newname = "new name";
            Guid ID = xmind.GetRelationship()[0].ID;

            XmindService.ChangeRelationshipName(xmind, ID, newname);

            Assert.Equal(newname, xmind.GetRelationship()[0].Title);
        }

        [Fact]
        public void Test_ChangeRelStartNode()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            XmindService.CreateRelationship(xmind, startNode, endNode);
            Guid relID = xmind.GetRelationship()[0].ID;

            var newStartNode = xmind.Topics[2];
            XmindService.ChangeRelStartNode(xmind, relID, newStartNode);

            Assert.Equal(newStartNode.ID, xmind.GetRelationship()[0].StartID);
            Assert.Equal(newStartNode, xmind.GetRelationship()[0].StartNode);
        }

        [Fact]
        public void Test_ChangeRelEndNode()
        {
            var xmind = GetDefaultXmind();
            var startNode = xmind.Topics[0];
            var endNode = xmind.Topics[1];
            XmindService.CreateRelationship(xmind, startNode, endNode);
            Guid relID = xmind.GetRelationship()[0].ID;
            var newEndNode = xmind.Topics[2];
            XmindService.ChangeRelEndNode(xmind, relID, newEndNode);

            Assert.Equal(newEndNode.ID, xmind.GetRelationship()[0].EndID);
            Assert.Equal(newEndNode, xmind.GetRelationship()[0].EndNode);
        }

        [Fact]
        public void Test_Add3ChildrenForMainTopic()
        {
            //arrange
            var xmind = GetDefaultXmind();
            var mainTopic = xmind.GetChildren()[0];
            var parentID = mainTopic.ID;
            var noSubTopics = 3;
            //act
            var children = XmindService.GenerateTopics(xmind, parentID, "Sub Topic", noSubTopics);
            XmindService.CreateTopic(xmind, "Sub Topic");
            XmindService.SetChildForTopic(mainTopic, children);
            //count children of subtopic
            var count = mainTopic.GetChildren().Count;
            //assert 
            Assert.Equal(noSubTopics, count);
        }

        [Fact]
        public void Test_ChangeTopicParent()
        {
            //init xmind
            var xmind = GetDefaultXmind();
            //get parent
            var parent = xmind.GetChildren()[0];
            //get new parent
            var newParent = xmind.GetChildren()[1];
            var oldparentID = parent.ID;
            var number_of_subtopics = 3;
            //generate subtopics
            var children = XmindService.GenerateTopics(xmind, oldparentID, "Sub Topic", number_of_subtopics);
            //add newly generated subtopics to old parent
            XmindService.SetChildForTopic(parent, children);
            //get first child of old parent
            var subject = parent.GetChildren()[0];
            //change parent
            XmindService.ChangeTopicParent(subject, newParent, parent);
            //assert
            Assert.Equal(newParent.ID, subject.ParentID);
        }

        [Fact]
        public void SheetTitleShouldBeSet()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];
            Assert.Equal(XmindConstants.SheetTitle, sheet.Title);
        }

        [Fact]
        public void Test_ChangeSheetTitle()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];
            var newsheetname = "new sheet";
            XmindService.RenameSheet(sheet, newsheetname);
            Assert.Equal(sheet.Title, newsheetname);
        }

        [Fact]
        public void AddSheet_ShouldAddSheetToRoot()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var initialSheetCount = xmind.Sheets.Count;

            // Act
            XmindService.SetSheet(xmind, "Test Sheet", "Test Description");

            // Assert
            Assert.Equal(initialSheetCount + 1, xmind.Sheets.Count);
            var addedSheet = xmind.Sheets[^1];
            Assert.Equal("Test Sheet", addedSheet.Title);
            Assert.Equal("Test Description", addedSheet.Description);
            Assert.False(addedSheet.IsExported);
            Assert.False(addedSheet.IsSaved);
        }

        [Fact]
        public void DeleteSheet()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            // Act
            XmindService.SetSheet(xmind, "Test Sheet", "Test Description");
            var addedSheet = xmind.Sheets[^1];
            //confirm created new sheet
            Assert.NotNull(addedSheet);
            //delete it
            XmindService.DeleteSheet(xmind, addedSheet.ID);
            var deletedSheet = xmind.Sheets.Find(x => x.ID == addedSheet.ID);
            //confirm deletion
            Assert.Null(deletedSheet);
        }

        [Fact]
        public void Test_TopicOrderAfterAddition()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var expectedOrder = new List<string>() { "Main Topic 1", "Main Topic 2", "Main Topic 3", "Main Topic 4", "Main Topic 5" };
            // Act
            var children = XmindService.GenerateTopics(xmind, xmind.ID, "Main Topic", 1);
            XmindService.SetChildForRoot(xmind, children);

            var actualOrder = xmind.GetChildren().Select(topic => topic.Title).ToList();
            Assert.Equal(expectedOrder, actualOrder);
        }


        [Fact]
        public void Export_ShouldSetIsExportedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];

            // Act
            XmindService.Export(sheet);

            // Assert
            Assert.True(sheet.IsExported);
        }

        [Fact]
        public void SaveSheet_ShouldSetIsSavedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];

            // Act
            XmindService.SaveSheet(sheet);

            // Assert
            Assert.True(sheet.IsSaved);
        }

        [Fact]
        public void Test_DuplicateSheet()
        {
            // Arrange
            var xmind = GetDefaultXmind();  // Assuming this method initializes a default Root object
            var originalSheet = xmind.Sheets[0];  // Assuming there is at least one sheet in xmind

            // Act
            var duplicatedRoot = XmindService.DuplicateSheet(xmind, originalSheet);

            // Assert
            Assert.NotNull(duplicatedRoot);
            Assert.NotEqual(xmind.ID, duplicatedRoot.ID); // IDs should not be the same for duplicated root

            // Assert Root properties
            Assert.Equal(xmind.Title, duplicatedRoot.Title);
            Assert.Equal(xmind.Height, duplicatedRoot.Height);
            Assert.Equal(xmind.Width, duplicatedRoot.Width);
            Assert.Equal(xmind.Topics.Count, duplicatedRoot.Topics.Count); // Ensure topics are copied

            // Assert Sheet properties
            Assert.Single(duplicatedRoot.Sheets);  // Ensure only one sheet is duplicated
            Assert.Equal(originalSheet.Title, duplicatedRoot.Sheets[0].Title);
            Assert.Equal(originalSheet.Description, duplicatedRoot.Sheets[0].Description);
            Assert.Equal(originalSheet.IsExported, duplicatedRoot.Sheets[0].IsExported);
            Assert.Equal(originalSheet.IsSaved, duplicatedRoot.Sheets[0].IsSaved);

            // Optionally, assert individual topic properties if needed
            for (int i = 0; i < xmind.Topics.Count; i++)
            {
                Assert.Equal(xmind.Topics[i].Title, duplicatedRoot.Topics[i].Title);
                Assert.NotEqual(xmind.Topics[i].ID, duplicatedRoot.Topics[i].ID); // IDs should be different
                // Add more assertions as per your Topic structure
            }
        }
    }
}