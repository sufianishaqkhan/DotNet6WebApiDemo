using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface ISerializeService
    {
        ResponseModel SerializeIEnumerableObj<T>(IEnumerable<T> data);
        ResponseModel SerializeListObj<T>(List<T> data);
        ResponseModel SerializeObj<T>(T data);
        ResponseModel Serializeinteger<T>(int data);
        ResponseModel Serializestring<T>(string data);
        ResponseModel Serializelong<T>(long data);
        string SerializeObjToString<T>(T data);
    }
}
