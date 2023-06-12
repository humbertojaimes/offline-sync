using System;
using OfflineSyncMobileApp.Enums;

namespace OfflineSyncMobileApp.Interfaces
{
    public interface IRestResponse<T>  where T: class
    {

        ResponseStatus ResponseStatus { get; set; }

        string ResponseMessage { get; set; }

        T Response { get; set; }
    }
}
