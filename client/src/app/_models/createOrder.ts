import { OrderProduct } from "./orderProduct";

    export interface CreateOrder {
        orderProducts: OrderProduct[];
        requiredDate: Date;
        requiredReturnDate: Date;
        comments: string;
        customeId: number;
        shippedAdress: string;
        returnAdress: string;
    }