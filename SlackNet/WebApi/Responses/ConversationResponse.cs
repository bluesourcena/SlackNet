using System.Collections.Generic;

namespace SlackNet.WebApi;

class ConversationResponse
{
    public Conversation Channel { get; set; }
}

class DiscoveryConversationResponse
{
    public List<DiscoveryConversation> Info { get; set; } = [];
}