import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { api } from './api';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';

const schema = yup.object({
  id: yup.number().nullable(),
  name: yup.string().required('El nombre es obligatorio.').max(50, 'Máx 50 caracteres.'),
  description: yup.string().nullable().max(100, 'Máx 100 caracteres.'),
  ageRestriction: yup.number().nullable()
    .transform(v => (isNaN(v) ? null : v))
    .min(0, 'Mínimo 0').max(100, 'Máximo 100'),
  company: yup.string().required('La compañía es obligatoria.').max(50, 'Máx 50 caracteres.'),
  price: yup.number().typeError('Precio inválido.')
    .required('El precio es obligatorio.')
    .min(1, 'Mínimo $1').max(1000, 'Máximo $1000')
});

export default function ProductForm({ editing, initial, onCancel, onSaved }) {
  const { register, handleSubmit, reset, formState: { errors, isSubmitting } } =
    useForm({ resolver: yupResolver(schema), defaultValues: initial });

  useEffect(() => { reset(initial || {}); }, [initial, reset]);

  const onSubmit = async (values) => {
    if (editing) {
      await api.put(`/products/${values.id}`, values);
    } else {
      await api.post('/products', values);
    }
    onSaved?.();
  };

  return (
    <div className="container py-4">
      <h3>{editing ? 'Editar producto' : 'Nuevo producto'}</h3>
      <form className="mt-3" onSubmit={handleSubmit(onSubmit)}>
        {editing && (
          <div className="mb-3">
            <label className="form-label">Id</label>
            <input className="form-control" disabled {...register('id')} />
          </div>
        )}

        <div className="mb-3">
          <label className="form-label">Nombre</label>
          <input className="form-control" {...register('name')} />
          {errors.name && <div className="text-danger">{errors.name.message}</div>}
        </div>

        <div className="mb-3">
          <label className="form-label">Descripción</label>
          <input className="form-control" {...register('description')} />
          {errors.description && <div className="text-danger">{errors.description.message}</div>}
        </div>

        <div className="mb-3">
          <label className="form-label">Restricción de edad</label>
          <input type="number" className="form-control" {...register('ageRestriction')} />
          {errors.ageRestriction && <div className="text-danger">{errors.ageRestriction.message}</div>}
        </div>

        <div className="mb-3">
          <label className="form-label">Compañía</label>
          <input className="form-control" {...register('company')} />
          {errors.company && <div className="text-danger">{errors.company.message}</div>}
        </div>

        <div className="mb-3">
          <label className="form-label">Precio</label>
          <input type="number" step="0.01" className="form-control" {...register('price')} />
          {errors.price && <div className="text-danger">{errors.price.message}</div>}
        </div>

        <div className="d-flex gap-2">
          <button className="btn btn-primary" disabled={isSubmitting}>
            {editing ? 'Guardar cambios' : 'Crear'}
          </button>
          <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancelar</button>
        </div>
      </form>
    </div>
  );
}
