using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using MyLib;

namespace WCFServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 服务器基址
            //Uri baseAddress = new Uri("http://localhost:999/services");



            //// 声明服务器主机
            //using (ServiceHost host = new ServiceHost(typeof(MyService), baseAddress))
            //{
            //    // 添加绑定和终结点
            //    WSHttpBinding binding = new WSHttpBinding();
            //    host.AddServiceEndpoint(typeof(IService), binding, "/test");
            //    // 添加服务描述
            //    host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            //    // 把自定义的IEndPointBehavior插入到终结点中
            //    foreach (var endpont in host.Description.Endpoints)
            //    {
            //        endpont.EndpointBehaviors.Add(new MyLib.MyEndPointBehavior());
            //    }
            //    try
            //    {
            //        // 打开服务
            //        host.Open();
            //        Console.WriteLine("服务已启动。");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //Console.ReadKey();
            //}
            ////////////////////////
            try
            {
                var baseAddress = "http://localhost:999/";
                var ws = new RealServerHost(baseAddress);
                ws.Start();
                Console.WriteLine("已启动");
            }
            catch (Exception ex)
            {
                var message = "服务端异常[" + ex.GetType().Name + "]:" + ex.Message;
                Console.WriteLine(message);
            }

            Console.ReadKey();



        }
    }



    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MyService : IMyService
    {
        public int AddInt(int a, int b)
        {
            return a + b;
        }

        public Student GetStudent()
        {
            Student stu = new Student();
            stu.StudentName = "小明";
            stu.StudentAge = 22;
            return stu;
        }

        public int Divide(int x, int y)
        {
            if (0 == y)
            {
                throw new FaultException("被除数y不能为零!");
            }
            return x / y;

        }

        public CalResultResponse ComputingNumbers(CalcultRequest inMsg)
        {
            CalResultResponse rmsg = new CalResultResponse();
            switch (inMsg.Operation)
            {
                case "加":
                    rmsg.ComputedResult = inMsg.NumberA + inMsg.NumberB;
                    break;
                case "减":
                    rmsg.ComputedResult = inMsg.NumberA - inMsg.NumberB;
                    break;
                case "乘":
                    rmsg.ComputedResult = inMsg.NumberA * inMsg.NumberB;
                    break;
                case "除":
                    rmsg.ComputedResult = inMsg.NumberA / inMsg.NumberB;
                    break;
                default:
                    throw new ArgumentException("运算操作只允许加、减、乘、除。");
                    break;
            }
            return rmsg;
        }
    }


}