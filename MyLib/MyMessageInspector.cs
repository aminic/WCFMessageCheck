using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace MyLib
{

    public class MyMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            //Console.WriteLine("客户端接收到的回复：\n{0}", reply.ToString());
            return;
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //Console.WriteLine("客户端发送请求前的SOAP消息：\n{0}", request.ToString());
            // 插入验证信息
            MessageHeader hdUserName = MessageHeader.CreateHeader("u", "ns", "admin");
            MessageHeader hdPassWord = MessageHeader.CreateHeader("p", "ns", "123");

            request.Headers.Add(hdUserName);
            request.Headers.Add(hdPassWord);
            var user = new UserInfo { LoginName = "xman", Password = "101" };
            var messageHeaderUser = MessageHeader.CreateHeader("userinfo", "check", user);
            request.Headers.Add(messageHeaderUser);

            return null;
        }
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("服务器端：接收到的请求：\n{0}", request.ToString());
            // 栓查验证信息
            //string un = request.Headers.GetHeader<string>("u", "ns");
            //string ps = request.Headers.GetHeader<string>("p", "ns");
            //if (un == "admin" && ps == "abcd")
            //{
            //    Console.WriteLine("用户名和密码正确。");
            //}
            //else
            //{
            //    throw new Exception("验证失败，滚吧！");
            //}
            var user = request.Headers.GetHeader<UserInfo>("userinfo", "check");
            if (user == null)
            {
                var message = "未传入有效用户信息";
                Console.WriteLine(message);
                throw new FaultException(message);
            }

            if (user.LoginName == "admin")
            {
                var message = "用户身份是超管";
                Console.WriteLine(message);
            }
            else
            {
                var message = "用户身份不是超管，验证失败";
                Console.WriteLine(message);
                throw new FaultException(message);
            }

            return instanceContext;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
            //Console.WriteLine("服务器即将作出以下回复：\n{0}", reply.ToString());
            return;
        }
    }


}