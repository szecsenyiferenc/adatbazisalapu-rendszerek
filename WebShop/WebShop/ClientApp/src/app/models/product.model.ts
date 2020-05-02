import { Category } from "./category.model";

export interface Product{
    id?: number;
    name: string;
    price: number;
    image?: string;
    likes?: number;
    categories?: Category[];
}