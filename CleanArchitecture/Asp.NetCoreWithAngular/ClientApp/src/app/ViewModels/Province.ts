import { City } from "./City";

export interface Province {
    id: number;
    name: string;
    prePhoneNumber: string;
    cities: City[];
}