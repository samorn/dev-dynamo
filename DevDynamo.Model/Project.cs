using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Model
{
    public class Project
    {
        public Project(string name) { 
            Id = Guid.NewGuid();
            Name = name;
            Tickets = new HashSet<Ticket>();
            WorkflowSteps = new HashSet<WorkflowStep>();
        }
        public Guid Id { get; set; }
        [StringLength(100)] public string Name { get; set; } = null!; 
        public string? Description { get; set; }

        // virtual => Enable EF lazy-loading(on-demand)
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; }
        public void LoadWorkflowTemplateBK(string template) 
        {
            if (WorkflowSteps.Any()) throw new InvalidOperationException("Workflow alrady setup");
            var lines = template.Split('\r','\n', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (lines.Count == 0) throw new InvalidOperationException("Template is Empty");
            if (lines[0] != "stateDiagram") throw new InvalidOperationException("Invalid template");
            foreach ( var line in lines.Skip(1))
            {

                var data = line.Split(new string[] {"-->",":"," "}, StringSplitOptions.RemoveEmptyEntries);
                var step = new WorkflowStep();

                if(data.Length >= 1) step.FromStats = data[0];
                if(data.Length >= 2) step.ToStatus = data[1];
                if (data.Length >= 3) step.Action = data[2];

                WorkflowSteps.Add(step);

            }


            //stateDiagram
            //[*] --> ToDo : Create Ticket
            //ToDo-- > Doing : Developer Starts
            //Doing-- > ToDo : Reset
            //Doing --> ReadyToTest : Developer Finishes
            //ReadyToTest-- > Done : Test Passed
            //ReadyToTest-- > Reopen : Test Failed
            //Reopen-- > Doing : Developer Reopens
            //Done-- > [*] : Close Ticket
        }

        /// <summary>
        /// Recreate WorkflowSTeps from stateDiagram template
        /// </summary>
        /// <param name="template"></param>
        public void LoadWorkflowTemplate(string template)
        {
            if (WorkflowSteps.Any()) throw new InvalidOperationException("Workflow already setup.");



            var lines = template.Replace("-->", ":").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (lines.Count == 0) throw new InvalidOperationException("Template is empty");
            if (lines[0].Trim() != "stateDiagram") throw new InvalidOperationException("Invalid template");



            foreach (var line in lines.Skip(1))
            {
                var data = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
                if (data.Length < 2) continue;



                var step = new WorkflowStep();
                step.FromStats = data[0].Trim();
                step.ToStatus = data[1].Trim();
                if (data.Length >= 3) step.Action = data[2].Trim();



                WorkflowSteps.Add(step);
            }
        }
    }
}