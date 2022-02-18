import { MainGroupDoctor } from "./MainGroupDoctor";
import { SpecialtyLevel } from "./SpecialtyLevel";
import { User } from "./User";

export interface SpecializedInformationDoctor {
    user: User;
    userId: number;
    mainGroupDoctor: MainGroupDoctor;
    mainGroupsDoctorId: number;
    specialtyLevel: SpecialtyLevel;
    specialtiyLevelId: number;
}