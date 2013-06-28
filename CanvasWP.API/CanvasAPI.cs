using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CanvasWP.API
{
    public delegate void AsyncCompletedEventHandler<T>(object sender, AsyncCompletedEventArgs<T> e);

    public class AsyncCompletedEventArgs<T> : System.ComponentModel.AsyncCompletedEventArgs
    {
        public T Result { get; private set; }
        public string NextURL { get; private set; }

        public AsyncCompletedEventArgs(T result, Exception ex, bool cancelled, object userState) :
            base(ex, cancelled, userState)
        {
            this.Result = result;
        }
    }

    public delegate void RequestCompletedDelegate(string responseText, string nextURL, object userState, Exception ex);
    
    class RequestState
    {
        // This class stores the State of the request.
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public Stream streamResponse;
        public RequestCompletedDelegate callback;
        public object userState;
        public string postData;
        public RequestState(HttpWebRequest req, RequestCompletedDelegate callback, object userState, string postData = null)
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = req;
            streamResponse = null;
            this.callback = callback;
            this.userState = userState;
            this.postData = postData;
        }
    }

    public partial class CanvasAPI
    {
        public string AccessToken { get; set; }
        public string BaseURL { get; set; }
        public string Domain
        {
            get
            {
                int pos = BaseURL.IndexOf("//") + 2;
                return BaseURL.Substring(pos, BaseURL.Length - pos);
            }
        }
        public long? UserID { get; set; }

        private IAsyncResult currentAsyncResult;

        public CanvasAPI()
        {
            this.BaseURL = "https://canvas.instructure.com/";
            this.AccessToken = null;
            this.currentAsyncResult = null;
        }
        
        public CanvasAPI(string url, string accessToken = null)
        {
            this.BaseURL = url;
            this.AccessToken = accessToken;
            this.currentAsyncResult = null;
        }

        private HttpWebRequest CreateRequest(string baseURL, string endpoint, string parms)
        {
            string url = baseURL + endpoint + (parms == null ? "" : "?" + parms);
            WebClient cli = new WebClient();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(baseURL + endpoint);
            req.UserAgent = "CanvasWP";
            if (AccessToken != null)
                req.Headers["Authorization"] = "Bearer " + AccessToken;
            return req;
        }

        public void BeginGet(string endpoint, string parms, RequestCompletedDelegate callback, object userState)
        {
            BeginGet(this.BaseURL, endpoint, parms, callback, userState);
        }

        public void BeginGet(string baseURL, string endpoint, string parms, RequestCompletedDelegate callback, object userState)
        {
            HttpWebRequest req = CreateRequest(baseURL, endpoint, parms);
            // TODO: Implement Timeout
            currentAsyncResult = req.BeginGetResponse(new AsyncCallback(RequestCompleted), new RequestState(req, callback, userState));
        }

        public void BeginPost(string endpoint, string parms, string postData, RequestCompletedDelegate callback, object userState = null)
        {
            BeginPost(this.BaseURL, endpoint, parms, postData, callback, userState);
        }

        public void BeginPost(string baseURL, string endpoint, string parms, string postData, RequestCompletedDelegate callback, object userState = null)
        {
            HttpWebRequest req = CreateRequest(baseURL, endpoint, parms);
            req.Method = "POST";
            // TODO: Implement Timeout
            req.BeginGetRequestStream(new AsyncCallback(RequestReady), new RequestState(req, callback, userState, postData));
        }

        private void RequestReady(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            HttpWebRequest req = state.request;

            Stream stream = req.EndGetRequestStream(result);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(state.postData);
            writer.Flush();
            writer.Close();
            currentAsyncResult = req.BeginGetResponse(new AsyncCallback(RequestCompleted), result.AsyncState);
        }

        private void RequestCompleted(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)state.request.EndGetResponse(result);

                string nextURL = null;
                if (resp.Headers.AllKeys.Contains("Link"))
                {
                    nextURL = (from l in resp.Headers["Link"].Split(',')
                               where l.Contains("rel=\"next\"")
                               select l).FirstOrDefault();
                    if (!string.IsNullOrEmpty(nextURL))
                    {
                        int start = nextURL.IndexOf('<') + BaseURL.Length + 1;
                        nextURL = nextURL.Substring(start, nextURL.LastIndexOf('>') - start);
                    }
                }

                StreamReader rdr = new StreamReader(resp.GetResponseStream());
                string responseText = rdr.ReadToEnd();
                state.callback(responseText, nextURL, state.userState, null);
                rdr.Close();
                resp.Close();
            }
            catch (Exception ex)
            {
                state.callback(null, null, state.userState, ex);
            }
        }

        private void CancelRequest()
        {
        }
    }
}
