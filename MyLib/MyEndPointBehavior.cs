using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    /// <summary>
    /// 插入到终结点的Behavior
    /// </summary>
    public class MyEndPointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // 不需要
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // 植入“偷听器”客户端
            clientRuntime.ClientMessageInspectors.Add(new MyMessageInspector());
            Console.WriteLine("植入“偷听器”客户端");
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // 植入“偷听器” 服务器端
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MyMessageInspector());
            Console.WriteLine("植入“偷听器” 服务器端");
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // 不需要
            return;
        }
    }
}
