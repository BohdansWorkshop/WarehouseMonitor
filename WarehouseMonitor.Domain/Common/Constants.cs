namespace WarehouseMonitor.Domain.Constants;

public static class ValidationConstants
{
    public static class Product
    {
        public const int NameMaxLength = 50;
        public const int SkuMaxLength = 30;
        public const int DescriptionMaxLength = 50;
    }

    public static class Warehouse
    {
        public const int NameMaxLength = 100;
        public const int AddressMaxLength = 500;
    }

    public static class ShipmentUnit
    {
        public const int CodeMaxLength = 32;
    }
}