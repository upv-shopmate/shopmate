#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class Category
    {
        public int Id { get; private set; }

        /// <summary>
        /// The name of this category.
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; internal set; }

        /// <summary>
        /// The products tagged with this category.
        /// </summary>
        public ICollection<Product> Products { get; internal set; } = new List<Product>();
    }
}
