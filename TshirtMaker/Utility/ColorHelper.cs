namespace TshirtMaker.Utility
{
    public static class ColorHelper
    {
        private static readonly Dictionary<string, string> ColorMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "#FFFFFF", "White" },
            { "#000000", "Black" },
            { "#FF0000", "Red" },
            { "#00FF00", "Green" },
            { "#0000FF", "Blue" },
            { "#FFFF00", "Yellow" },
            { "#FF00FF", "Magenta" },
            { "#00FFFF", "Cyan" },
            { "#FFA500", "Orange" },
            { "#800080", "Purple" },
            { "#FFC0CB", "Pink" },
            { "#A52A2A", "Brown" },
            { "#808080", "Gray" },
            { "#C0C0C0", "Silver" },
            { "#FFD700", "Gold" },
            { "#4B0082", "Indigo" },
            { "#EE82EE", "Violet" },
            { "#F0E68C", "Khaki" },
            { "#E6E6FA", "Lavender" },
            { "#FFA07A", "Light Salmon" },
            { "#20B2AA", "Light Sea Green" },
            { "#87CEEB", "Sky Blue" },
            { "#778899", "Light Slate Gray" },
            { "#B0C4DE", "Light Steel Blue" },
            { "#FFFFE0", "Light Yellow" },
            { "#00FF7F", "Spring Green" },
            { "#4682B4", "Steel Blue" },
            { "#D2B48C", "Tan" },
            { "#008080", "Teal" },
            { "#FF6347", "Tomato" },
            { "#40E0D0", "Turquoise" },
            { "#F5DEB3", "Wheat" },
            { "#F5F5DC", "Beige" },
            { "#FFE4C4", "Bisque" },
            { "#FAEBD7", "Antique White" },
            { "#F0FFFF", "Azure" },
            { "#F5F5F5", "White Smoke" },
            { "#FFFAF0", "Floral White" },
            { "#F8F8FF", "Ghost White" },
            { "#FFFACD", "Lemon Chiffon" },
            { "#FFF0F5", "Lavender Blush" },
            { "#2F4F4F", "Dark Slate Gray" },
            { "#696969", "Dim Gray" },
            { "#1E90FF", "Dodger Blue" },
            { "#B22222", "Fire Brick" },
            { "#228B22", "Forest Green" },
            { "#DAA520", "Goldenrod" },
            { "#ADFF2F", "Green Yellow" },
            { "#FF69B4", "Hot Pink" },
            { "#CD5C5C", "Indian Red" },
            { "#7CFC00", "Lawn Green" },
            { "#90EE90", "Light Green" },
            { "#FFB6C1", "Light Pink" },
            { "#32CD32", "Lime Green" },
            { "#66CDAA", "Medium Aquamarine" },
            { "#0000CD", "Medium Blue" },
            { "#BA55D3", "Medium Orchid" },
            { "#9370DB", "Medium Purple" },
            { "#3CB371", "Medium Sea Green" },
            { "#7B68EE", "Medium Slate Blue" },
            { "#00FA9A", "Medium Spring Green" },
            { "#48D1CC", "Medium Turquoise" },
            { "#191970", "Midnight Blue" },
            { "#FFE4E1", "Misty Rose" },
            { "#6B8E23", "Olive Drab" },
            { "#FF4500", "Orange Red" },
            { "#DA70D6", "Orchid" },
            { "#DB7093", "Pale Violet Red" },
            { "#FFDAB9", "Peach Puff" },
            { "#CD853F", "Peru" },
            { "#DDA0DD", "Plum" },
            { "#B0E0E6", "Powder Blue" },
            { "#BC8F8F", "Rosy Brown" },
            { "#4169E1", "Royal Blue" },
            { "#8B4513", "Saddle Brown" },
            { "#FA8072", "Salmon" },
            { "#F4A460", "Sandy Brown" },
            { "#2E8B57", "Sea Green" },
            { "#FFF5EE", "Seashell" },
            { "#A0522D", "Sienna" },
            { "#6A5ACD", "Slate Blue" },
            { "#708090", "Slate Gray" },
            { "#FFFAFA", "Snow" },
            { "#D2691E", "Chocolate" },
            { "#FF7F50", "Coral" },
            { "#6495ED", "Cornflower Blue" },
            { "#DC143C", "Crimson" },
            { "#8B008B", "Dark Magenta" },
            { "#556B2F", "Dark Olive Green" },
            { "#FF8C00", "Dark Orange" },
            { "#9932CC", "Dark Orchid" },
            { "#8B0000", "Dark Red" },
            { "#E9967A", "Dark Salmon" },
            { "#8FBC8F", "Dark Sea Green" },
            { "#483D8B", "Dark Slate Blue" },
            { "#00CED1", "Dark Cyan" },
            { "#9400D3", "Dark Violet" },
            { "#FF1493", "Deep Pink" },
            { "#00BFFF", "Deep Sky Blue" }
        };

        public static string HexToColorName(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return "Unknown";

            hex = hex.Trim().ToUpperInvariant();

            if (!hex.StartsWith("#"))
                hex = "#" + hex;

            if (ColorMap.TryGetValue(hex, out var colorName))
                return colorName;

            if (hex.Length == 7)
            {
                var r = Convert.ToInt32(hex.Substring(1, 2), 16);
                var g = Convert.ToInt32(hex.Substring(3, 2), 16);
                var b = Convert.ToInt32(hex.Substring(5, 2), 16);

                var closestColor = ColorMap
                    .OrderBy(kvp =>
                    {
                        var targetHex = kvp.Key;
                        var targetR = Convert.ToInt32(targetHex.Substring(1, 2), 16);
                        var targetG = Convert.ToInt32(targetHex.Substring(3, 2), 16);
                        var targetB = Convert.ToInt32(targetHex.Substring(5, 2), 16);

                        return Math.Sqrt(
                            Math.Pow(r - targetR, 2) +
                            Math.Pow(g - targetG, 2) +
                            Math.Pow(b - targetB, 2)
                        );
                    })
                    .FirstOrDefault();

                return closestColor.Value ?? "Custom";
            }

            return "Custom";
        }
    }
}
