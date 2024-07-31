using EcommerceApi.Dtos.Product;
using EcommerceApi.Models;

namespace EcommerceApi.Mappers
{
	public static class ProductMapper
	{
		public static Product FromCreateDtoToProduct(this CreateProductDto productDto)
		{
			return new Product
			{
				Name = productDto.Name,
				Description = productDto.Description,
				Price = productDto.Price,
				Currency = productDto.Currency,
				Category = productDto.Category,
				StockAmount = productDto.StockAmount,
				SellerId = productDto.SellerId,
			};
		}

		public static Product FromUpdateDtoToProduct(this UpdateProductDto productDto)
		{
			return new Product
			{
				Name = productDto.Name,
				Description = productDto.Description,
				Price = productDto.Price,
				Currency = productDto.Currency,
				Category = productDto.Category,
				StockAmount = productDto.StockAmount,
			};
		}

		public static CustomerViewProductDto ToCustomerViewProductDto(this Product product)
		{
			return new CustomerViewProductDto
			{
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Currency = product.Currency,
				Category = product.Category,
				Rating = product.Rating,
				StockAmount = product.StockAmount,
				Comments = product.Comments,
				SellerName = product.Seller.Name,
			};
		}
	}
}