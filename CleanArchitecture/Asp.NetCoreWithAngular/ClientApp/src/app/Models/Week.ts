import { GivingTimeOnline } from "./GivingTimeOnline";

export interface Week {
    id: number;
    name: string;
    presenceDoctorInOffices: GivingTimeOnline[];
}