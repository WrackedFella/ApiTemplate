using AutoMapper;
using Storefront.Api.AutoMapper.Profiles;

namespace Storefront.Api.AutoMapper
{
  public class AutoMapperConfig
  {
	public static void RegisterAutoMapperProfiles()
	{
	  Mapper.Initialize(x =>
	  {
		x.AddProfile<GlobalProfile>();
		x.AddProfile<ProductProfile>();
	  });
	}
  }
}
