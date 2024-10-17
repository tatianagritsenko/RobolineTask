using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RobolineTestTask.Database;

public class ProductCategory
{
    [Range(1, int.MaxValue, ErrorMessage = "The value must be a positive number")]
    [DefaultValue(0)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [DefaultValue(null)]
    public string? Description { get; set; }
}
