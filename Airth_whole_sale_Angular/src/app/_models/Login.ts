import { Role } from './role';

export class Login {
    email: string;
    password: string;
    returnUrl: string;
    role: Role;
    token:any;
}