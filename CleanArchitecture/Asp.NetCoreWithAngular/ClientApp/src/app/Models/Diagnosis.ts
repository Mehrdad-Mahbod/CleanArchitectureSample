import { PatientDiagnosis } from "./PatientDiagnosis";

export interface Diagnosis {
    id: number | null;
    userId: number;
    name: string;
    priority: number | null;
    parent: Diagnosis;
    parentId: number | null;
    children:Diagnosis[];
    patientDiagnoses: PatientDiagnosis[];
}