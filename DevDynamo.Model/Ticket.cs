using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevDynamo.Model
{
    public class Ticket
    {
        public Ticket(int id,  string Status)
        {
            Id = Id;
            Status = Status;
        }

        public Ticket() { }
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        //public string Name { get; set; }
        public string? Description { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = null!;


    }
}
