﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Models;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// 商品の一覧を取得します。
        /// </summary>
        /// <returns></returns>
        /// <response code="200">商品の一覧を返します。</response>
        /// <response code="404">商品が登録されていません。</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> Get()
        {
            var entity = await _repository.GetAsync();
            if (entity == null || entity.Count == 0)
            {
                return NotFound();
            }

            //var response = new List<ProductResponse>();
            //foreach (var product in entity)
            //{
            //    response.Add(new ProductResponse
            //    {
            //        ProductId = product.ProductId,
            //        Desc = product.Desc,
            //        Name = product.Name,
            //        Price = product.Price
            //    });
            //}
            var response = _mapper.Map<IEnumerable<ProductResponse>>(entity);
            return Ok(response);
        }

        /// <summary>
        /// 商品を ID で検索します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <returns></returns>
        /// <response code="200">検索した商品を返します。</response>
        /// <response code="404">指定された商品 ID が登録されていません。</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> Get(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProductResponse>(entity);
            return Ok(response);
        }

        /// <summary>
        /// 商品を追加します。
        /// </summary>
        /// <param name="product">商品</param>
        /// <returns></returns>
        /// <response code="201">追加した商品を返します。</response>
        /// <response code="400">追加する商品が指定されていません。</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponse>> Post([FromBody] ProductRequest product)
        {
            var entity = _mapper.Map<Product>(product);
            await _repository.InsertAsync(entity);

            var response = _mapper.Map<ProductResponse>(entity);
            return CreatedAtAction(nameof(Get), new { id = response.ProductId }, response);
        }

        /// <summary>
        /// 商品を編集します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <param name="product">商品</param>
        /// <returns></returns>
        /// <response code="204">商品を編集しました。</response>
        /// <response code="404">編集する商品が登録されていません。</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(long id, [FromBody] ProductRequest product)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _mapper.Map(product, entity);

            await _repository.UpdateAsync();
            return NoContent();
        }

        /// <summary>
        /// 商品を削除します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <returns></returns>
        /// <response code="204">商品を削除しました。</response>
        /// <response code="404">削除する商品が登録されていません。</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(entity);
            return NoContent();
        }
    }
}
