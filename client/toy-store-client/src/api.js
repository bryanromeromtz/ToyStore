import axios from 'axios';

export const api = axios.create({
  baseURL: 'https://localhost:7180/api', // cambia por tu puerto/HTTP si no usas HTTPS
  // baseURL: 'http://localhost:5180/api'
});
