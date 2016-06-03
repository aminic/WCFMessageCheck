using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;


namespace WCFClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var baseAddress = "http://localhost:999";
                var c = new WSClient(baseAddress);
                var s = c.GetService<IMyService>();
                //-1.传递附加信息
                using (OperationContextScope scope = new OperationContextScope(s as IContextChannel))
                {
                    var user = new UserInfo { LoginName = "xman", Password = "password is password" };
                    var header = MessageHeader.CreateHeader("userinfo", "check", user);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    s.TestGetHeaderMethod();
                }


                //0.无效除
                //var r = s.Divide(10, 0);
                //Console.WriteLine($"相除结果:{r}");
                //1.调用带元数据参数和返回值的操作
                //var result = s.AddInt(20, 20);
                //Console.WriteLine($"20+20= {result}");
                //2.调用带有数据协定的操作
                //var student = s.GetStudent();
                //Console.WriteLine("\n学生信息---------------------------");
                //Console.WriteLine($"姓名：{student.StudentName}\n年龄：{student.StudentAge}");
                //3.调用带消息协定的操作
                //var result1 = s.ComputingNumbers(new CalcultRequest
                //{
                //    Operation = "乘",
                //    NumberA = 15,
                //    NumberB = 70
                //});
                //Console.WriteLine($"15乘以70的结果是：{result1.ComputedResult}");

            }
            catch (FaultException ex)
            {
                var message = "客户端异常[" + ex.GetType().Name + "]:" + ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n[" + ex.InnerException.GetType().Name + "]    " + ex.InnerException.Message;

                }
                Console.WriteLine(message);
            }
            Console.Read();



        }
    }
}