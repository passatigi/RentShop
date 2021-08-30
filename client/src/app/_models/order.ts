import { OrderProduct } from "./orderProduct";

export interface Order {
    id: number;
    orderProducts: OrderProduct[];
    orderDate: Date;
    requiredDate: Date;
    shippedDate: Date;
    requiredReturnDate: Date;
    returnDate: Date;
    status: string;
    comments: string;
    customeId: number;
    customer?: any;
    deliverymanId: number;
    deliveryman?: any;
    adressId: number;
    shippedAdress: string;
    returnAdress: string;
}