using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStyring.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int CategoryId { get; set; }
        //Virtual - Id'er i task fra category
        public virtual Category Category { get; set; }
        public bool Finished { get; set; }

    }
}

