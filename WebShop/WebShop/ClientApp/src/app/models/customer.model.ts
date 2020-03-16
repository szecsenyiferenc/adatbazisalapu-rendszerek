export interface Customer{
    email: string;
    firstName: string;
    lastName: string;
    balance?: number;
    phone: string;
    isRegularCustomer?: boolean;
    city: string;
    street: string;
    houseNumber: number;
    visitedProducts?: any[];
    purchasedProducts?: any[];
    comments?: any[];
    likes?: any[];
}