﻿using Microsoft.Extensions.Logging;
using OffLogs.Client.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OffLogs.Client
{
    public class OfflogsHttpClient: IOfflogsHttpClient
    {
        private readonly string _apiUrl = "https://api.offlogs.com/log/add";

        private readonly HttpClient _client;
        private readonly string _apiToken;

        public OfflogsHttpClient(string apiToken)
        {
            _apiToken = apiToken;
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(3);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task SendLog(
            LogLevel level,
            string message,
            IDictionary<string, string> properties
        )
        {
            await SendLogAsync(level, message, null, properties);
        }

        public async Task SendLogAsync(
            LogLevel level,
            string message,
            ICollection<string> traces = null,
            IDictionary<string, string> properties = null
        )
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            var logDto = new LogDto(level, message);
            if (properties != null)
                logDto.AddProperties(properties);
            if (traces != null)
                logDto.AddTraces(traces);
            await SendLogsAsync(new List<LogDto>() { logDto });
        }

        public async Task SendLogsAsync(
            ICollection<LogDto> logs
        )
        {
            if (logs == null)
                throw new ArgumentNullException(nameof(logs));
            if (logs.Count == 0)
                return;

            var logsList = new LogsListDto(logs);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(logsList);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
