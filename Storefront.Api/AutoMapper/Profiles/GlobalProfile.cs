using AutoMapper;
using Storefront.Api.Core;
using Storefront.Dal.Core;

namespace Storefront.Api.AutoMapper.Profiles
{
	public class GlobalProfile : Profile
	{
		public GlobalProfile()
		{
			CreateMap<ModelBase, EntityBase>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<EntityBase, ModelBase>();
		}
	}
}
