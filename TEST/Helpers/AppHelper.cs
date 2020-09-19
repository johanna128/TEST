using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TEST.ViewModel;

namespace TEST.Helpers
{
    public class AppHelper
    {
        private string userKey = "OWUyYWM5ZGQtMjI0NC00ZWU4LWJjZWUtMjE5N2NhMzcxY2E5";
        public string oneSignalURL = "https://onesignal.com/api/v1/apps";

        public AppViewModel ViewApps(AppViewModel viewModel)
        {
            WebRequest request = PrepearRequest("GET", null);
            var json = GetJson(request);
            viewModel.AppDataList = JsonConvert.DeserializeObject<List<AppData>>(json);

            return viewModel;
        }

        public AppViewModel CreateApp(AppViewModel viewModel, AppData appData)
        {
            WebRequest request = PrepearRequest("POST", null);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = JsonConvert.SerializeObject(appData);
                streamWriter.Write(jsonString);
            }

            var json = GetJson(request);
            AppData appDataResult = JsonConvert.DeserializeObject<AppData>(json);

            viewModel.AppDataList = new List<AppData>();
            viewModel.AppDataList.Add(appDataResult);

            viewModel.SuccessMessage = "A new app is now created";

            return viewModel;
        }

        public void UpdateApp(AppData appData)
        {  
                        WebRequest request = PrepearRequest("PUT", appData.id);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = JsonConvert.SerializeObject(appData);
                streamWriter.Write(jsonString);
            }

            var myWebResponse = request.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            responseStream.Close();
            myWebResponse.Close();
        }

        private WebRequest PrepearRequest(string method, string id)
        {
            string url;
            if (id == null)
            {
                url = oneSignalURL;
            }
            else
            {
                url = string.Format("{0}/{1}", oneSignalURL, id);
            }

            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            request.Headers.Add("Authorization", string.Format("{0} {1}", "Basic", userKey));
            request.ContentType = "application/json";

            return request;
        }

        private string GetJson(WebRequest request)
        {
            var myWebResponse = request.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();
            if (responseStream == null) return null;

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            responseStream.Close();
            myWebResponse.Close();

            return json;
        }
    }
}