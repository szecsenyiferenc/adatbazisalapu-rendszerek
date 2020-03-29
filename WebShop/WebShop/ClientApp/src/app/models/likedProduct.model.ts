import { Product } from "./product.model";

export interface LikedProduct extends Product{
    like?: boolean;
}