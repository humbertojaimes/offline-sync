using System;
using System.Collections.Generic;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class CatTabla
    {
        public int IdCatTabla { get; set; }
        public string NombreTabla { get; set; } = null!;
        public DateTime FechaSync { get; set; }
    }
}
