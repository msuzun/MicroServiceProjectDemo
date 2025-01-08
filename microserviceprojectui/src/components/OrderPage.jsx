import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import api from '../utils/Api.jsx';

const OrderPage = () => {
  const [orders, setOrders] = useState([]);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [customerId, setCustomerId] = useState('');
  const [newProductName, setNewProductName] = useState('');
  const [newQuantity, setNewQuantity] = useState('');
  const [newTotalPrice, setNewTotalPrice] = useState(''); // Yeni Total Price inputu
  const [customers, setCustomers] = useState([]); // Müşteri listesi
  const [products, setProducts] = useState([]);   // Ürün listesi

  // Fetch All Orders
  const fetchOrders = async () => {
    try {
      const response = await api.get('/order');
      setOrders(response.data);
    } catch (error) {
      alert('Failed to fetch orders');
    }
  };

  // Fetch Customers
  const fetchCustomers = async () => {
    try {
      const response = await api.get('/customer');
      setCustomers(response.data);
    } catch (error) {
      alert('Failed to fetch customers');
    }
  };

  // Fetch Products
  const fetchProducts = async () => {
    try {
      const response = await api.get('/product');
      setProducts(response.data);
    } catch (error) {
      alert('Failed to fetch products');
    }
  };

  useEffect(() => {
    fetchOrders();
    fetchCustomers();
    fetchProducts();
  }, []);

  // Create Order
  const handleCreateOrder = async () => {
    try {
        const responseId = await api.get(`/product/get-id-by-name/${newProductName}`);
        const newProductId = responseId.data
        console.log(newProductId)
      const payload = {
        customerId: parseInt(customerId),
        orderItems: [
          {
            productId: parseInt(newProductId),
            quantity: parseInt(newQuantity),
            totalPrice: parseFloat(newTotalPrice),
          },
        ],
      };

      console.log("Payload being sent to API:", payload); // Gönderilen payload'u kontrol et

      const response = await api.post('/order', payload);
      console.log("Order response:", response);
      alert(`Order created with ID: ${response.data}`);
      setCreateModalOpen(false);
      fetchOrders();
    } catch (error) {
      alert('Failed to create order');
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
        <h1 className="text-xl font-bold mb-4">Order Management</h1>
        <button
          onClick={() => setCreateModalOpen(true)}
          className="bg-blue-500 text-white px-4 py-2 rounded-md mb-4"
        >
          Create Order
        </button>
        <table className="w-full bg-white shadow-md rounded-md">
          <thead>
            <tr className="bg-gray-200">
              <th className="p-2 w-1/4 text-center align-middle">Order ID</th>
              <th className="p-2 w-1/4 text-center align-middle">Customer ID</th>
              <th className="p-2 w-1/4 text-center align-middle">Order Items</th>
              <th className="p-2 w-1/4 text-center align-middle">Actions</th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order, index) => (
              <tr key={index} className="border-b">
                <td className="p-2 text-center align-middle">{order.orderId}</td>
                <td className="p-2 text-center align-middle">{order.customerId}</td>
                <td className="p-2 text-center align-middle">
                  <ul>
                    {order.orderItems.map((item, i) => (
                      <li key={i}>
                        Product ID: {item.productId}, Quantity: {item.quantity}, Total Price: ${item.totalPrice}
                      </li>
                    ))}
                  </ul>
                </td>
                <td className="p-2 text-center align-middle space-x-2">
                  <button className="bg-green-500 text-white px-2 py-1 rounded-md">
                    Details
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Create Order Modal */}
      {isCreateModalOpen && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-md shadow-md">
            <h2 className="text-lg font-semibold mb-4">Create Order</h2>
            <label className="block mb-2">Customer</label>
            <select
              value={customerId}
              onChange={(e) => setCustomerId(e.target.value)}
              className="border p-2 mb-4 w-full"
            >
              <option value="">Select Customer</option>
              {customers.map((customer,i) => (
                <option key={i} value={customer.customerId}>
                  {customer.name}
                </option>
              ))}
            </select>
            <h3 className="text-md font-semibold mb-2">Order Items</h3>
            <div className="mb-4">
              <label className="block">Product</label>
              <select
                value={newProductName}
                onChange={(e) => setNewProductName(e.target.value)}
                className="border p-2 mb-2 w-full"
              >
                <option value="">Select Product</option>
                {products.map((product,i) => (
                  <option key={i} value={product.name}>
                    {product.name}
                  </option>
                ))}
              </select>
              <label className="block">Quantity</label>
              <input
                type="number"
                value={newQuantity}
                onChange={(e) => setNewQuantity(e.target.value)}
                className="border p-2 mb-2 w-full"
              />
              <label className="block">Total Price</label>
              <input
                type="number"
                value={newTotalPrice}
                onChange={(e) => setNewTotalPrice(e.target.value)}
                className="border p-2 mb-2 w-full"
              />
             
            </div>
            <button
              onClick={handleCreateOrder}
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
    </div>
  );
};

export default OrderPage;
