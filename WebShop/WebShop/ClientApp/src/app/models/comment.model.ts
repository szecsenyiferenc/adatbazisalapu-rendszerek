import { Customer } from "./customer.model";

export interface Comment{
    author: Customer;
    dateTime?: Date;
    text?: string;
}