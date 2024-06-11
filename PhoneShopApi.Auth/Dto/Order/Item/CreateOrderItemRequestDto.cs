﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Dto.Order.Item
{
    public class CreateOrderItemRequestDto
    {
        public int OrderId { get; set; }
        public int PhoneOptionId { get; set; }
        public int Quantity { get; set; }
    }
}
