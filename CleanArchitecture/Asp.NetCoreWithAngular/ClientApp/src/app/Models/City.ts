import { Province } from "./Province";

export interface City {    
    province: Province;
    provinceId: number;
    name: string;
}