export interface GroupedObjectProperties{
  groupLevel:number
  groupId:string
  isCollapsed:boolean // only applies to levels not 0
}
export type GroupedObject<T> = T & GroupedObjectProperties;
