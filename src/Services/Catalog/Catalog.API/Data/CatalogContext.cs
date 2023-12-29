using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API;

public class CatalogContext : ICatalogContext
{
    // Así inyectamos la configuracion del appsettings.json
    public CatalogContext(IConfiguration configuration)
    {   
        // Al crear el CatalogContext, creamos una conexion con MongoDB y hacemos un seed
        // Así extraemos uno de los valores del archivo appsettings.json inyectado
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));   
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        //CatalogContextSeed.SeedData(Products);
    }
    public IMongoCollection<Product> Products {get;}
}
