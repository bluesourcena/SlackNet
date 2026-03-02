using System.Threading;
using System.Threading.Tasks;
using Args = System.Collections.Generic.Dictionary<string, object>;

namespace SlackNet.WebApi;

public interface IDiscoveryConversationsApi
{
    /// <summary>
    /// Fetches a conversation's history of messages and events.
    /// </summary>
    /// <remarks>See the <a href="https://api.slack.com/methods/discovery.conversations.history">Slack documentation</a> for more information.</remarks>
    /// <param name="channelId">Conversation ID to fetch history for.</param>
    /// <param name="latestTs">End of time range of messages to include in results.</param>
    /// <param name="oldestTs">Start of time range of messages to include in results.</param>
    /// <param name="limit">The maximum number of items to return. Fewer than the requested number of items may be returned, even if the end of the users list hasn't been reached.</param>
    /// <param name="cursor">
    /// Paginate through collections of data by setting the cursor parameter to a value
    /// returned by a previous request's <see cref="DiscoveryConversationMessagesResponse.Offset"/>.
    /// Default value fetches the first "page" of the collection.
    /// </param>
    /// <param name="cancellationToken"></param>
    Task<DiscoveryConversationMessagesResponse> History(string channelId, string latestTs = null, string oldestTs = null, int limit = 100, string cursor = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve information about a conversation.
    /// </summary>
    /// <remarks>See the <a href="https://api.slack.com/methods/discovery.conversations.info">Slack documentation</a> for more information.</remarks>
    /// <param name="channelId">Conversation ID to learn more about.</param>
    /// <param name="offset">Offset for paging</param>
    /// <param name="cancellationToken"></param>
    Task<DiscoveryConversation> Info(string channelId, string offset = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all channels in a Slack team.
    /// </summary>
    /// <remarks>See the <a href="https://api.slack.com/methods/discovery.conversations.list">Slack documentation</a> for more information.</remarks>
    /// <param name="limit">The maximum number of items to return. Fewer than the requested number of items may be returned, even if the end of the list hasn't been reached. Must be an integer no larger than 1000.</param>
    /// <param name="types">Types of conversations to include. Default is <see cref="ConversationType.All"/>.</param>
    /// <param name="cursor">
    /// Paginate through collections of data by setting the cursor parameter to a value
    /// returned by a previous request's <see cref="DiscoveryConversationListResponse.Offset"/>.
    /// Default value fetches the first "page" of the collection.
    /// </param>
    /// <param name="teamId">encoded team id to list channels in, required if token belongs to org-wide app</param>
    /// <param name="cancellationToken"></param>
    Task<DiscoveryConversationListResponse> List(int limit = 100, ConversationType types = ConversationType.All, string cursor = null, string teamId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve members of a conversation.
    /// </summary>
    /// <remarks>See the <a href="https://api.slack.com/methods/discovery.conversations.members">Slack documentation</a> for more information.</remarks>
    /// <param name="channelId">ID of the conversation to retrieve members for.</param>
    /// <param name="limit">The maximum number of items to return. Fewer than the requested number of items may be returned, even if the end of the users list hasn't been reached.</param>
    /// <param name="cursor">
    /// Paginate through collections of data by setting the cursor parameter to a value
    /// returned by a previous request's <see cref="DiscoveryConversationMembersResponse.Offset"/>.
    /// Default value fetches the first "page" of the collection.
    /// </param>
    /// <param name="cancellationToken"></param>
    Task<DiscoveryConversationMembersResponse> Members(string channelId, int limit = 100, string cursor = null, CancellationToken cancellationToken = default);
}

public class DiscoveryConversationsApi(ISlackApiClient client) : IDiscoveryConversationsApi
{
    public Task<DiscoveryConversationMessagesResponse> History(string channelId, string latestTs = null, string oldestTs = null, int limit = 100, string cursor = null, CancellationToken cancellationToken = default) =>
        client.Get<DiscoveryConversationMessagesResponse>("discovery.conversations.history", new Args
            {
                { "channel", channelId },
                { "latest", latestTs },
                { "limit", limit },
                { "oldest", oldestTs },
                { "offset", cursor }
            }, cancellationToken);

    public async Task<DiscoveryConversation> Info(string channelId, string offset = "", CancellationToken cancellationToken = default) =>
        (await client.Get<DiscoveryConversationResponse>("discovery.conversations.info", new Args
            {
                { "channel", channelId },
                { "offset", offset }
            }, cancellationToken).ConfigureAwait(false))
        .Info[0];

    public Task<DiscoveryConversationListResponse> List(int limit = 100, ConversationType type = ConversationType.All, string cursor = null, string teamId = null, CancellationToken cancellationToken = default) =>
        client.Get<DiscoveryConversationListResponse>("discovery.conversations.list", new Args
            {
                { "limit", limit },
                { "only_public", type == ConversationType.PublicChannel },
                { "only_private", type == ConversationType.PrivateChannel },
                { "only_im", type == ConversationType.Im },
                { "only_mpim", type == ConversationType.Mpim },
                { "only_ext_shared", false },
                { "team", teamId },
                { "offset", cursor }
            }, cancellationToken);

    public Task<DiscoveryConversationMembersResponse> Members(string channelId, int limit = 100, string cursor = null, CancellationToken cancellationToken = default) =>
        client.Get<DiscoveryConversationMembersResponse>("discovery.conversations.members", new Args
            {
                { "channel", channelId },
                { "offset", cursor },
                { "limit", limit }
            }, cancellationToken);
}
