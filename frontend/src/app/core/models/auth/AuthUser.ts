export interface AuthUser {
  //really all of the data is in the token so I'll just parse it from there.

  // id: string;
  username: string;
  // email: string;
  roles: string[];
}