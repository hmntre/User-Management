export interface User {
  id: string;
  name: string;
  email: string;
  role: string;
  isActive: boolean;
  createdAt: string;
  lastLoginAt?: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  success: boolean;
  message: string;
  token?: string;
  user?: User;
}

export interface CreateUserRequest {
  name: string;
  email: string;
  password: string;
  role?: string;
}

export interface UpdateUserRequest {
  name: string;
  email: string;
  role?: string;
  isActive?: boolean;
}

export interface ApiResponse<T> {
  data?: T;
  message?: string;
  errors?: string[];
}