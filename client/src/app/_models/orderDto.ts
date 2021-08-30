import { RealProduct } from "./realProduct";

export interface OrderDto {
    id: number;
    orderProducts: RealProduct[];
    orderDate: Date;
    requiredDate: Date;
    shippedDate: Date;
    requiredReturnDate: Date;
    returnDate: Date;
    status: string;
    comments: string;
    customeId: number;
    deliverymanId: number;
    adressId: number;
    shippedAdress: string;
    returnAdress: string;
}