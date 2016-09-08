using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStyring.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //virtual - liste af tasks, som man kan bruge categorys id til 
        public virtual List<Task> Tasks { get; set; }
    }
}