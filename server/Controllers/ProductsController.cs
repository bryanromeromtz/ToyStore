using Microsoft.AspNetCore.Mvc;
using ToyStore.Models;
using ToyStore.Repositories;

namespace ToyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista completa de productos.
        /// </summary>
        /// <remarks>
        /// Devuelve un listado de todos los productos almacenados en la base de datos.
        /// </remarks>
        /// <response code="200">Devuelve la lista de productos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto por su identificador.
        /// </summary>
        /// <param name="id">Id del producto a buscar</param>
        /// <returns>El producto solicitado</returns>
        /// <response code="200">Producto encontrado</response>
        /// <response code="404">No existe ningún producto con ese Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Objeto producto a crear</param>
        /// <returns>El producto creado</returns>
        /// <response code="201">Producto creado correctamente</response>
        /// <response code="400">El modelo no es válido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">Identificador del producto a actualizar</param>
        /// <param name="product">Datos del producto actualizados</param>
        /// <response code="204">Actualizado correctamente</response>
        /// <response code="400">El modelo no es válido o el Id no coincide</response>
        /// <response code="404">El producto no existe</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest("El Id de la URL no coincide con el del objeto.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _repository.UpdateAsync(product);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina un producto por su Id.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar</param>
        /// <response code="204">Eliminado correctamente</response>
        /// <response code="404">No existe un producto con ese Id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
