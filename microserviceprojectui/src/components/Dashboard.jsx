import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

const Dashboard = () => {
  const [loginTime, setLoginTime] = useState('');

  useEffect(() => {
    const currentTime = new Date().toLocaleString();
    setLoginTime(currentTime);
  }, []);

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
            <Link to="/products">Product</Link>
          </li>
        </ul>
      </nav>

      <div className="p-6">
        <h1 className="text-xl font-bold mb-4">Merhaba, sisteme giriş saatiniz: {loginTime}</h1>
      </div>
    </div>
  );
};

export default Dashboard;