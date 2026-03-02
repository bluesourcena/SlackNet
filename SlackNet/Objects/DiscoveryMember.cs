using System;
using Newtonsoft.Json;

namespace SlackNet;

/// <summary>
/// Represents a member in a Discovery API conversation response.
/// Distinct from the standard conversation members response which returns string IDs.
/// </summary>
public class DiscoveryMember
{
    public string Id { get; set; }
    public bool IsExternal { get; set; }
    public int DateJoined { get; set; }
    public int DateLeft { get; set; }
    public string Team { get; set; }

    [JsonIgnore]
    public DateTime DateJoinedDate => DateTimeOffset.FromUnixTimeSeconds(DateJoined).DateTime;

    [JsonIgnore]
    public DateTime DateLeftDate => DateLeft == 0 ? DateTime.MinValue : DateTimeOffset.FromUnixTimeSeconds(DateLeft).DateTime;
}
