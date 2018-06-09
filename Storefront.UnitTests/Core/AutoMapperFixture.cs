﻿using AutoMapper;
using Storefront.Api.AutoMapper;
using System;
using Xunit;

namespace Storefront.UnitTests.Core
{
  public class AutoMapperFixture : IDisposable
  {
	public AutoMapperFixture()
	{
	  AutoMapperConfig.RegisterAutoMapperProfiles();
	}

	public void Dispose()
	{
	  Mapper.Reset();
	}
  }

  [CollectionDefinition("AutoMapper")]
  public class AutoMapperCollection : ICollectionFixture<AutoMapperFixture>
  {
	// Empty, used to attach new ICollectionFixture<> definitions to this collection. i.e. you can
	// have multiple Fixtures to a Collection. This class lets you do it.
  }
}
