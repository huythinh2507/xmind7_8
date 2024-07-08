namespace xmind1_project
{
    public class Xmind_Test
    {
        public Root GetDefaultXmind()
        {
            var xmind = new XmindService().CreateDefaultXmind();
            return xmind;
        }

        private Root AddRelationship(int startID, int endID)
        {
            var xmind = GetDefaultXmind();
            new XmindService().Connect(xmind, startID, endID);
            return xmind;
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
            new XmindService().CreateFloatingTopic(xmind, 1);

            // Assert that there is one floating topic
            Assert.Equal(1, xmind.children.Count(topic => topic.Type == new Constants()._FloatingTopic));
        }

        [Fact]
        public void Test_Create_3_Main_Topics()
        {
            //create 4 default topics + 3 more main topics
            var number_of_topics_to_be_created = 3;
            var xmind = GetDefaultXmind();
            new XmindService().CreateMainTopic(xmind, number_of_topics_to_be_created);
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
            new XmindService().CreateMainTopic(xmind, number_of_topics_to_be_created);


            //get name of the 7th topic
            var nameof_7th_topic = xmind.GetChildren()[6].Title;

            Assert.Equal($"{new Constants()._MainTopic} {7}", nameof_7th_topic);
        }

        [Fact]
        public void Test_Delete_Topic()
        {
            List<int> topic_ids_to_be_removed = new List<int> { 3 };
            var xmind = GetDefaultXmind();
            new XmindService().DeleteTopic(xmind, topic_ids_to_be_removed); // Remove the square brackets around 3

            var deleted_topic = xmind.children.Find(i => i.ID == 3);
            Assert.Null(deleted_topic);
        }

        [Fact]
        public void Test_Delete_Multi_Topics()
        {
            List<int> topic_ids_to_be_removed = new List<int> { 1, 3 }; // Corrected the list initialization
            var xmind = GetDefaultXmind();
            new XmindService().DeleteTopic(xmind, topic_ids_to_be_removed);

            var remainingTopics = xmind.children.FindAll(topic => topic_ids_to_be_removed.Contains(topic.ID));
            Assert.Equal(0, remainingTopics.Count);
        }


        [Fact]
        public void DoesRelationshipExist()
        {
            var startID = 1;
            var endID = 2;
            var xmind = AddRelationship(startID, endID);

            Assert.Equal(startID, xmind.GetRelationship()[0].StartID);
            Assert.Equal(endID, xmind.GetRelationship()[0].EndID);
        }

        [Fact]
        public void DoesRelationshipTitleExist()
        {
            var startID = 1;
            var endID = 2;
            var xmind = AddRelationship(startID, endID);

            Assert.Equal("", xmind.GetRelationship()[0].title);
        }

        [Fact]
        public void IsTitlechangeable()
        {
            var startID = 1;
            var endID = 2;
            var xmind = AddRelationship(startID, endID);
            var newname = "new name";
            Guid ID = xmind.GetRelationship()[0].id;

            new XmindService().ChangeRelationShipName(xmind, ID, newname);

            Assert.Equal(newname, xmind.GetRelationship()[0].title);
        }

        [Fact]
        public void TestAddSubtopicFunction()
        {
            var xmind = GetDefaultXmind();
            var main_topic_1 = xmind.GetChildren()[0];
            new XmindService().CreateSubTopic(main_topic_1, 3);

            Assert.NotNull(main_topic_1);
        }

        [Fact]
        public void DoesSubTopicHave3Children()
        {
            var xmind = GetDefaultXmind();
            var main_topic_1 = xmind.GetChildren()[0];
            new XmindService().CreateSubTopic(main_topic_1, 3);
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
            var expectedOrder = new List<string>() { "Main Topic 1", "Main Topic 2", "Main Topic 3", "Main Topic 4", "Main Topic 5" };

            // Act
            new XmindService().CreateMainTopic(xmind, 1); // Add one main topic

            // Assert
            var actualOrder = xmind.GetChildren().Select(topic => topic.Title).ToList();
            Assert.Equal(expectedOrder, actualOrder);
        }

        [Fact]
        public void Export_ShouldSetIsExportedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();

            // Act
            xmind.Export(sheet);

            // Assert
            Assert.True(sheet.IsExported);
        }

        [Fact]
        public void Export_ShouldSetIsSavedToTrue()
        {
            // Arrange
            var xmind = GetDefaultXmind();
            var sheet = xmind.GetSheet();

            // Act
            xmind.Save(sheet);

            // Assert
            Assert.True(sheet.IsSaved);
        }
        [Fact]
        public void Test()
        {
            // Arrange
            var a = 1;
            var b = 2;
            var c = 3;
            var d = a + b;

            // Assert
            Assert.Equal(c, d);
        }

    }
}