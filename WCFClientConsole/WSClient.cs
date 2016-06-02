using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFClientConsole
{
    public class WSClient
    {
        string WCFHostPrefix;

        List<object> services;

        public WSClient(string hostPrefix)
        {
            WCFHostPrefix = hostPrefix;
            Init();
        }
        private void Init()
        {
            services = new List<object>();
        }

        /// <summary>
        /// 注册客户端调用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        T Register<T>(string url, string interfaceFullName, string businessClassName)
        {
            EndpointAddress endpointAddress = new EndpointAddress(url);
            ChannelFactory<T> factory = new ChannelFactory<T>(new BasicHttpBinding(), endpointAddress);
            //加入拦截行为
            factory.Endpoint.EndpointBehaviors.Add(new MyEndPointBehavior());
            return factory.CreateChannel();
        }

       

        public T GetService<T>()
        {
            var o = services.Where(x => x.GetType().Name == typeof(T).Name).SingleOrDefault();
            if (o != null)
            {
                var service = (T)o;
            }

            Type interfaceType = typeof(T);
            string businessClassName = typeof(T).Name.Substring(1);
            string address = string.Format("{0}/{1}", WCFHostPrefix, businessClassName);
            var s = Register<T>(address, interfaceType.FullName, businessClassName);
            services.Add(s);
            return s;
        }
    }
}
