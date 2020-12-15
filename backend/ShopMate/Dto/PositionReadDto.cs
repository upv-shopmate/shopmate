#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class PositionReadDto
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
    }
}