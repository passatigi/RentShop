import { Address } from "./address";
import { RealProduct } from "./realProduct";
import { User } from "./user";

export interface Order {
    id?: number;
    orderDate?: Date;

    requiredDate: Date;
    shippedDate?: Date;
    
    requiredReturnDate: Date;
    returnDate?: Date;

    status?: string;
    comments?: string;

    
    shippedAddressId: number;
    shippedAddress?: Address;
    returnAddressId: number;
    returnAddress?: Address;

    deliverymanId?: number;
    deliveryman?: User;
    deliverymanReturnId?: number;
    deliverymanReturn?: User;
    customerId: number;
    customer?: User;
    
    orderProducts: RealProduct[];
    totalPrice?: number;
}