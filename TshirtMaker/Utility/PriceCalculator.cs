using TshirtMaker.Models.Enums;

namespace TshirtMaker.Utility
{
    public static class PriceCalculator
    {
        private static readonly Dictionary<ClothingType, decimal> BaseClothingPrices = new()
        {
            { ClothingType.TShirt, 15.00m },
            { ClothingType.Hoodie, 35.00m },
            { ClothingType.Sweat, 28.00m },
            { ClothingType.Tank, 12.00m },
            { ClothingType.LongSleeve, 20.00m },
            { ClothingType.Jacket, 45.00m },
            { ClothingType.Hat, 18.00m },
            { ClothingType.ToteBag, 10.00m }
        };

        private static readonly Dictionary<Material, decimal> MaterialPriceMultipliers = new()
        {
            { Material.HeavyCotton, 1.0m },
            { Material.Polyester, 0.9m },
            { Material.CottonPolyesterBlend, 0.95m },
            { Material.Linen, 1.3m },
            { Material.Wool, 1.5m },
            { Material.Fleece, 1.2m }
        };

        public static decimal CalculatePrice(ClothingType clothingType, Material material)
        {
            if (!BaseClothingPrices.TryGetValue(clothingType, out var basePrice))
            {
                basePrice = 15.00m;
            }

            if (!MaterialPriceMultipliers.TryGetValue(material, out var multiplier))
            {
                multiplier = 1.0m;
            }

            return Math.Round(basePrice * multiplier, 2);
        }
    }
}
