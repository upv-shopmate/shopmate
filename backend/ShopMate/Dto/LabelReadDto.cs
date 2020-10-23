#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class LabelReadDto
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}
