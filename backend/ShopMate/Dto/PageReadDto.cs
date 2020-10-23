#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class PageReadDto<T>
    {
        public ICollection<T> Items { get; private set; }

        public int? NextPage { get; private set; }

        public PageReadDto(ICollection<T> items, int? nextPage)
        {
            Items = items;
            NextPage = nextPage;
        }
    }
}
