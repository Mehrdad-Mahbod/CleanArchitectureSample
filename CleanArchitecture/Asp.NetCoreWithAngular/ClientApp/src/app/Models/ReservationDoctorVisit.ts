import { PatientDiagnosis } from "./PatientDiagnosis";
import { PatientDrug } from "./PatientDrug";
import { PatientExamination } from "./PatientExamination";
import { PicturePatientTreatmentPrescription } from "./PicturePatientTreatmentPrescription";
import { TextPatientTreatmentPrescription } from "./TextPatientTreatmentPrescription";

export interface ReservationDoctorVisit {
    id:number;
    userId: number;
    givingTimeOnlineId: number | null;
    reservationDate:Date;
    reservationDateStr:string;
    patientExaminations: PatientExamination[];
    patientDiagnoses: PatientDiagnosis[];
    picturesPatientTreatmentPrescriptions: PicturePatientTreatmentPrescription[];
    textsPatientsTreatmentPrescriptions: TextPatientTreatmentPrescription[];
    patientsDrugs: PatientDrug[];
}