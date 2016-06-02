using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class Bindings
    {
        public static T GetBindingInstance<T>() where T : BasicHttpBinding
        {

            var basicHttpBinding = new BasicHttpBinding
            {
                Name = "basicHttpBindingConfig",
                MaxBufferSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                MessageEncoding = WSMessageEncoding.Mtom,

                TransferMode = TransferMode.Streamed,
                ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
                {
                    MaxDepth = 2147483647,
                    MaxStringContentLength = 2147483647,
                    MaxArrayLength = 2147483647,
                    MaxBytesPerRead = 2147483647,
                    MaxNameTableCharCount = 2147483647
                },


                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.None,
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.None,
                        ProxyCredentialType = HttpProxyCredentialType.None,
                        Realm = ""
                    },
                    Message = new BasicHttpMessageSecurity
                    {
                        ClientCredentialType = BasicHttpMessageCredentialType.UserName,
                        AlgorithmSuite = SecurityAlgorithmSuite.Default

                    }
                },
                SendTimeout = new TimeSpan(0, 30, 0)
            };
            return basicHttpBinding as T;






        }
    }
}
