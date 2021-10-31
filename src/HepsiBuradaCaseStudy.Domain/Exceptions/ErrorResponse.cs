using System;

namespace HepsiBuradaCaseStudy.Domain.Exceptions
{
    [Serializable]
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
