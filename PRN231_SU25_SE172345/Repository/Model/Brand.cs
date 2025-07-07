using System;
using System.Collections.Generic;

namespace Repository.Model;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Handbag> Handbags { get; set; } = new List<Handbag>();
}
