﻿using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;

namespace TeacherAITools.Application.Common.Exceptions
{
    public class ApiException : Exception
    {
        private int _errorCode;

        private string _error;

        private string _message;

        public ApiException(ResponseCode responseCode)
        {
            _errorCode = (int)responseCode;
            _error = responseCode.ToString();
            _message = responseCode.GetDescription();
        }

        public ApiException(int errorCode, string error, string message)
        {
            _errorCode = errorCode;
            _error = error;
            _message = message;
        }

        public int ErrorCode => _errorCode;

        public string Error => _error;

        public string ErrorMessage => _message;
    }
}
