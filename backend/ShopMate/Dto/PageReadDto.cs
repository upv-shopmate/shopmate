#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class PageReadDto<T>
    {
        public ICollection<T> Items { get; }

        public int? NextPage { get; }

        public PageReadDto(ICollection<T> items, int? nextPage)
        {
            Items = items;
            NextPage = nextPage;
        }
    }
}
