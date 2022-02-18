import { PatientExamination } from "./PatientExamination";

export interface Examination{
    id :number | undefined;
    userId: number | undefined;
    name: string | undefined;
    priority: number | null;
    patientExaminations: PatientExamination[];
    //addedDate:Date;
}