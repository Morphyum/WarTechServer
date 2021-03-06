﻿using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;

namespace PersistentMapServer.MessageInspector {
    /* MessageInpector that looks for requests that contain Accept-Encoding: gzip header. If that's set, returns the 
     * response compressed.
     */
    public class GZipInspector : IDispatchMessageInspector {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, System.ServiceModel.InstanceContext instanceContext) {
            try {
                var prop = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                var accept = prop.Headers[HttpRequestHeader.AcceptEncoding];

                if (!string.IsNullOrEmpty(accept) && accept.Contains("gzip"))
                    OperationContext.Current.Extensions.Add(new CompressOutputExtension());
            } catch { }

            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState) {
            if (OperationContext.Current.Extensions.OfType<CompressOutputExtension>().Any()) {                
                WebOperationContext.Current.OutgoingResponse.Headers.Add($"Content-Encoding:gzip");
            }
        }
    }
}
