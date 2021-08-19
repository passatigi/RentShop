import { ProductFeature } from "./productFeature";
import { RealProduct } from "./realProduct";

export interface Product {
    id: number;
    name: string;
    vendor: string;
    description: string;
    categoryId: number;
    categoryName: string;
    productImgsLinks: string[];
    productFeatures: ProductFeature[];
    realProducts: RealProduct[];
}