using ASL.Utility.SMSManager.Interfaces;
using ASL.Utility.SMSManager.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASL.Utility.SMSManager.Services
{
    public class SSLCommerzSMSSender : ISMSSender
    {
        public async Task<string> SendSMSEnglishAsync(List<SMSContent> contents, SMSGatewaySettings settings)
        {
            try
            {
                if (contents.Count == 0) return string.Empty;

                using var client = new HttpClient();

                List<KeyValuePair<string, string>> requestParemeter = new List<KeyValuePair<string, string>>
                {
                    { new KeyValuePair<string, string>("user", settings.UserId) },
                    { new KeyValuePair<string, string>("pass", settings.Password) },
                    { new KeyValuePair<string, string>("sid", settings.SidEng) }
                };

                if (contents.Count > 0)
                {
                    var index = 0;
                    foreach (var item in contents)
                    {
                        requestParemeter.Add(new KeyValuePair<string, string>($"sms[{index}][0]", item.To));
                        requestParemeter.Add(new KeyValuePair<string, string>($"sms[{index}][1]", item.Message));
                        requestParemeter.Add(new KeyValuePair<string, string>($"sms[{index}][2]", item.From));
                        index++;
                    }
                }

                var requestResult = await client.PostAsync(settings.GatewayURL, new FormUrlEncodedContent(requestParemeter));
                var rawRequestString = await requestResult.Content.ReadAsStringAsync();

                return rawRequestString;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
