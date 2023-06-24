using DevDynamo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private static List<Project> projects = new();



        static ProjectsController()
        {
            projects.Add(new Project("Superman"));
            projects.Add(new Project("The Flash"));
        }



        public IActionResult Index()
        {
            return View(projects);
        }



        [HttpPost]
        public IActionResult Create()
        {
            var ms = DateTime.Now.Millisecond;
            var p = new Project($"Project {ms}");




            var path = "./WorkflowTemplate/Default.txt";
            var workFlow = System.IO.File.ReadAllText(path);
            p.LoadWorkflowTemplate(workFlow);



            projects.Add(p);



            return RedirectToAction("Index");
        }



        public IActionResult Details(Guid id)
        {
            var p = projects.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }



            return View(p);
        }


    }
}
