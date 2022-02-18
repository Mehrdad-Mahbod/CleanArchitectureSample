import { Drug } from "./Drug";
import { PatientDrug } from "./PatientDrug";

export interface DoctorDrug {
    id:number;
    userId: number;
    drug:Drug;
    drugId: number;
    description: string;
    priority: number | null;
    patientsDrugs: PatientDrug[];
}