using Xmind_Library;

namespace xmind1_project
{
    public class Xmind_Test
    {
        //init default xmind
        public static Root GetDefaultXmind()
        {
            var xmind = XmindService.InitDefaultXmind();
            return xmind;
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
            //should be 4 children
            Assert.Equal(4, number_of_default_children);
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
            Assert.Equal(numberOfNewFloatingTopics, xmind.Topics.Count(topic => topic.Type == Constants.FloatingTopic));
        }

        [Fact]
        public void Test_Create3MainTopics()
        {
            //create 4 default topics + 3 more main topics
            var numberOfTopicsToBeCreated = 3;
            var xmind = GetDefaultXmind();
            var children = XmindService.GenerateTopics(xmind, xmind.ID, "Main Topic" ,numberOfTopicsToBeCreated);
            XmindService.SetChildForRoot(xmind, children);
            //count number of total topics
            var totalTopics = xmind.GetChildren().Count;
            //shoudl be 4 + 3
            Assert.Equal(numberOfTopicsToBeCreated + Constants.DefaultTopicNumber, totalTopics);
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

            Assert.Equal($"{ Constants.MainTopic} {7}", nameof_7th_topic);
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
        public void Test_ChangeTopicParent()
        {
            var xmind = GetDefaultXmind();
            var newParentID = xmind.GetChildren()[1].ID;
            XmindService.ChangeTopicParent(xmind.GetChildren()[0], newParentID);
            Assert.Equal(xmind.GetChildren()[0].ParentID, newParentID);
        }

        [Fact]
        public void Does_RelationshipExist()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);

            Assert.Equal(startID, xmind.GetRelationship()[0].StartID);
            Assert.Equal(endID, xmind.GetRelationship()[0].EndID);
        }

        [Fact]
        public void DeleteRelationship()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //get the first relationship
            var relationship_id = xmind.Relationships[0].ID;
            //delete it
            XmindService.DeleteRelationship(xmind, relationship_id);

            var deletedRelationship = xmind.Relationships.Find(b  => b.ID == relationship_id);
            Assert.Null(deletedRelationship);
        }

        [Fact]
        public void DoesRelationshipTitleExist()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);

            Assert.Equal("", xmind.GetRelationship()[0].Title);
        }

        [Fact]
        public void IsTitlechangeable()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //set new name
            var newname = "new name";
            Guid ID = xmind.GetRelationship()[0].ID;

            XmindService.ChangeRelationShipName(xmind, ID, newname);

            Assert.Equal(newname, xmind.GetRelationship()[0].Title);
        }

        [Fact]
        public void Test_ChangeRelStartNode()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //set new name
            Guid relID = xmind.GetRelationship()[0].ID;
            var newStartID = xmind.Topics[2].ID;
            XmindService.ChangeRelStartNode(xmind, relID, newStartID);

            Assert.Equal(newStartID, xmind.GetRelationship()[0].StartID);
        }

        [Fact]
        public void Test_ChangeRelEndNode()
        {
            var xmind = GetDefaultXmind();
            var startID = xmind.Topics[0].ID;
            var endID = xmind.Topics[1].ID;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //set new name
            Guid relID = xmind.GetRelationship()[0].ID;
            var newEndID = xmind.Topics[2].ID;
            XmindService.ChangeRelEndNode(xmind, relID, newEndID);

            Assert.Equal(newEndID, xmind.GetRelationship()[0].EndID);
        }

        [Fact]
        public void Test_Add3ChildrenForMainTopic()
        {
            var xmind = GetDefaultXmind();
            var main_topic_1 = xmind.GetChildren()[0];
            var parentID = main_topic_1.ID;
            var children = XmindService.GenerateTopics(xmind, parentID, "Sub Topic", 3);
            XmindService.SetChildForTopic(main_topic_1, children);
            //count children of subtopic
            var count = main_topic_1.GetChildren().Count;
            Assert.Equal(3, count);
        }

        

        [Fact]
        public void SheetTitleShouldBeSet()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];

            Assert.Equal(Constants.SheetTitle, sheet.Title);
        }


        [Fact]
        public void Test_ChangeSheetTitle()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];
            var newsheetname = "new sheet";
            XmindService.ChangeSheetTitle(sheet, newsheetname);

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
        public void Test_TopicOrderAfterAddition()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var expectedOrder = new List<string>() { "Main Topic 1", "Main Topic 2", "Main Topic 3", "Main Topic 4", "Main Topic 5" };
            // Act
            var children = XmindService.GenerateTopics(xmind, xmind.ID, "Main Topic" ,1); 
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
        public void Save_ShouldSetIsSavedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.Sheets[0];

            // Act
            XmindService.Save(sheet);

            // Assert
            Assert.True(sheet.IsSaved);
        }

    }
}