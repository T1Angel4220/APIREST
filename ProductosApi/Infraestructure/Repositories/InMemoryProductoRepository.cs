using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
	public class InMemoryProductoRepository : IProductoRepository
	{
		private readonly List<Producto> _productos = new()
		{
			new Producto { Id = 1, Nombre = "Laptop", Precio = 1200.50m, Disponible = true },
			new Producto { Id = 2, Nombre = "Parlante", Precio = 25.00m, Disponible = true },
			new Producto { Id = 3, Nombre = "Teclado", Precio = 45.99m, Disponible = false }
		};

		public IEnumerable<Producto> GetAll() => _productos;

		public Producto? GetById(int id) => _productos.FirstOrDefault(p => p.Id == id);

		public void Add(Producto producto)
		{
			producto.Id = _productos.Count == 0 ? 1 : _productos.Max(p => p.Id) + 1;
			_productos.Add(producto);
		}

		public void Update(Producto producto)
		{
			var existing = _productos.FirstOrDefault(p => p.Id == producto.Id);
			if (existing is not null)
			{
				existing.Nombre = producto.Nombre;
				existing.Precio = producto.Precio;
				existing.Disponible = producto.Disponible;
			}
		}

		public void Delete(int id)
		{
			var producto = _productos.FirstOrDefault(p => p.Id == id);
			if (producto is not null)
				_productos.Remove(producto);
		}
	}
}
