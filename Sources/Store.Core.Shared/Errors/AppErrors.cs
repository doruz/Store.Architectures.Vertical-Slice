namespace Store.Core.Shared;

public static class AppErrors
{
    public static async Task<T> EnsureIsNotNull<T>(this Task<T?> item, string id)
        => (await item).EnsureIsNotNull(id);

    public static T EnsureIsNotNull<T>(this T? item, string id)
        => item ?? throw AppError.NotFound("not_found", id);
}