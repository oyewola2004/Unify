using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unify.UNIFY.Model.Entities;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Category : AuditableEntity
    {
        public string Name {get; set;}
        public string Description {get; set;}
        public ICollection<Post> Post {get; set;}
    }
}