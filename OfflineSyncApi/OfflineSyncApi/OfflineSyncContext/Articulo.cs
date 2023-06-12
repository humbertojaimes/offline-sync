using System;
using System.Collections.Generic;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class Articulo
    {
        public int Id { get; set; }
        public long Ean { get; set; }
        public string Descripcion { get; set; } = null!;
        public byte[] ArticuloHashId { get; set; }
    }
}
