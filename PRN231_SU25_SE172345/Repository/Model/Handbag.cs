using System;
using System.Collections.Generic;
using Repository.Model;

namespace Repository;

public partial class Handbag
{
    public int Id { get; set; }

    public string ModelName { get; set; } = null!;

    public string Material { get; set; } = null!;

    public double Price { get; set; }

    public int Stock { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;
}
