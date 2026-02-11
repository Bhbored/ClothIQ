using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.PublicData
{
    public static class PublicPresets
    {
        public static readonly List<MaterialPreview> MaterialPresets = new()
        {
            new MaterialPreview
            {
                Title = "Heavy Cotton",
                Material = Material.HeavyCotton.ToString(),
                PreviewImageUrl = "public/materialPresets/Heavy_Cotton.jpg"
            },
            new MaterialPreview
            {
                Title = "Polyester",
                Material = Material.Polyester.ToString(),
                PreviewImageUrl = "public/materialPresets/Polyester.jpg"
            },
            new MaterialPreview
            {
                Title = "Cotton Polyester Blend",
                Material = Material.CottonPolyesterBlend.ToString(),
                PreviewImageUrl = "public/materialPresets/Cotton Polyester Blend.jpg"
            },
            new MaterialPreview
            {
                Title = "Linen",
                Material = Material.Linen.ToString(),
                PreviewImageUrl = "public/materialPresets/Linen.jpg"
            },
            new MaterialPreview
            {
                Title = "Wool",
                Material = Material.Wool.ToString(),
                PreviewImageUrl = "public/materialPresets/Wool.jpg"
            },
            new MaterialPreview
            {
                Title = "Fleece",
                Material = Material.Fleece.ToString(),
                PreviewImageUrl = "public/materialPresets/Fleece.jpg"
            }
        };


        public static readonly List<StylePresetPreview> StylePresetPreviews = new()
        {
            new StylePresetPreview
            {
                Title = "Cyberpunk",
                StylePreset = StylePresetType.Cyberpunk,
                PreviewImageUrl = "public/stylePresets/Cyberpunk.jpg"
            },
            new StylePresetPreview
            {
                Title = "Anime",
                StylePreset = StylePresetType.Anime,
                PreviewImageUrl = "https://images.unsplash.com/photo-1578632767115-351597cf2477?w=800&h=600&fit=crop"
            },
            new StylePresetPreview
            {
                Title = "Minimalism",
                StylePreset = StylePresetType.Minimalism,
                PreviewImageUrl = "https://images.unsplash.com/photo-1557683316-973673baf926?w=800&h=600&fit=crop"
            },
            new StylePresetPreview
            {
                Title = "Oil Paint",
                StylePreset = StylePresetType.OilPaint,
                PreviewImageUrl = "https://images.unsplash.com/photo-1541961017774-22349e4a1262?w=800&h=600&fit=crop"
            },
            new StylePresetPreview
            {
                Title = "Watercolor",
                StylePreset = StylePresetType.Watercolor,
                PreviewImageUrl = "public/stylePresets/Watercolor.jpg"
            },
            new StylePresetPreview
            {
                Title = "Vintage",
                StylePreset = StylePresetType.Vintage,
                PreviewImageUrl = "https://images.unsplash.com/photo-1513475382585-d06e58bcb0e0?w=800&h=600&fit=crop"
            },
            new StylePresetPreview
            {
                Title = "Gothic",
                StylePreset = StylePresetType.Gothic,
                PreviewImageUrl = "public/stylePresets/Gothic.jpg"
            },
            new StylePresetPreview
            {
                Title = "Streetwear",
                StylePreset = StylePresetType.Streetwear,
                PreviewImageUrl = "public/stylePresets/Streetwear.jpg"
            },
            new StylePresetPreview
            {
                Title = "Abstract",
                StylePreset = StylePresetType.Abstract,
                PreviewImageUrl = "https://images.unsplash.com/photo-1557672172-298e090bd0f1?w=800&h=600&fit=crop"
            },
            new StylePresetPreview
            {
                Title = "Photorealistic",
                StylePreset = StylePresetType.Photorealistic,
                PreviewImageUrl = "public/stylePresets/Photorealistic.jpg"
            },
            new StylePresetPreview
            {
                Title = "Pixel Art",
                StylePreset = StylePresetType.PixelArt,
                PreviewImageUrl = "public/stylePresets/Pixel_Art.jpg"
            },
            new StylePresetPreview
            {
                Title = "Graffiti",
                StylePreset = StylePresetType.Graffiti,
                PreviewImageUrl = "public/stylePresets/Graffiti.jpg"
            }
        };


        public static Design RemixedDesign = new();
        public static readonly List<string> Avatars = [
            "https://api.dicebear.com/7.x/avataaars/svg?seed=default",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=John",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Jane",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Alex",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Emma",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Mike",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Sophia",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=David",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Lisa",
            "https://api.dicebear.com/7.x/avataaars/svg?seed=Chris"
        ];
    }
}
