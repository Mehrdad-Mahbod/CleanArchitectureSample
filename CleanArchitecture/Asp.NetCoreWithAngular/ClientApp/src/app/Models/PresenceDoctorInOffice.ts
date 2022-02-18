import { ReservationDoctorVisit } from "./ReservationDoctorVisit";
import { Week } from "./Week";

export interface GivingTimeOnline {
    userId: number;
    week: Week;
    weekId: number;
    doctorOfficeId: number | null;
    fromTime: string | null;
    toTime: string | null;
    countVisit: number | null;
    reservationDoctorVisits: ReservationDoctorVisit[];
}