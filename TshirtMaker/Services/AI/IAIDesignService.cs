using TshirtMaker.Models.Enums;

namespace TshirtMaker.Services.AI
{
    public interface IAIDesignService
    {
        string? ErrorMessage { get; }
        Task<List<string>> GenerateInitialDesignsAsync(string prompt, string? negativePrompt, StylePresetType? style, Guid userId, CancellationToken cancellationToken = default);
        Task<string> FinalizeDesignAsync(string selectedDesignUrl, string clothingImageUrl, Guid userId, string color, CancellationToken cancellationToken = default);
    }
}
