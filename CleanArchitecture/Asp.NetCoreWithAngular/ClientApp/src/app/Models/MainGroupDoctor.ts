export interface MainGroupDoctor {
    id?:number;
    name: string;
    parent: MainGroupDoctor ;
    parentId: number ;
    parentName:string;
    children: MainGroupDoctor[];
}