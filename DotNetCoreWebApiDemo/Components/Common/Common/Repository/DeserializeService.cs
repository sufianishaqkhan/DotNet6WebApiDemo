using Common.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class DeserializeService : IDeserializeService
    {
        public IEnumerable<T> DeserializeIEnumerableObj<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            return result;
        }
        public List<T> DeserializeListObj<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<List<T>>(data);
            return result;
        }

        public T DeserializeObj<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }
        public int Deserializeinteger<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<int>(data);
            return result;
        }
        public string Deserializestring<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<string>(data);
            return result;
        }
        public long Deserializelong<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<long>(data);
            return result;
        }
    }
}
