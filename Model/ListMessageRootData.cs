using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class ListMessageRootData
  {
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("instanceId")]
    public string? InstanceId { get; set; }
    [JsonPropertyName("data")]
    public List<ListMessageData>? MessageData { get; set; }
  }
}
