using System;

namespace HepsiBuradaCaseStudy.Domain.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string ErrorCode { get; set; } = Utils.DefaultBusinessExceptionErrorCode;
        public BusinessException()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }


        public BusinessException(string message,string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message,Exception innerException) : base(message,innerException)
        {
                
        }
    }
}
