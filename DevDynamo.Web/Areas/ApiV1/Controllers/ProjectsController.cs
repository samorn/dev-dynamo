using DevDynamo.Model;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
   // [Area("ApiV1")]
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDb db;
        public ProjectsController(AppDb db) { 
         this.db = db;
        }


     [HttpGet]
        public ActionResult<IEnumerable<ProjectResponse>> GetAll() {
            var items = db.Projects.ToList();
            return  items.ConvertAll(x => ProjectResponse.FromModel(x));
        }


        
        [HttpGet("{id}")]
        public ActionResult<ProjectResponse> GetById(Guid id)
        {
            var item = db.Projects.SingleOrDefault(x => x.Id == id);
            if (item is null) { 
                return  NotFound(new ProblemDetails() {  Title="Projecs is not found"});
            }

            return ProjectResponse.FromModel(item);
        }
        [HttpPost]
        public ActionResult<ProjectResponse> Create(CreateProjectReques Reques)
        {
           
            var p = new Project(Reques.Name);




            var path = $"./WorkflowTemplate/{Reques.Template}.txt";
            if (!System.IO.File.Exists(path)) {
                return BadRequest(new ProblemDetails { Title = $"Template {Reques.Template} not found" });
            }
            var workFlow = System.IO.File.ReadAllText(path);
            p.LoadWorkflowTemplate(workFlow);



            db.Projects.Add(p);
            db.SaveChanges();
            var res = ProjectResponse.FromModel(p);
            return CreatedAtAction(nameof(GetById), new { id=p.Id},res);


        }

    }

 
}
