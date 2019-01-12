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
        public SelectDisp(int id, bool selected, Object obj)
        {
            this.Id = id;
            this.Selected = selected;
            this.Obj = obj;
        }

        public int Id { get; set; }
        public bool Selected { get; set; }
        public Object Obj { get; set; }

    }
}