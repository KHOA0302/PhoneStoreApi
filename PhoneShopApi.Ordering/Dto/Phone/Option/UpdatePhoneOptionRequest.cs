﻿namespace PhoneShopApi.Ordering.Dto.Phone.Option
{
    public record UpdatePhoneOptionRequest(
        int BuiltInStorageCapacity,
        string BuiltInStorageUnit,
        string PhoneColorName,
        decimal Price,
        int Quantity);
}
