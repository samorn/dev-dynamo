using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class CreateProjectReques
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string Template { get; set; } = null!;
    }
}
