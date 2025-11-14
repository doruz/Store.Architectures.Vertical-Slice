using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

public sealed class ProductsService(RepositoriesContext repositories)
{
    //public async Task<IEnumerable<ProductModel>> GetAllAvailable()
    //{
    //    var products = await repositories.Products.FilterAsync(product => product.Stock > 0);

    //    return products
    //        .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
    //        .Select(ProductsMapper.ToProductModel);
    //}

    //public async Task<IEnumerable<ProductModel>> GetAll()
    //{
    //    var products = await repositories.Products.GetAllAsync();

    //    return products
    //        .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
    //        .Select(ProductsMapper.ToProductModel);
    //}

    //public Task<ProductModel> FindProductAsync(string id) =>
    //    repositories.Products
    //        .FindAsync(id)
    //        .EnsureIsNotNull(id)
    //        .MapAsync(ProductsMapper.ToProductModel);

    //public async Task<ProductModel> AddAsync(NewProductModel productModel)
    //{
    //    var newProduct = productModel.ToProduct();

    //    await repositories.Products.AddAsync(newProduct);

    //    return newProduct.ToProductModel();
    //}

    //public async Task UpdateAsync(string id, EditProductModel productModel)
    //{
    //    var existingProduct = await repositories.Products
    //        .FindAsync(id)
    //        .EnsureIsNotNull(id);
      
    //    existingProduct.Update(productModel.Name, productModel.Price, productModel.Stock);

    //    await repositories.Products.UpdateAsync(existingProduct);
    //}

    public async Task DeleteAsync(string id)
    {
        if (await repositories.Products.ExistsAsync(id) is false)
        {
            throw ProductErrors.NotFound(id);
        }

        await repositories.Products.DeleteAsync(id);
    }
}