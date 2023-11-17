﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaChatBot.Model
{
  internal class ListMessagesResponse
  {
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("data")]
    public ListMessageRootData? Data { get; set; }
  }
}
