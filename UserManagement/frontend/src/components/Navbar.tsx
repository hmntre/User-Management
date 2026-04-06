import React from 'react';
import { Link } from 'react-router-dom';

const Navbar: React.FC = () => {
  return (
    <nav className="bg-white shadow-lg">
      <div className="max-w-7xl mx-auto px-4">
        <div className="flex justify-between items-center h-16">
          <div className="flex items-center space-x-4">
            <Link to="/" className="text-xl font-bold text-gray-800">
              User Management
            </Link>
          </div>

          <div className="flex items-center space-x-4">
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;