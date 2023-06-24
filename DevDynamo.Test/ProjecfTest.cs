using DevDynamo.Model;

namespace DevDynamo.Test
{
    public class ProjecfTest
    {
        public class Genaral {
            [Fact]
            public void NewProject()
            {
                var project = new Project(name:"test");
                Assert.NotNull(project);
                Assert.Equal("test", project.Name);
                Assert.Empty(project.WorkflowSteps);
                Assert.Empty(project.Tickets);
            }
        }
        public class LoadWorkflowTemplate
        {
            [Fact]
            public void Test1()
            {
                var p = new Project("test");
                var template = @"stateDiagram
                [*] --> ToDo : Create Ticket
                ToDo --> Doing : Developer Starts
                Doing --> ToDo : Reset
                Doing --> ReadyToTest : Developer Finishes
                ReadyToTest --> Done : Test Passed
                ReadyToTest --> Reopen : Test Failed
                Reopen --> Doing : Developer Reopens
                Done --> [*] : Close Ticket";
                p.LoadWorkflowTemplate(template);
                Assert.Equal(8, p.WorkflowSteps.Count());
            }

        }

    }
}