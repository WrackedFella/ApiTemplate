using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storefront.Dal.Core;
using Storefront.Dal.DbContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storefront.Api.Core
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class ControllerBase<TEntity, TModel> : Controller
		where TEntity : EntityBase
		where TModel : ModelBase
	{
		protected readonly StorefrontDbContext Context;

		// ToDo: Implement search using IPredicateBuilder, or Get(params TModel searchParams).
		protected ControllerBase(StorefrontDbContext context)
		{
			this.Context = context;
		}

		[HttpGet]
		public virtual async Task<ActionResult<IEnumerable<TModel>>> Get()
		{
			return await this.Context.Set<TEntity>().ProjectTo<TModel>().ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TModel>> Get(Guid id)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			var result = Mapper.Map<TModel>(record);
			return result;
		}

		[HttpPost]
		public async Task<ActionResult<CreatedAtRouteResult>> Post([FromBody] TModel model)
		{
			var record = Mapper.Map<TEntity>(model);
			this.Context.Set<TEntity>().Add(record);
			await this.Context.SaveChangesAsync();

			return CreatedAtRoute(new { id = record.Id }, record);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AcceptedAtRouteResult>> Put(Guid id, [FromBody] TModel model)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			Mapper.Map(model, record);

			this.Context.Set<TEntity>().Update(record);
			await this.Context.SaveChangesAsync();
			return AcceptedAtRoute(new { id = record.Id }, model);
		}

		[HttpPatch("{id}")]
		public async Task<ActionResult<AcceptedAtRouteResult>> Patch(Guid id, [FromBody]JsonPatchDocument<TModel> patch)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			var mappedRecord = Mapper.Map<TModel>(record);
			patch.ApplyTo(mappedRecord, this.ModelState);

			Mapper.Map(mappedRecord, record);

			return AcceptedAtRoute(new { id = record.Id }, record);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<NoContentResult>> Delete(Guid id)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			this.Context.Set<TEntity>().Remove(record);
			await this.Context.SaveChangesAsync();
			return NoContent();
		}
	}

	[ApiController]
	[Route("api/[controller]")]
	public abstract class ControllerBase<TEntity, TModel, TListModel> : Controller
		where TEntity : EntityBase
		where TModel : ModelBase
		where TListModel : ModelBase
	{
		protected readonly StorefrontDbContext Context;

		protected ControllerBase(StorefrontDbContext context)
		{
			this.Context = context;
		}

		[HttpGet]
		public virtual async Task<ActionResult<IEnumerable<TListModel>>> GetAll()
		{
			return await this.Context.Set<TEntity>().ProjectTo<TListModel>().ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TModel>> GetById(Guid id)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			var result = Mapper.Map<TModel>(record);
			return result;
		}

		[HttpPost]
		public async Task<ActionResult<CreatedAtRouteResult>> Create([FromBody] TModel model)
		{
			var record = Mapper.Map<TEntity>(model);
			this.Context.Set<TEntity>().Add(record);
			await this.Context.SaveChangesAsync();

			return CreatedAtRoute(new { id = record.Id }, record);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<AcceptedAtRouteResult>> Update(Guid id, [FromBody] TModel model)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			Mapper.Map(model, record);

			this.Context.Set<TEntity>().Update(record);
			await this.Context.SaveChangesAsync();
			return AcceptedAtRoute(new { id = model.Id }, model);
		}

		[HttpPatch("{id}")]
		public async Task<ActionResult<AcceptedAtRouteResult>> Patch(Guid id, [FromBody]JsonPatchDocument<TModel> patch)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			var mappedRecord = Mapper.Map<TModel>(record);
			patch.ApplyTo(mappedRecord, this.ModelState);

			Mapper.Map(mappedRecord, record);

			return AcceptedAtRoute(new { id = record.Id }, record);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<NoContentResult>> Delete(Guid id)
		{
			var record = await this.Context.Set<TEntity>().FindAsync(id);
			if (record == null)
			{
				return NotFound();
			}

			this.Context.Set<TEntity>().Remove(record);
			await this.Context.SaveChangesAsync();
			return NoContent();
		}
	}
}
