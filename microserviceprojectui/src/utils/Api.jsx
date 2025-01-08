import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5020', // API Gateway adresiniz
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default api;
