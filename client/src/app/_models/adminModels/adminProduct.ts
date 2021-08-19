import { AdminProductFeature } from "./adminProductFeature";
import { AdminRealProduct } from "./adminRealProduct";

export interface AdminProduct {
  id?: number
  name?: string
  vendor?: string
  description?: string
  categoryId?: number
  categoryName?: string
  productImgsLinks?: string[]
  productFeatures?: AdminProductFeature[]
  realProducts?: AdminRealProduct[]
}