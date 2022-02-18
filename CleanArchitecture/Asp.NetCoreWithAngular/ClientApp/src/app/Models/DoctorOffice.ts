import { PhotoDoctorOffice } from "./PhotoDoctorOffice";
import { GivingTimeOnline } from "./GivingTimeOnline";
import { PhoneDoctorOffice } from "./PhoneDoctorOffice";

export interface DoctorOffice {
    id:number;
    userId: number;
    cityId:number;
    address: string;
    phonesDoctorsOffices: PhoneDoctorOffice[];
    photosDoctorsOffices: PhotoDoctorOffice[];
    givingTimesOnline: GivingTimeOnline[];
}

