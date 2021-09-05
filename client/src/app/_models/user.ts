import { Address } from "./address";

export interface User {
    id: number
    fullName: string
    email: string
    phoneNumber: string
    token: string
    addresses: Address[]
    roles: string[];
}
  