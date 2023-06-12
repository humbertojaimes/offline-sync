using System;
using System.Collections.Generic;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class ResolverConflicto
    {
        public int IdResolverConflictos { get; set; }
        public int IdCatTabla { get; set; }
        public int IdCatConflictos { get; set; }
        public string Resuelto { get; set; } = null!;
    }
}
