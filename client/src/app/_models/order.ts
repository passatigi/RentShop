import { OrderProduct } from "./orderProduct";
import { RealProduct } from "./realProduct";
import { User } from "./user";

export interface Order {
    id: number
    orderDate: string
    requiredDate: Date
    shippedDate?: Date
    requiredReturnDate: Date
    returnDate?: Date
    status: string
    comments: string
    deliveryman: User
    customer: User
    shippedAdress: string
    returnAdress: string
    realProducts: RealProduct[]

    totalPrice?: number;
  
    orderProducts: OrderProduct[];
 }

