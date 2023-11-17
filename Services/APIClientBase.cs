using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WaChatBot.Services
{
  internal abstract class APIClientBase
  {
    protected readonly string _baseUrl;
    protected readonly string _apiKey;
    public string _apiInstance = "";
    public APIClientBase(string apiKey, string baseUrl)
    {
      _baseUrl = baseUrl;
      _apiKey = apiKey;
    }
  }
}
