using DevDynamo.Model;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class ChangeTicketStatusResponse
    {
        public int Id { get; set; }
        [StringLength(50)] public string Status { get; set; } = null!;
    }
}
