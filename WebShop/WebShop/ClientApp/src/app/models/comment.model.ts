import { Customer } from "./customer.model";
import { Product } from "./product.model";

export interface ProductComment{
    customer: Customer;
    product?: Product;
    dateTime?: Date;
    text?: string;
}