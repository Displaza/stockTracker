
export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: {
    id: string;
    username: string;
    email: string;
    roles: string[];
  };
}

//will move this to separate file later
export interface User {
  id: string;
  username: string;
  email: string;
  roles: string[];
}