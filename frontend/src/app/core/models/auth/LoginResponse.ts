export interface LoginResponse {
  token: string;
  expiresAt: Date;

  // user: {
  //   // id: string;
  //   username: string;
  //   // email: string;
  //   roles: string[];
  // };
}