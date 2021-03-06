﻿#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.


namespace ShopMate.Dto
{
    public class UserReadDto
    {
        public string Name { get; internal set; }

        public string Email { get; internal set; }

        public string Phone { get; internal set; }

        public decimal MoneySpent { get; internal set; }
    }
}
