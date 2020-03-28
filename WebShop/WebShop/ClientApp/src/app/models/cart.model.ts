import { Customer } from "./customer.model";
import { CartItem } from "./cart-item.model";

export interface Cart{
    customer: Customer;
    cartItems: CartItem[];
}