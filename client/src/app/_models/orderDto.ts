import { Address } from "./address";
import { RealProduct } from "./realProduct";
import { User } from "./user";

export interface OrderDto {
    id: number;
    orderDate: Date;
    requiredDate: Date;
    shippedDate?: Date;
    requiredReturnDate: Date;
    returnDate?: Date;
    status: string;
    comments: string;
    deliveryman: User;
    deliverymanReturn: User;
    customer: User;
    shippedAddress: Address;
    returnAddress: Address;
    orderProducts: RealProduct[];
    returnAddressId: number;
    shippedAddressId: number;
    customerId: number;
    deliverymanId: number;
    deliverymanReturnId: number;
    totalPrice: number;
}