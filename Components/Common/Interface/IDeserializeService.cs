using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IDeserializeService
    {
        IEnumerable<T> DeserializeIEnumerableObj<T>(string data);
        List<T> DeserializeListObj<T>(string data);
        T DeserializeObj<T>(string data);
        int Deserializeinteger<T>(string data);
        string Deserializestring<T>(string data);
        long Deserializelong<T>(string data);
    }
}
