using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    [ServiceContract(Namespace = "MyNamespace")]
    public interface IMyService
    {
        [OperationContract]
        int AddInt(int a, int b);

        [OperationContract]
        Student GetStudent();

        [OperationContract]
        CalResultResponse ComputingNumbers(CalcultRequest inMsg);

        [OperationContract]
        int Divide(int x, int y);

        [OperationContract]
        void TestGetHeaderMethod();
    }
}
