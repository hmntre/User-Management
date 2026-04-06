import axios, { AxiosInstance, AxiosResponse } from 'axios';
// import { User, LoginRequest, AuthResponse, CreateUserRequest, UpdateUserRequest } from '../types';
import { User, CreateUserRequest, UpdateUserRequest } from '../types';

class ApiService {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: '/api',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // // Add request interceptor to include auth token
    // this.api.interceptors.request.use((config) => {
    //   const token = localStorage.getItem('token');
    //   if (token) {
    //     config.headers.Authorization = `Bearer ${token}`;
    //   }
    //   return config;
    // });

    // // Add response interceptor for error handling
    // this.api.interceptors.response.use(
    //   (response) => response,
    //   (error) => {
    //     if (error.response?.status === 401) {
    //       localStorage.removeItem('token');
    //       window.location.href = '/login';
    //     }
    //     return Promise.reject(error);
    //   }
    // );
  }

  // // Authentication
  // async login(credentials: LoginRequest): Promise<AuthResponse> {
  //   const response: AxiosResponse<AuthResponse> = await this.api.post('/auth/login', credentials);
  //   return response.data;
  // }

  // async getCurrentUser(): Promise<User> {
  //   const response: AxiosResponse<User> = await this.api.get('/auth/me');
  //   return response.data;
  // }

  // Users CRUD
  async getUsers(): Promise<User[]> {
    const response: AxiosResponse<User[]> = await this.api.get('/users');
    return response.data;
  }

  async getUserById(id: string): Promise<User> {
    const response: AxiosResponse<User> = await this.api.get(`/users/${id}`);
    return response.data;
  }

  async createUser(user: CreateUserRequest): Promise<User> {
    const response: AxiosResponse<User> = await this.api.post('/users', user);
    return response.data;
  }

  async updateUser(id: string, user: UpdateUserRequest): Promise<void> {
    await this.api.put(`/users/${id}`, user);
  }

  async deleteUser(id: string): Promise<void> {
    await this.api.delete(`/users/${id}`);
  }

  async toggleUserStatus(id: string): Promise<void> {
    await this.api.patch(`/users/${id}/toggle`);
  }
}

export const apiService = new ApiService();