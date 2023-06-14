using Common.Interface;
using Entities.ViewModels;
using Newtonsoft.Json;

namespace Common.Repository
{
    public class SerializeService : ISerializeService
    {
        public ResponseModel SerializeIEnumerableObj<T>(IEnumerable<T> data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data ,Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public ResponseModel SerializeListObj<T>(List<T> data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public ResponseModel SerializeObj<T>(T data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }
        public ResponseModel Serializeinteger<T>(int data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }
        public ResponseModel Serializestring<T>(string data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }
        public ResponseModel Serializelong<T>(long data)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            responseModel.StatusCode = 200;
            return responseModel;
        }
        public string SerializeObjToString<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
