import { UserRole } from "./UserRole";

export class User {
    id: number | undefined ;                                      
    parentId: number | undefined ;                                      
    gender: number | undefined ;                              
    name: string | undefined ;                                 
    family: string | undefined ;                                                                                                                        
    userName:string  | undefined ;                           
    password: string | undefined ; 
    rePassword : string | undefined ;                                           
    email: string | undefined ;                                 
    phoneNumber:string| undefined ;
    imgType :string| undefined ;
    imgBase64: string| undefined ; 
    userRoles:UserRole[]| undefined ; 
    fromPage:number| undefined;
    toPage:number| undefined;
  
  }