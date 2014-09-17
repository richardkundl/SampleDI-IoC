using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleDI.Context
{
    public class HttpContextService : IContextService
    {
        public HttpContextService()
        {
            if (HttpContext.Current == null)
            {
                throw new ArgumentException("No available Http context.");
            }
        }

        public string GetContextulFullFilePath(string fileName)
        {
            return HttpContext.Current.Server.MapPath(string.Concat("~/", fileName));
        }

        public string GetUserName()
        {
            var userName = "null";
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.User != null)
                {
                    userName = (HttpContext.Current.User.Identity.IsAuthenticated
                                    ? HttpContext.Current.User.Identity.Name
                                    : "null");
                }
            }
            catch
            {
            }

            return userName;
        }

        public ContextProperties GetContextProperties()
        {
            var props = new ContextProperties();
            if (HttpContext.Current == null)
            {
                return props;
            }

            HttpRequest request = null;
            try
            {
                request = HttpContext.Current.Request;
            }
            catch
            {
            }

            if (request != null)
            {
                props.UserAgent = request.Browser == null ? string.Empty : request.Browser.Browser;
                props.RemoteHost = request.ServerVariables == null ? string.Empty : request.ServerVariables["REMOTE_HOST"];
                props.Path = request.Url == null ? string.Empty : request.Url.AbsolutePath;
                props.Query = request.Url == null ? string.Empty : request.Url.Query;
                props.RemoteHost = request.UrlReferrer == null ? string.Empty : request.UrlReferrer.ToString();
                props.Method = request.HttpMethod;
            }

            var items = HttpContext.Current.Items;
            if (items != null)
            {
                var requestId = items["RequestId"];
                if (requestId != null)
                {
                    props.RequestId = requestId.ToString();
                }
            }

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                var sessionId = session["SessionId"];
                if (sessionId != null)
                {
                    props.SessionId = sessionId.ToString();
                }
            }

            return props;
        }
    }
}
