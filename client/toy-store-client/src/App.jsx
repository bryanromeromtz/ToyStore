import { useState } from 'react';
import ProductList from './ProductList';
import ProductForm from './ProductForm';

export default function App() {
  const [mode, setMode] = useState('list'); 
  const [editingProduct, setEditingProduct] = useState(null);

  const goList = () => { setMode('list'); setEditingProduct(null); };
  const goCreate = () => { setMode('create'); setEditingProduct(null); };
  const goEdit = (p) => { setEditingProduct(p); setMode('edit'); };

  if (mode === 'create') return <ProductForm onCancel={goList} onSaved={goList} />;
  if (mode === 'edit')   return <ProductForm editing initial={editingProduct} onCancel={goList} onSaved={goList} />;
  return <ProductList onCreate={goCreate} onEdit={goEdit} />;
}
