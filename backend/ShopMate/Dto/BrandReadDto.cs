#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class BrandReadDto
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<string> Aliases { get; private set; }

        public string? Logo { get; private set; }
    }
}
