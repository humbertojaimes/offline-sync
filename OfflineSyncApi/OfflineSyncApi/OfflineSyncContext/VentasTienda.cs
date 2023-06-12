using System;
using System.Collections.Generic;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class VentasTienda
    {
        public int Id { get; set; }
        public string Fecha { get; set; } = null!;
        public int Tienda { get; set; }
        public int Articulo { get; set; }
        public int Venta { get; set; }
        public byte[] VentasTiendasHashId { get; set; }
    }
}
