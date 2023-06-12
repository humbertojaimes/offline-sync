using System.Data;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfflinceSyncDTOs;
using OfflineSyncApi.OfflineSyncContext;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<VentasContext>
    (builder.Configuration.GetConnectionString("OfflineSyncContext"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/products", ( VentasContext context)
    =>  Results.Ok(context.Articulos.Select(a => new Product(a.Id, a.Ean, a.Descripcion))));

app.MapGet("/stores", (VentasContext context)
    =>  Results.Ok(context.Tiendas.Select(t => new Store(t.Id, t.Tienda1, t.Direccion))));

app.MapGet("/sales", (VentasContext context)
    =>
{
    var sales = context.VentasTiendas.Select(s => new Sale(s.Id,DateTime.ParseExact(s.Fecha, "yyyyMMdd", CultureInfo.InvariantCulture)
    , s.Tienda, s.Articulo, s.Venta, BitConverter.ToString(s.VentasTiendasHashId).Replace("-", "")));

    return Results.Ok(sales);
 });

app.MapGet("/salesId", (VentasContext context)
    =>
{
    var sales = context.VentasTiendas.Take(10000).Select(s => new SaleSync(s.Id, BitConverter.ToString(s.VentasTiendasHashId).Replace("-", "")));

    return Results.Ok(sales);
});

app.MapGet("/products/version", async (VentasContext context)
    =>
{
    var version = await context.CatTablas.FirstAsync(c => c.NombreTabla == "Articulos");
    return Results.Ok(new ProductsVersion(version.FechaSync));
});

app.MapPost("/syncsales", (List<SaleSync> sales, VentasContext context)
    =>
{
    DataTable VentasTiendasSyncOfflineApp = new();
    VentasTiendasSyncOfflineApp.Columns.Add("ID", typeof(int));
    VentasTiendasSyncOfflineApp.Columns.Add("VentasTiendasHashID", typeof(byte[]));

    var ventasHash = sales.Select(s => new VentasTiendasSyncOfflineApp()
    { ID = s.Id, VentasTiendasHashID = Convert.FromHexString(s.HashId) }
    );

    foreach (var s in ventasHash)
    {
        VentasTiendasSyncOfflineApp.Rows.Add(s.ID, s.VentasTiendasHashID);
    }

    SqlParameter parameter = new("@VTSOLAPP", SqlDbType.Structured);
    parameter.Value = VentasTiendasSyncOfflineApp;
    parameter.TypeName = "[dbo].[VentasTiendasSyncOfflineApp]";
    var response = context.VentasTiendasSyncChanges.FromSqlRaw
    ("EXECUTE dbo.usp_VentasTiendasSyncOfflineHashID @VTSOLAPP", parameter).ToList()
    .Select(v => new SaleSyncChange(v.Id, v.Fecha is not null ? DateTime.ParseExact(v.Fecha, "yyyyMMdd", CultureInfo.InvariantCulture)
    : default(DateTime), v.Tienda, v.Articulo, v.Venta,  BitConverter.ToString(v.VentasTiendasHashId).Replace("-", "")
    ,v.TipoMovimiento));
    return Results.Ok(response);
});


app.Run();

