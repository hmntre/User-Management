import { useState, useEffect } from 'react';
import { User, CreateUserRequest, UpdateUserRequest } from '../types';
import { apiService } from '../services/api';
import toast from 'react-hot-toast';

export const useUsers = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchUsers = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await apiService.getUsers();
      setUsers(data);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch users';
      setError(errorMessage);
      toast.error(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  const createUser = async (userData: CreateUserRequest) => {
    try {
      const newUser = await apiService.createUser(userData);
      setUsers(prev => [...prev, newUser]);
      toast.success('User created successfully');
      return newUser;
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to create user';
      toast.error(errorMessage);
      throw err;
    }
  };

  const updateUser = async (id: string, userData: UpdateUserRequest) => {
    try {
      await apiService.updateUser(id, userData);
      setUsers(prev => prev.map(user =>
        user.id === id ? { ...user, ...userData } : user
      ));
      toast.success('User updated successfully');
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to update user';
      toast.error(errorMessage);
      throw err;
    }
  };

  const deleteUser = async (id: string) => {
    try {
      await apiService.deleteUser(id);
      setUsers(prev => prev.filter(user => user.id !== id));
      toast.success('User deleted successfully');
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to delete user';
      toast.error(errorMessage);
      throw err;
    }
  };

  const toggleUserStatus = async (id: string) => {
    try {
      await apiService.toggleUserStatus(id);
      setUsers(prev => prev.map(user =>
        user.id === id ? { ...user, isActive: !user.isActive } : user
      ));
      toast.success('User status updated successfully');
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to update user status';
      toast.error(errorMessage);
      throw err;
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return {
    users,
    loading,
    error,
    fetchUsers,
    createUser,
    updateUser,
    deleteUser,
    toggleUserStatus,
  };
};