using System.Collections.Generic;

namespace SlackNet.WebApi;

public class ConversationMembersResponse
{
    public IList<string> Members { get; set; } = [];
    public ResponseMetadata ResponseMetadata { get; set; } = new();
}

public class DiscoveryConversationMembersResponse
{
    public IList<DiscoveryMember> Members { get; set; } = [];
    public string Offset { get; set; } = string.Empty;
}