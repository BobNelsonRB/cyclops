﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cyclops.Data.Common
{
    public static class Extensions
    {
        public static void AddHeaders<T>(this ApiRequest<T> request, Func<IEnumerable<Tuple<string, string>>> addHeaders) where T : class, new()
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
            HashSet<string> hs = new HashSet<string>();
            if (request.Headers != null)
            {
                foreach (Tuple<string, string> header in request.Headers)
                {
                    if (hs.Add(header.Item1))
                    {
                        list.Add(header);
                    }
                }
            }
            foreach (Tuple<string, string> header in addHeaders())
            {
                if (hs.Add(header.Item1))
                {
                    list.Add(header);
                }
            }
            request.Headers = list;
        }

        public static HttpClient HttpClient<T>(this ApiRequest<T> request) where T : class, new()
        {
            HashSet<string> hs = new HashSet<string>();
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(String.Format("{0}://{1}", request.Protocol.ToString(), request.RootUrl))
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            foreach (Tuple<string, string> header in request.Headers)
            {
                if (!String.IsNullOrWhiteSpace(header.Item1) && !String.IsNullOrWhiteSpace(header.Item2))
                {
                    if (hs.Add(header.Item1))
                    {
                        client.DefaultRequestHeaders.Add(header.Item1, header.Item2);
                    }
                }


            }
            return client;
        }

        public static string ComposeUrl<T>(this ApiRequest<T> request) where T : class, new()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(request.Url);
            if (request.RouteParameter != null)
            {
                sb.Append("/" + request.RouteParameter.ToString());
            }
            else if (request.QueryString.Count > 0)
            {
                int i = 0;
                sb.Append("?");
                foreach (var item in request.QueryString)
                {
                    if (i++ > 0)
                    {
                        sb.Append("&");
                    }
                    sb.AppendFormat("{0}={1}", item.Key, item.Value.ToString());
                }
            }
            return sb.ToString();
        }

        public static HttpContent Content<T>(this ApiRequest<T> request) where T : class, new()
        {
            HttpContent content = null;
            if (request.Model != null)
            {
                string json = JsonConvert.SerializeObject(request.Model);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return content;
        }

        public static Dictionary<string, object> ToDictionary(this Parameters parameters)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (Parameter item in parameters)
            {
                d.Add(item.Key, item.Value);
            }
            return d;
        }

    }
}
