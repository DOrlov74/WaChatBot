using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class Instance
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }
    [JsonPropertyName("webhook_url")]
    public string? Webhook_url { get; set; }
    [JsonPropertyName("is_trial")]
    public int Is_trial { get; set; }
  }
}
