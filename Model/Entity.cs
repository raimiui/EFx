using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.Model
{
    public class Entity
    {
        public virtual int Id { get; set; }
        public virtual bool IsNew
        {
            get
            {
                return Id < 1;
            }
        }
    }
}
