import { ReservationDoctorVisit } from "./ReservationDoctorVisit";
import { Week } from "./Week";

export interface GivingTimeOnline {
    id: number;
    userId: number;
    week: Week;
    weekId: number;
    doctorOfficeId: number | null;
    fromTime: string | null;
    toTime: string | null;
    countVisit: number | null;
    persianDateString: string;
    persianDate: string;
    isHoliDay: boolean;
    remaining:number;
    reservationDoctorVisits: ReservationDoctorVisit[];
}