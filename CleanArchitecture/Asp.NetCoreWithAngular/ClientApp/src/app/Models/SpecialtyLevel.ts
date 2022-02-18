export interface SpecialtyLevel  {
    id:number;
    name: string;
    parent: SpecialtyLevel ;
    parentId: number | null ;
    parentName:string;
    children: SpecialtyLevel[];
}