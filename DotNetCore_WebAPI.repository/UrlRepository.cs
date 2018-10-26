using DotNetCore_WebAPI.entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.repository
{
    public abstract class UrlRepository
    {
        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }

        public UrlRepository(Uri baseEndpoint)
        {
            BaseEndpoint = baseEndpoint ?? throw new ArgumentNullException("baseEndpoint");
            _httpClient = new HttpClient();
        }

        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            try
            {
                addHeaders();
                var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.GetAsync() experienced a url timeout", GetType().FullName), ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(String.Format("{0}.GetAsync() HttpRequestException", GetType().FullName), ex);
            }
            catch (UriFormatException ex)
            {
                throw new Exception(String.Format("{0}.GetAsync() UriFormatException", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.GetAsync() Exception (not a timeout)", GetType().FullName), ex);
            }
        }

        /// <summary>  
        /// Common method for making POST calls  
        /// </summary>  
        protected async Task<TaskResult<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            try
            {
                addHeaders();
                var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaskResult<T>>(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() experienced a url timeout", GetType().FullName), ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() HttpRequestException", GetType().FullName), ex);
            }
            catch(UriFormatException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() UriFormatException", GetType().FullName), ex);
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() Exception (not a timeout)", GetType().FullName), ex);
            }
            
        }
        protected async Task<TaskResult<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            try
            {
                addHeaders();
                var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaskResult<T1>>(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() experienced a url timeout", GetType().FullName), ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() HttpRequestException", GetType().FullName), ex);
            }
            catch (UriFormatException ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() UriFormatException", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.PostAsync() Exception (not a timeout)", GetType().FullName), ex);
            }
        }

        protected Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint)
            {
                Query = queryString
            };
            return uriBuilder.Uri;
        }

        protected HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        protected void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }

    }
}
