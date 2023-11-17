using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WaChatBot.Model;

namespace WaChatBot.Services
{
  internal class APIClient: APIClientBase
  {
    private int _lastSent;
    public APIClient(string apiKey = "", string baseUrl = "https://waapi.app/api/v1") : base(apiKey, baseUrl)
    {
    }

    public async Task<bool> GetInstanceId()
    {
      var authUrl = "instances";
      var response = await SendRequestAsync<InstancesResponse>(HttpMethod.Get, authUrl);
      if (response != null && response.Instances.Count > 0)
      { 
        _apiInstance = response.Instances[0].Id.ToString();
        return true;
      }
      return false;
    }

    public async Task<List<MessageData>> FetchNewMessages(string clientNum)
    {
      var sendMessageUrl = $"instances/{_apiInstance}/client/action/fetch-messages";
      ListMessageRequest request = new()
      {
        ChatId = clientNum
      };
      List<MessageData> result = new();
      var response = await SendPostRequestAsync<ListMessagesResponse, ListMessageRequest>(sendMessageUrl, request);
      if (response != null && response.Status == "success" && response.Data != null)
      {
        foreach (ListMessageData message in response.Data.MessageData)
        {
          if (message.Message.Type == "chat" && message.Message.Timestamp > _lastSent)
          {
            result.Add(message.Message);
          }
        }
      }
      return result;
    }
    public async Task<bool> SendMessage(string clientNum, string message)
    {
      var sendMessageUrl = $"instances/{_apiInstance}/client/action/send-message";
      SendMessageRequest request = new()
      {
        ChatId = clientNum,
        Message = message
      };
      var response = await SendPostRequestAsync<SendMessageResponse, SendMessageRequest>(sendMessageUrl, request);
      if (response != null && response.Status == "success")
      {
        _lastSent = response.Data.MessageData.Timestamp;
        return true;
      }
      return false;
    }
    private async Task<T?> SendRequestAsync<T>(HttpMethod httpMethod, string entity)
    {
      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      var url = BuildUrl(entity: entity);
      using (var httpRequest = CreateHttpRequest(verb: httpMethod, url: url))
      using (var httpResponse = await client.SendAsync(httpRequest))
      {
        httpResponse.EnsureSuccessStatusCode();
        string apiResponse = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(apiResponse);
      }
    }

    private async Task<T?> SendPostRequestAsync<T, U>(string entity, U request)
    {
      var client = new HttpClient();
      var url = BuildUrl(entity: entity);
      using (var httpRequest = CreateHttpRequest(verb: HttpMethod.Post, url: url))
      {
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var options = new JsonSerializerOptions()
        { WriteIndented = true };
        string data = JsonSerializer.Serialize((U)request, options);
        using (var httpContent = new StringContent(data, Encoding.UTF8, "application/json"))
        {
          httpRequest.Content = httpContent;
          using (var httpResponse = await client.SendAsync(httpRequest))
          {
            httpResponse.EnsureSuccessStatusCode();
            string apiResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(apiResponse);
          }
        }
      }
    }

    private HttpRequestMessage CreateHttpRequest(HttpMethod verb, string url)
    {
      var request = new HttpRequestMessage(verb, url);
      if (!string.IsNullOrWhiteSpace(_apiKey))
      {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
      }
      return request;
    }


    private string BuildUrl(string entity, string? queryString = null)
    {
      var url = _baseUrl;
      if (!String.IsNullOrEmpty(entity))
      {
        url = String.Format("{0}/{1}", url, entity);
      }
      if (queryString != null)
      {
        url += "?" + queryString;
      }
      return url;
    }
  }
}
