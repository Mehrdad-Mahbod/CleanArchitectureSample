import { GivingTimeOnline } from "./GivingTimeOnline";

export interface TurnTaking {
    id: number;
    userId: number;
    gregorianDate:Date;
    persianDateString: string;
    persianDate: string;
    isHoliDay: boolean;
    remaining:number;
    givingTimeOnlineList: GivingTimeOnline[];
    isEnabled : boolean;
}