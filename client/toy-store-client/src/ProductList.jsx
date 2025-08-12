import { useEffect, useState } from 'react';
import { api } from './api';

export default function ProductList({ onCreate, onEdit }) {
  const [products, setProducts] = useState([]);

  const load = async () => {
    const { data } = await api.get('/products');
    setProducts(data);
  };

  useEffect(() => { load(); }, []);

  const handleDelete = async (id) => {
    if (!confirm('¿Seguro que deseas eliminar este producto?')) return;
    await api.delete(`/products/${id}`);
    await load();
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h3>Productos</h3>
        <button className="btn btn-primary" onClick={onCreate}>Nuevo</button>
      </div>
      <div className="table-responsive">
        <table className="table table-striped align-middle">
          <thead>
            <tr>
              <th>Id</th>
              <th>Nombre</th>
              <th>Compañía</th>
              <th>Edad</th>
              <th>Precio</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {products.map(p => (
              <tr key={p.id}>
                <td>{p.id}</td>
                <td>{p.name}</td>
                <td>{p.company}</td>
                <td>{p.ageRestriction ?? '-'}</td>
                <td>${p.price.toFixed(2)}</td>
                <td className="text-end">
                  <button className="btn btn-sm btn-outline-secondary me-2" onClick={() => onEdit(p)}>Editar</button>
                  <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(p.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {products.length === 0 && (
              <tr><td colSpan="6" className="text-center py-4">Sin productos</td></tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
