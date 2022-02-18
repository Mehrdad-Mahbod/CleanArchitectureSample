import { DoctorDrug } from "./DoctorDrug";

export interface Drug {
    id:number;
    name: string;
    salt: string;
    dosageForm: string;
    strengh: string;
    routeAdmin: string;
    aTCCode: string;
    accessLevel: string;
    remarks: string;
    date: string;
    doctorsDrugs: DoctorDrug[];
}