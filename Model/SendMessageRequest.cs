using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class SendMessageRequest
  {
    [JsonPropertyName("chatId")]
    public string? ChatId { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
  }
}
