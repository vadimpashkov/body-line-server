﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
 using Schedule.Configuration;

 namespace WebApp.Identity
{
    public class ReCaptchaValidator
    {
        private const string ValidationUrl = "https://www.google.com/recaptcha/api/siteverify";

        private readonly IHttpClientFactory _clientFactory;
        private readonly string _secret;

        public ReCaptchaValidator(IHttpClientFactory clientFactory, IOptions<CaptchaConfiguration> config)
        {
            _clientFactory = clientFactory;
            _secret = config.Value.PrivateKey;
        }

        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secret),
                new KeyValuePair<string, string>("response", token)
            });

            using var client = _clientFactory.CreateClient();
            var res = await client.PostAsync(ValidationUrl, content);
            if (res.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException(res.ReasonPhrase);

            var response = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(response, new { success = false }).success;
        }
    }
}
