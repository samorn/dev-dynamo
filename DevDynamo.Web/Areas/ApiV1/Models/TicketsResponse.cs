using DevDynamo.Model;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class TicketsResponse
    {
        public int Id { get; set; }
        [StringLength(100)] public string Title { get; set; }
        public string? Description { get; set; }
        [StringLength(50)] public string Status { get; set; } = null!;
        public static TicketsResponse FromModel(Ticket T)
        {
            return new TicketsResponse
            {
                Id = T.Id,
                Title = T.Title,
                Description = T.Description,
                Status = T.Status
            };

        }


    }
}
