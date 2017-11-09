using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Helpers
{
    class APIRequestHelper
    {
        HttpClient httpClient;

        public APIRequestHelper()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetRequest(string url)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("GET Request executed succesfully: " + response.StatusCode);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Debug.WriteLine("GET Request failed: " + response.StatusCode);
                    return null;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception occured: " + e.Message);
                return null;
            }
        }

        public async Task<string> PostRequest(string url, string content)
        {
            StringContent apiContent = new StringContent(content, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, apiContent);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Post Request executed succesfully: " + response.StatusCode);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Debug.WriteLine("Post Request failed: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception occured: " + e.Message);
                return null;
            }
        }

        public async Task<string> PutRequest(string url, string content)
        {
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.PutAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("PUT Request executed succesfully: " + response.StatusCode);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Debug.WriteLine("PUT Request failed: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception occured: " + e.Message);
                return null;
            }
        }
    }
}
