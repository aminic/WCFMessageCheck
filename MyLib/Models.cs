using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string StudentName;
        [DataMember]
        public int StudentAge;
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string LoginName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }

    [MessageContract]
    public class CalcultRequest
    {
        [MessageHeader]
        public string Operation { get; set; }
        [MessageBodyMember]
        public int NumberA { get; set; }
        [MessageBodyMember]
        public int NumberB { get; set; }
    }

    [MessageContract]
    public class CalResultResponse
    {
        [MessageBodyMember]
        public int ComputedResult;
    }
}
