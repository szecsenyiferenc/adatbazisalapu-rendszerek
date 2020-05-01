import { Product } from "./product.model";

export interface VisitedProduct extends Product{
    timesOfVisit?: number;
}