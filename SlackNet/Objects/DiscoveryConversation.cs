using System.Collections.Generic;

namespace SlackNet;

public class DiscoveryConversation
{
    public string Id { get; set; }
    public string Name { get; set; }
    public long Created { get; set; }
    public int MemberCount { get; set; }
    public bool IsGeneral { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsIm { get; set; }
    public bool IsMpim { get; set; }
    public bool IsFile { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsArchived { get; set; }
    public string Creator { get; set; }
    public bool IsMoved { get; set; }
    public bool IsShared { get; set; }
    public string NameNormalized { get; set; }
    public bool IsGlobalShared { get; set; }
    public bool IsOrgShared { get; set; }
    public bool IsOrgMandatory { get; set; }
    public bool IsOrgDefault { get; set; }
    public List<string> PreviousNames { get; set; } = [];
    public Purpose Purpose { get; set; }
    public Topic Topic { get; set; }
    public Retention Retention { get; set; }
    public Shared Shared { get; set; }
    public string ConversationHostId { get; set; }
    public bool HasGuests { get; set; }
}

public class Retention
{
    public string Type { get; set; }
    public int Duration { get; set; }
}

public class Shared
{
    public List<string> SharedTeamIds { get; set; } = [];
    public List<string> ConnectedTeamIds { get; set; } = [];
    public List<string> ConnectedLimitedTeamIds { get; set; } = [];
}
