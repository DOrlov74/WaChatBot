using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class ListMessageData
  {
    [JsonPropertyName("message")]
    public MessageData? Message { get; set; }
  }
}
