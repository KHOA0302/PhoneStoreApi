﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Auth.Dto.Order
{
    public class CreateOrderRequestDto
    {
        public string UserId { get; set; } = string.Empty;
        public int PaymentId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
    }
}
