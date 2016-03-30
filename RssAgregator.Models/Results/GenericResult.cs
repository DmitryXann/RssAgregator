using RssAgregator.Models.Enums;

namespace RssAgregator.Models.Results
{
    public class InfoResult
    {
        public ResultCodeEnum ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }

    public class GenericResult<T>
    {
        public InfoResult InfoResult { get; private set; }

        public T DataResult { get; private set; }

        public GenericResult()
        {
            InfoResult = new InfoResult();
        }

        public void SetDataResult(T dataResult)
        {
            InfoResult.ResultCode = ResultCodeEnum.Success;
            DataResult = dataResult;
        }

        public void SetDataResult(GenericResult<T> previousResult)
        {
            InfoResult = previousResult.InfoResult;
            DataResult = previousResult.DataResult;
        }


        public void SetSuccessResultCode()
        {
            InfoResult.ResultCode = ResultCodeEnum.Success;
        }


        public void SetErrorResultCode(string message)
        {
            InfoResult.ResultCode = ResultCodeEnum.Error;
            InfoResult.ResultMessage = message;
        }

        public void SetErrorResultCode<GT>(GenericResult<GT> previousResult, string message = null, bool getMessageFromPreviousResult = true)
        {
            InfoResult.ResultCode = ResultCodeEnum.Error;

            if (getMessageFromPreviousResult && previousResult.InfoResult.ResultCode == ResultCodeEnum.Success && typeof(GT) == typeof(string))
            {
                InfoResult.ResultMessage = previousResult.DataResult as string;
            }
            else
            {
                InfoResult.ResultMessage = string.IsNullOrEmpty(message) ? previousResult.InfoResult.ResultMessage : message;
            } 
        }


        public void SetWarningResultCode(string message)
        {
            InfoResult.ResultCode = ResultCodeEnum.Warning;
            InfoResult.ResultMessage = message;
        }

        public void SetWarningResultCode<GT>(GenericResult<GT> previousResult, string message = null, bool getMessageFromPreviousResult = true)
        {
            InfoResult.ResultCode = ResultCodeEnum.Warning;

            if (getMessageFromPreviousResult && previousResult.InfoResult.ResultCode == ResultCodeEnum.Success && typeof(GT) == typeof(string))
            {
                InfoResult.ResultMessage = previousResult.DataResult as string;
            }
            else
            {
                InfoResult.ResultMessage = string.IsNullOrEmpty(message) ? previousResult.InfoResult.ResultMessage : message;
            }
        }
    }
}
