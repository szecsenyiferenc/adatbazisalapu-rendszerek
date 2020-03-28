import { Product } from "./product.model";
import { Customer } from "./customer.model";

export interface CartItem{
    product: Product;
    quantity: number;
}