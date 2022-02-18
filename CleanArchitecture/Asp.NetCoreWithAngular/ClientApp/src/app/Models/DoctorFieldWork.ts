import { User } from "./User";

export interface DoctorFieldWork {
    user: User;
    userId: number;
    title: string;
    description: string;
    workingExperience: string;
}