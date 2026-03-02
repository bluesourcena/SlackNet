using System.Collections.Generic;

namespace SlackNet.WebApi;

public class ConversationListResponse
{
    public IList<Conversation> Channels { get; set; } = [];
    public ResponseMetadata ResponseMetadata { get; set; } = new();
}

public class DiscoveryConversationListResponse
{
    public IList<Conversation> Channels { get; set; } = [];
    public string Offset { get; set; } = string.Empty;
}