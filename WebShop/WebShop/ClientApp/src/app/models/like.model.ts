import { Customer } from "./customer.model";
import { Product } from "./product.model";

export interface Like{
    customer: Customer;
    product: Product;
    value: boolean;
}