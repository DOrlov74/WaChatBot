using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class MessageResponseData
  {
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("instanceId")]
    public string? InstanceId { get; set; }
    [JsonPropertyName("data")]
    public MessageData? MessageData { get; set; }
  }
}
