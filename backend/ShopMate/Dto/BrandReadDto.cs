#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class BrandReadDto
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public ICollection<string> Aliases { get; internal set; }

        public string? Logo { get; internal set; }
    }
}
