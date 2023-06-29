using DevDynamo.Model;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    public class TicketsController : ControllerBase
    {
        private readonly AppDb db;
        public TicketsController(AppDb db)
        {
            this.db = db;
        }
        //
        [HttpPut]
        public ActionResult<TicketsResponse> ChangeTicketStatus(ChangeTicketStatusResponse Reques)
        {
            
            if (Reques.Status =="") throw new InvalidOperationException("Status not found.");

            var T = new Ticket(Reques.Id,Reques.Status);
                
            var item = db.Tickets.SingleOrDefault(x => x.Id == Reques.Id);
            if (item is null)
            {
                return NotFound(new ProblemDetails() { Title = "Invalid next status" });
            }
           
            item.Status = Reques.Status;
            db.SaveChanges();
            var res = TicketsResponse.FromModel(item);

            return res;


        }
    }
}
