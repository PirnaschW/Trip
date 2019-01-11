using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class SelectDisp
    {
        public SelectDisp() { }
        public SelectDisp(int id, string name, bool selected, Object obj)
        {
            this.Id = id;
            this.Name = name;
            this.Selected = selected;
            this.Obj = obj;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public Object Obj { get; set; }

    }
}