using System;
using OfflineSyncMobileApp.Enums;
using OfflineSyncMobileApp.Interfaces;

namespace OfflineSyncMobileApp.Implementations
{
    public class GenericRestResponse<T> : IRestResponse<T> where T : class
    {

        public ResponseStatus ResponseStatus { get; set; } = ResponseStatus.Error;
        public string ResponseMessage { get; set; } = "Error";
        public T Response { get; set; }
    }
}
