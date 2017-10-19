// HttpClient and how to use Headers, Content-Type and PostAsync
// http://d-fens.ch/2014/04/12/httpclient-and-how-to-use-headers-content-type-and-postasync/

// Copyright 2014-2015 Ronald Rink, d-fens GmbH

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// NOTICE
// d-fens HttpClient
// this software contains work developed at
// d-fens GmbH, General-Guisan-Strasse 6, CH-6300 Zug, Switzerland

using Newtonsoft.Json;
using RawHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
//using System.Json;

public class HttpClientWrapper
{
    #region Constructor

    public HttpClientWrapper(string token)
    {
        Token = token;
    }

    #endregion Constructor

    #region Properties

    public string Token { get; set; }

    #endregion Properties

    #region Public methods

    public List<string> Invoke(string httpMethod, string uri, string body)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = new Uri(uri);
        int timeoutSec = 90;
        client.Timeout = new TimeSpan(0, 0, timeoutSec);
        string contentType = "application/json";
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
        client.DefaultRequestHeaders.Add("Authorization", String.Format("SSWS {0}", Token));
        var userAgent = ".NET Foundation Repository Reporter";
        client.DefaultRequestHeaders.Add("User-Agent", userAgent);

        HttpResponseMessage response;
        var method = new HttpMethod(httpMethod);

        List<string> pages = new List<string>();
        switch (method.ToString().ToUpper())
        {
            case "GET":
            case "HEAD":
                pages = GetPagesOfResponse(uri, client);
                break;
            case "POST":
                    HttpContent httpBody = new StringContent(body);
                    httpBody.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    response = client.PostAsync(uri, httpBody).Result;
                    pages = GetPagesOfResponse(uri, client);
                break;
            case "PUT":
                    HttpContent httpBodyPut = new StringContent(body);
                    httpBodyPut.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    response = client.PutAsync(uri, httpBodyPut).Result;
                    pages = GetPagesOfResponse(uri, client);
                break;
            case "DELETE":
                response = client.DeleteAsync(uri).Result;
                pages = GetPagesOfResponse(uri, client);
                break;
            default:
                throw new NotImplementedException();
        }

        return pages;
    }

    #endregion Public methods

    #region Private methods

    private List<string> GetPagesOfResponse(string uri, HttpClient client)
    {
        List<string> pages = new List<string>();
        string nextUrl = uri;
        while (true)
        {
            var response = client.GetAsync(nextUrl).Result;
            nextUrl = "";
            string json = string.Empty;
            using (HttpContent con = response.Content)
            {
                json = con.ReadAsStringAsync().Result;
            }
            pages.Add(json);

            HttpResponseHeaders headers = response.Headers;
            var links = headers.GetValues("Link");
            int c = 0;
            using (var e = links.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    c++;
                    if (e.Current.Contains("self"))
                    {
                        continue;
                    }
                    if (e.Current.Contains("next"))
                    {
                        nextUrl = e.Current.Substring(1);
                        int pos = nextUrl.IndexOf('>');
                        nextUrl = nextUrl.Substring(0, pos);
                    }
                }
                if (string.IsNullOrEmpty(nextUrl))
                {
                    break;
                }
            }
        }

        return pages;
    }

    #endregion Private methods
}