using Common.Interface;
using Entities.ViewModels;

namespace Common.Repository
{
    public class ExceptionService: IExceptionService
    {
        public ResponseModel ControllerException(Exception ex)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.status_code = 500;
            responseModel.error = ex.Message;
            return responseModel;
        }
    }
}
