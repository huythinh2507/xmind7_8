namespace xmind1_project

{
    public class Xmind_Test
    {
        public static Root GetDefaultXmind()
        {
            var xmind = XmindService.CreateDefaultXmind();
            return xmind;
        }

        private static void CreateRelationship(int startID, int endID)
        {
            var xmind = GetDefaultXmind();
            XmindService.Connect(xmind, startID, endID);
        }

        [Fact]
        public void Test_Create_Ini_Xmind()
        {
            var xmind = GetDefaultXmind();
            Assert.NotNull(xmind);
        }

        [Fact]
        public void Test_Ini_Xmind_Default_Children_Are_4()
        {
            var xmind = GetDefaultXmind();
            var number_of_default_children = xmind.GetChildren().Count;
            Assert.Equal(4, number_of_default_children);
        }

        [Fact]
        public void Test_Create_Floating_Topics()
        {
            var xmind = GetDefaultXmind();
            XmindService.CreateFloatingTopic(xmind, 1);

            // Assert that there is one floating topic
            Assert.Equal(1, xmind.Children.Count(topic => topic.Type == new Constants()._FloatingTopic));
        }

        [Fact]
        public void Test_Create_3_Main_Topics()
        {
            //create 4 default topics + 3 more main topics
            var number_of_topics_to_be_created = 3;
            var xmind = GetDefaultXmind();
            XmindService.CreateMainTopic(xmind, number_of_topics_to_be_created);
            //count number of total topics
            var number_of_total_topics = xmind.GetChildren().Count;
            //shoudl be 4 + 3
            Assert.Equal(number_of_topics_to_be_created + new Constants()._defaultTopicNumber, number_of_total_topics);
        }

        [Fact]
        public void Test_Title_Of_A_Specific_Topic()
        {
            //create 4 default topics + 3 more main topics
            var number_of_topics_to_be_created = 3;
            var xmind = GetDefaultXmind();
            XmindService.CreateMainTopic(xmind, number_of_topics_to_be_created);


            //get name of the 7th topic
            var nameof_7th_topic = xmind.GetChildren()[6].Name;

            Assert.Equal($"{new Constants()._MainTopic} {7}", nameof_7th_topic);
        }

        [Fact]
        public void Test_Delete_Topic()
        {
            List<int> topic_ids_to_be_removed = [3];
            var xmind = GetDefaultXmind();
            XmindService.DeleteTopic(xmind, topic_ids_to_be_removed); // Remove the square brackets around 3

            var deleted_topic = xmind.Children.Find(i => i.ID == 3);
            Assert.Null(deleted_topic);
        }

        [Fact]
        public void Test_Delete_Multi_Topics()
        {
            List<int> topic_ids_to_be_removed = [1, 3]; // Corrected the list initialization
            var xmind = GetDefaultXmind();
            XmindService.DeleteTopic(xmind, topic_ids_to_be_removed);

            var remainingTopics = xmind.Children.FindAll(topic => topic_ids_to_be_removed.Contains(topic.ID));
            Assert.Empty(remainingTopics);
        }

        [Fact]
        public void Test_Change_Root_Name_Function()
        {
            var newname = "new name";
            var xmind = GetDefaultXmind();
            XmindService.ChangeRootName(xmind, newname);

            
            Assert.Equal(xmind.Title, newname);
        }

        [Fact]
        public void Test_Change_Topic_Name_Function()
        {
            var newname = "new name";
            var xmind = GetDefaultXmind();
            XmindService.ChangeTopicName(xmind.GetChildren()[0], newname);


            Assert.Equal(xmind.GetChildren()[0].Title, newname);
        }

        [Fact]
        public void DoesRelationshipExist()
        {
            var xmind = GetDefaultXmind();
            var startID = 1;
            var endID = 2;
            //create rela
            XmindService.Connect(xmind, startID, endID);

            Assert.Equal(startID, xmind.GetRelationship()[0].StartID);
            Assert.Equal(endID, xmind.GetRelationship()[0].EndID);
        }

        [Fact]
        public void DeleteRelationship()
        {
            
            var xmind = GetDefaultXmind();
            var startID = 1;
            var endID = 2;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //get the first relationship
            var relationship_id = xmind.RelationshipList[0].ID;
            //delete it
            XmindService.DeleteRelationship(xmind, relationship_id);

            var deletedRelationship = xmind.RelationshipList.Find(b  => b.ID == relationship_id);
            Assert.Null(deletedRelationship);
        }

        [Fact]
        public void DoesRelationshipTitleExist()
        {
            var xmind = GetDefaultXmind();
            var startID = 1;
            var endID = 2;
            //create rela
            XmindService.Connect(xmind, startID, endID);

            Assert.Equal("", xmind.GetRelationship()[0].title);
        }

        [Fact]
        public void IsTitlechangeable()
        {
            var xmind = GetDefaultXmind();
            var startID = 1;
            var endID = 2;
            //create rela
            XmindService.Connect(xmind, startID, endID);
            //set new name
            var newname = "new name";
            Guid ID = xmind.GetRelationship()[0].ID;

            XmindService.ChangeRelationShipName(xmind, ID, newname);

            Assert.Equal(newname, xmind.GetRelationship()[0].title);
        }

        [Fact]
        public void TestAddSubtopicFunction()
        {
            var xmind = GetDefaultXmind();
            var main_topic_1 = xmind.GetChildren()[0];
            XmindService.CreateSubTopic(main_topic_1, 3);

            Assert.NotNull(main_topic_1);
        }

        [Fact]
        public void DoesSubTopicHave3Children()
        {
            var xmind = GetDefaultXmind();
            var main_topic_1 = xmind.GetChildren()[0];
            XmindService.CreateSubTopic(main_topic_1, 3);
            //count children of subtopic
            var count = main_topic_1.GetChildren().Count;
            Assert.Equal(3, count);
        }

        [Fact]
        public void Sheet_Title_ShouldBeSet()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();

            sheet.Title = "My Mind Map Sheet";

            Assert.Equal("My Mind Map Sheet", sheet.Title);
        }

        [Fact]
        public void Test_Change_Sheet_Title()
        {
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();
            var newsheetname = "new sheet";
            xmind.ChangeSheetTitle(newsheetname);

            Assert.Equal(sheet.Title, newsheetname);
        }

        [Fact]
        public void Test_Topic_Order_After_Addition()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var expectedOrder = new List<string?>() { "Main Topic 1", "Main Topic 2", "Main Topic 3", "Main Topic 4", "Main Topic 5" };
            // Act
            XmindService.CreateMainTopic(xmind, 1); 
                                                   
            var actualOrder = xmind.GetChildren().Select(topic => topic.Name).ToList();
            Assert.Equal(expectedOrder, actualOrder);
        }


        [Fact]
        public void Export_ShouldSetIsExportedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();

            // Act
            Root.Export(sheet);

            // Assert
            Assert.True(sheet.IsExported);
        }

        [Fact]
        public void Save_ShouldSetIsSavedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();

            // Act
            Root.Save(sheet);

            // Assert
            Assert.True(sheet.IsSaved);
        }

    }
}