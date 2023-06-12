using System;
using System.Collections.Generic;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class Tienda
    {
        public int Id { get; set; }
        public string Tienda1 { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public byte[] TiendaHashId { get; set; }
    }
}
