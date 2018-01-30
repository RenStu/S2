using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcService.Controllers
{
    public class BaseController : Controller
    {
        #region protected

        protected string GetEndPoint(string address)
        {
            ServicePartitionResolver resolver = ServicePartitionResolver.GetDefault();
            ResolvedServicePartition partition =
                  resolver.ResolveAsync(new Uri(address), new ServicePartitionKey(), new CancellationToken()).Result;

            ResolvedServiceEndpoint endpoint = partition.GetEndpoint();

            JObject addresses = JObject.Parse(endpoint.Address);
            return (string)addresses["Endpoints"].First();
        }

        protected HttpClientHandler GetCookie(string endpoint)
        {
            var cookieContainer = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler
            {
                UseCookies = true,
                UseDefaultCredentials = true,
                CookieContainer = cookieContainer
            };

            foreach (var cookie in Request.Cookies)
            {
                cookieContainer.Add(new Cookie(cookie.Key, cookie.Value, "/", new Uri(endpoint).Host));
            }

            return handler;
        }

        protected static ByteArrayContent ObjectToContent(object obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        protected T ContentToObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected string GetAsync(string endpoint)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient(GetCookie(endpoint)))
            {
                response = client.GetAsync(endpoint).Result;
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        protected string PostAsync(string endpoint, ByteArrayContent byteContent)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient(GetCookie(endpoint)))
            {
                response = client.PostAsync(endpoint, byteContent).Result;
            }
            return response.Content.ReadAsStringAsync().Result;
        }


        protected string PutAsync(string endpoint, ByteArrayContent byteContent)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient(GetCookie(endpoint)))
            {
                response = client.PutAsync(endpoint, byteContent).Result;
            }

            return response.Content.ReadAsStringAsync().Result;
        }


        protected string DeleteAsync(string endpoint)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient(GetCookie(endpoint)))
            {
                response = client.DeleteAsync(endpoint).Result;
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        #endregion
    }
}