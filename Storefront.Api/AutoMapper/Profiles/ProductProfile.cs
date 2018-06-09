using AutoMapper;
using Storefront.Api.Core;
using Storefront.Api.Models;
using Storefront.Dal.Core;
using Storefront.Dal.Entities;
using System.Linq;

namespace Storefront.Api.AutoMapper.Profiles
{
  public class ProductProfile : Profile
  {
	public ProductProfile()
	{
	  CreateMap<Product, ProductModel>()
		  .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
		  .IncludeBase<EntityBase, ModelBase>();
	  CreateMap<ProductModel, Product>()
		  .IncludeBase<ModelBase, EntityBase>();

	  CreateMap<Category, CategoryModel>()
		  .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.ToList()))
		  .IncludeBase<EntityBase, ModelBase>();
	  CreateMap<CategoryModel, Category>()
		  .IncludeBase<ModelBase, EntityBase>();
	}
  }
}
