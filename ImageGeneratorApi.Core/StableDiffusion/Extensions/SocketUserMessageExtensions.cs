using Discord;

namespace ImageGeneratorApi.Core.StableDiffusion.Extensions;

public static class SocketUserMessageExtensions
{
    /// <summary>
    /// Get all image attachments from this message or the message it references
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static IReadOnlyList<IAttachment> GetMessageImageAttachments(this IUserMessage message)
    {
        // Get all attachments for message
        var attachments = message.Attachments.ToList<IAttachment>();

        // Get all attachments from mentioned message (if any)
        attachments.AddRange(message.ReferencedMessage?.Attachments ?? []);

        // Remove all non image attachments
        attachments.RemoveAll(a => !a.ContentType.StartsWith("image/"));

        return attachments;
    }

    /// <summary>
    /// Get all image attachments from this message or the message it references
    /// </summary>
    /// <param name="message"></param>
    /// <param name="http"></param>
    /// <returns></returns>
    public static async Task<List<IAttachment>?> GetMessageImages(this IUserMessage message, HttpClient http)
    {
        var result = await GetMessageImageAttachments(message)
            .ToAsyncEnumerable()
            // .SelectAwait(async a =>
            // {
            //    //return await a.GetPngStream(http);
            //    Console.WriteLine(a);
            //    IAttachment attachment = (IAttachment)a;
            //    return await attachment.GetPngStream(http);
            // })
            .Where(a => a != null)
            //.Select(a => a!)
            .ToListAsync();

        return result;
    }
}