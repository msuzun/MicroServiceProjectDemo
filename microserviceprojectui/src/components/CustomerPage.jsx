import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import api from '../utils/Api.jsx';

const CustomerPage = () => {
  const [customers, setCustomers] = useState([]);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');

  // Fetch All Customers
  const fetchCustomers = async () => {
    try {
      const response = await api.get('/customer');
      setCustomers(response.data);
    } catch (error) {
      alert('Failed to fetch customers');
    }
  };

  useEffect(() => {
    fetchCustomers(); // Sayfa yüklendiğinde müşterileri getir
  }, []);

  // Create Customer
  const handleCreateCustomer = async () => {
    try {
      const response = await api.post('/customer', { name, email });
      alert(`Customer created with ID: ${response.data}`);
      setCreateModalOpen(false);
      fetchCustomers();
    } catch (error) {
      alert('Failed to create customer');
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
        <h1 className="text-xl font-bold mb-4">Customer Management</h1>
        <button
          onClick={() => setCreateModalOpen(true)}
          className="bg-blue-500 text-white px-4 py-2 rounded-md mb-4"
        >
          Create Customer
        </button>
        <table className="w-full bg-white shadow-md rounded-md">
          <thead>
            <tr className="bg-gray-200">
              <th className="p-2 w-1/4 text-center align-middle">Customer ID</th>
              <th className="p-2 w-1/4 text-center align-middle">Name</th>
              <th className="p-2 w-1/4 text-center align-middle">Email</th>
              <th className="p-2 w-1/4 text-center align-middle">Actions</th>
            </tr>
          </thead>
          <tbody>
            {customers.map((customer, index) => (
              <tr key={index} className="border-b">
                <td className="p-2 text-center align-middle">{customer.customerId}</td>
                <td className="p-2 text-center align-middle">{customer.name}</td>
                <td className="p-2 text-center align-middle">{customer.email}</td>
                <td className="p-2 text-center align-middle">
                  <button className="bg-green-500 text-white px-2 py-1 rounded-md">
                    Details
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Create Customer Modal */}
      {isCreateModalOpen && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-md shadow-md">
            <h2 className="text-lg font-semibold mb-4">Create Customer</h2>
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="border p-2 mb-2 w-full"
            />
            <button
              onClick={handleCreateCustomer}
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

export default CustomerPage;
