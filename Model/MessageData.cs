using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class MessageData
  {
    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("from")]
    public string? From { get; set; }
    [JsonPropertyName("to")]
    public string? To { get; set; }
    [JsonPropertyName("body")]
    public string? Body { get; set; }
  }
}
