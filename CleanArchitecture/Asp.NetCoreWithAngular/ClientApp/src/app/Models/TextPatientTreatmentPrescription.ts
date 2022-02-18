import { NumberValueAccessor } from "@angular/forms";

export interface TextPatientTreatmentPrescription {
    id:number | null;
    reservationDoctorVisitId: number | null;
    text: string;
}