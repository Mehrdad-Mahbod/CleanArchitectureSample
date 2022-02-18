import { DoctorFieldWork } from "./DoctorFieldWork";
import { DoctorOffice } from "./DoctorOffice";
import { SpecializedInformationDoctor } from "./SpecializedInformationDoctor";
import { UserRole } from "./UserRole";

export class User {
    id: number | undefined ;                                      
    parentId: number | undefined ;                                      
    gender: number | undefined ;                              
    name: string | undefined ;                                 
    family: string | undefined ;                                                                                                                        
    userName:string  | undefined ;                           
    password: string | undefined ;                                             
    email: string | undefined ;                                 
    phoneNumber:string| undefined ;
    imgType :string| undefined ;
    imgBase64: string| undefined ; 
    userRoles:UserRole[]| undefined ; 
    doctorOffices:DoctorOffice[]| undefined ;
    specializedInformationDoctors:SpecializedInformationDoctor[]|undefined;
    doctorsFieldsWork:DoctorFieldWork[]|undefined;
    fromPage:number| undefined;
    toPage:number| undefined;
  
  }