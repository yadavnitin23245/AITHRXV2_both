import { Role } from './role';

export class Account {
    id: string;
    title: string;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
    jwtToken?: string;
    token:any;
    userName: string;					
	phoneNumber: string;
    userrole:any;
    addresslineone: string;
    addresslinetwo: string;
    city: string;
    country: string;
    state: string;
    gstNumber: string;
    eFTinfo: string;
}