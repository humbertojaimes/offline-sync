using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OfflineSyncMobileApp.Enums;
using OfflineSyncMobileApp.Interfaces;

namespace OfflineSyncMobileApp.Implementations
{
    public class GenericRestService<T,U> : BaseRestService, IRestService<T, U>
        where T : class
        where U : class
    { 
        private readonly string baseUri;

        public GenericRestService(string baseUri, string token)
            : base(token)
        {
            this.baseUri = baseUri;
        }

        public GenericRestService(string baseUri)
            :base()
        {
            this.baseUri = baseUri;
        }

        public virtual async Task<IRestResponse<IEnumerable<U>>> GetAll(string uri = "") 
            => await SendRequest<U, IEnumerable<U>>(HttpMethod.Get, uri: $"{baseUri}{uri}");

        public virtual async Task<IRestResponse<U>> Get(int id,string uri = "")
            => await SendRequest<U,U>(HttpMethod.Get, uri: $"{baseUri}{uri}/{id}");

        public virtual async Task<IRestResponse<U>> Get(string uri = "")
            => await SendRequest<U, U>(HttpMethod.Get, uri: $"{baseUri}{uri}");


        public virtual async Task<IRestResponse<U>> Post(T content, string uri = "")
            => await SendRequest<T,U>(HttpMethod.Post, content, $"{baseUri}{uri}");

        public async Task<IRestResponse<U>> Put(T content, string uri = "")
            => await SendRequest<T, U>(HttpMethod.Post, content, $"{baseUri}{uri}");

        public virtual async Task<IRestResponse<U>> Delete(T content, string uri = "")
            => await SendRequest<T, U>(HttpMethod.Delete, content, $"{baseUri}{uri}");
    }
}
