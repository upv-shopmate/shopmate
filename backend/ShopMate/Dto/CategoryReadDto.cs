#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class CategoryReadDto
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public CategoryReadDto? Parent { get; internal set; }
    }
}