import { RealProduct } from "./realProduct";

export interface OrderProduct {
    realProductId: number
    realProduct?: RealProduct
    orderId?: number
  }