using System.ComponentModel.DataAnnotations;

namespace Persistence.MsSqlEFCore;

public class ProductModel
{
    [Required]
    public string Id { get; set; }

    public string Name { get; set; }
}
