using System;

namespace OfflinceSyncDTOs
{
    public record Product(int Id, long Ean, string Description);

    public record Store(int Id, string Name, string Address);

    public record Sale(int Id,DateTime Date, int StoreId, int ProductId, int SaleAmount, string HashId);

    public record SaleSync(int Id, string HashId);

    public record SaleSyncChange(int Id, DateTime Date, int? StoreId, int? ProductId, int? SaleAmount, string? HashId,string OperationType);

    public record ProductsVersion(DateTime version);

}

