import { OrderProduct } from "./orderProduct";

    export interface CreateOrder {
        orderProducts: OrderProduct[];
        requiredDate: Date;
        requiredReturnDate: Date;
        comments: string;
        customerId: number;
        shippedAddressId: number;
        returnAddressId: number;
        totalPrice: number;
    }