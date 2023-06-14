using Common.Interface;
using Entities.ViewModels;

namespace Common.Repository
{
    public class ExceptionService: IExceptionService
    {
        public ResponseModel ControllerException(Exception ex)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.StatusCode = 500;
            responseModel.Error = ex.Message;
            return responseModel;
        }
    }
}
