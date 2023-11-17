using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WaChatBot.Model;

namespace WaChatBot.Services
{
  internal class BotService
  {
    private APIClient _apiClient;

    public BotService(string apiKey) 
    {
      _apiClient = new APIClient(apiKey);
    }

    public async Task<bool> Start()
    {
      if (await _apiClient.GetInstanceId())
      {
        return true;
      }
      return false;
    }

    public async Task<bool> SendMessage(string clientNum, string message)
    {
      if (await _apiClient.SendMessage($"{clientNum}@c.us", message))
      {
        return true;
      }
      return false;
    }

    public async Task<string> GetSelection(string clientNum, List<string> items)
    {
      List<MessageData> messages = new();
      int count = 0;
      do {
        await Task.Delay(2 * 1000);
        messages = await _apiClient.FetchNewMessages($"{clientNum}@c.us");
        ++count;
      } while (messages.Count==0 && count<100);
      foreach (MessageData message in messages)
      {
        foreach (string item in items)
        {
          if (Regex.IsMatch(message.Body, item, RegexOptions.IgnoreCase))
          {
            return item;
          }
        }
        
      }
      return "";
    }
  }
}
