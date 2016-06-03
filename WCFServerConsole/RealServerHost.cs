using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

namespace WCFServerConsole
{

    public class RealServerHost
    {

        string baseUrl;

        /// <summary>
        /// 服务宿主
        /// </summary>
        ICollection<ServiceHost> hosts = new List<ServiceHost>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl">形如http://localhost:999/</param>
        public RealServerHost(string baseUrl)
        {
            this.baseUrl = baseUrl;
            RegisterServices(new Type[]{
                typeof(MyService),
                //typeof(OrderSortNumberService),
                //typeof(AccountBookService),
                //typeof(WSConnectionService),


            });
        }

        private void RegisterServices(Type[] serviceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                RegisterService(serviceType);
            }
        }

        void RegisterService(Type serviceType)
        {
            var interType = serviceType.GetInterfaces().Where(x => x.Name == "I" + serviceType.Name).FirstOrDefault();
            if (interType == null)
                return;

            var uri = new Uri(this.baseUrl + serviceType.Name);
            ServiceHost h = new ServiceHost(serviceType, uri);


            var behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            //behavior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            var binding = Bindings.GetBindingInstance<BasicHttpBinding>();
            behavior.HttpGetBinding = binding;
            h.Description.Behaviors.Add(behavior);


            //传递异常信息
            var debug = h.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
            {
                h.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                debug.IncludeExceptionDetailInFaults = true;
            }

            //


            ////加入拦截行为
            //var endpoint = h.AddServiceEndpoint(interType, binding, "");
            //endpoint.Behaviors.Add(new MyEndPointBehavior());




            hosts.Add(h);
        }

        public void Start()
        {
            foreach (var h in hosts)
            {
                h.Open();
            }

        }
        public void Stop()
        {
            foreach (var h in hosts)
            {
                h.Close();
            }
            hosts.Clear();
        }

        public Dictionary<string, string> GetHostState()
        {
            var result = new Dictionary<string, string>();

            foreach (var h in hosts)
                result.Add(h.Description.Endpoints.First().Address.Uri.ToString(), h.State.ToString());

            return result;
        }
    }
}
