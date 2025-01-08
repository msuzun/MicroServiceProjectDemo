import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import api from '../utils/Api.jsx';

const ProductPage = () => {
  const [products, setProducts] = useState([]);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [isUpdateModalOpen, setUpdateModalOpen] = useState(false);
  const [name, setName] = useState('');
  const [price, setPrice] = useState('');
  const [stock, setStock] = useState('');

  // Fetch All Products
  const fetchProducts = async () => {
    try {
      const response = await api.get('/product');
      setProducts(response.data);
      console.log(response.data);
    } catch (error) {
      alert('Failed to fetch products');
    }
  };

  useEffect(() => {
    fetchProducts(); // Sayfa yüklendiğinde ürünleri getir
  }, []);

  // Create Product
  const handleCreateProduct = async () => {
    try {
      const response = await api.post('/product', {
        name,
        price: parseFloat(price),
        stock: parseInt(stock),
      });
      alert(`Product created with ID: ${response.data}`);
      setCreateModalOpen(false);
      fetchProducts();
    } catch (error) {
      alert('Failed to create product');
    }
  };

  // Update Product
  const handleUpdateProduct = async () => {
    try {
      const response = await api.get(`/product/get-id-by-name/${selectedProduct.name}`);
      const productId = response.data;

      await api.put(`/product/${productId}`, {
        name,
        price: parseFloat(price),
        stock: parseInt(stock),
      });

      alert('Product updated successfully');
      setUpdateModalOpen(false);
      fetchProducts();
    } catch (error) {
      console.error(error);
      alert('Failed to update product');
    }
  };

  // Delete Product
  const handleDeleteProduct = async (name) => {
    try {
      const response = await api.get(`/product/get-id-by-name/${name}`);
      const productId = response.data;

      await api.delete(`/product/${productId}`);
      alert('Product deleted successfully');
      fetchProducts();
    } catch (error) {
      console.error(error);
      alert('Failed to delete product');
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      {/* Navbar */}
      <nav className="bg-blue-500 text-white px-4 py-2 flex justify-center">
  <ul className="flex space-x-6">
    <li className="hover:underline cursor-pointer">
      <Link to="/order">Order</Link>
    </li>
    <li className="hover:underline cursor-pointer">
      <Link to="/customer">Customer</Link>
    </li>
    <li className="hover:underline cursor-pointer">
      <Link to="/product">Product</Link>
    </li>
  </ul>
</nav>

      <div className="p-6">
        <h1 className="text-xl font-bold mb-4">Product Management</h1>
        <button
          onClick={() => setCreateModalOpen(true)}
          className="bg-blue-500 text-white px-4 py-2 rounded-md mb-4"
        >
          Create Product
        </button>
        <table className="w-full bg-white shadow-md rounded-md">
          <thead>
            <tr className="bg-gray-200">
              <th className="p-2 w-1/4 text-center align-middle">Name</th>
              <th className="p-2 w-1/4 text-center align-middle">Price</th>
              <th className="p-2 w-1/4 text-center align-middle">Stock</th>
              <th className="p-2 w-1/4 text-center align-middle">Actions</th>
            </tr>
          </thead>
          <tbody>
            {products.map((product, index) => (
              <tr key={index} className="border-b">
                <td className="p-2 text-center align-middle">{product.name}</td>
                <td className="p-2 text-center align-middle">${product.price}</td>
                <td className="p-2 text-center align-middle">{product.stock}</td>
                <td className="p-2 text-center align-middle space-x-2">
                  <button
                    onClick={() => {
                      setSelectedProduct(product);
                      setUpdateModalOpen(true);
                      setName(product.name);
                      setPrice(product.price);
                      setStock(product.stock);
                    }}
                    className="bg-yellow-500 text-white px-2 py-1 rounded-md"
                  >
                    Update
                  </button>
                  <button
                    onClick={() => handleDeleteProduct(product.name)}
                    className="bg-red-500 text-white px-2 py-1 rounded-md"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Create Product Modal */}
      {isCreateModalOpen && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-md shadow-md">
            <h2 className="text-lg font-semibold mb-4">Create Product</h2>
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <input
              type="number"
              placeholder="Price"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <input
              type="number"
              placeholder="Stock"
              value={stock}
              onChange={(e) => setStock(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <button
              onClick={handleCreateProduct}
              className="bg-blue-500 text-white px-4 py-2 rounded-md mr-2"
            >
              Save
            </button>
            <button
              onClick={() => setCreateModalOpen(false)}
              className="bg-gray-500 text-white px-4 py-2 rounded-md"
            >
              Cancel
            </button>
          </div>
        </div>
      )}

      {/* Update Product Modal */}
      {isUpdateModalOpen && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-md shadow-md">
            <h2 className="text-lg font-semibold mb-4">Update Product</h2>
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <input
              type="number"
              placeholder="Price"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <input
              type="number"
              placeholder="Stock"
              value={stock}
              onChange={(e) => setStock(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <button
              onClick={handleUpdateProduct}
              className="bg-blue-500 text-white px-4 py-2 rounded-md mr-2"
            >
              Save
            </button>
            <button
              onClick={() => setUpdateModalOpen(false)}
              className="bg-gray-500 text-white px-4 py-2 rounded-md"
            >
              Cancel
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default ProductPage;
