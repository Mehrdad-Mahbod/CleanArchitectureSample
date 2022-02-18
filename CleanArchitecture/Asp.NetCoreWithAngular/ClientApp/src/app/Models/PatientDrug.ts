import { DoctorDrug } from "./DoctorDrug";
import { ReservationDoctorVisit } from "./ReservationDoctorVisit";

export interface PatientDrug {
    reservationDoctorVisitId: number;
    reservationDoctorVisit: ReservationDoctorVisit;
    doctorDrugId: number | null;
    doctorDrug: DoctorDrug;
}