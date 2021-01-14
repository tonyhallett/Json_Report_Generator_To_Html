import { Group } from "./Group";
import { GroupedObject } from "./GroupedObject";
import { GroupSelector } from "./GroupSelector";
import { GroupValueSummer } from "./GroupValueSummer";

export type GroupFilter<T>  =(group:Group<T>)=>boolean
export class GroupSorter<T>{
  
  private toGroup:T[]
  private identifier:keyof T
  private idProp:keyof T
  private summer:GroupValueSummer<T>
  private rootGroup:Group<T> = new Group<T>(-1,Group.root,"");
  private currentSort:{
    key:keyof T
    ascending:boolean
  }
  private groupFilter: GroupFilter<T>
  constructor(toGroup: T[],summer:GroupValueSummer<T>, identifier: keyof T, idProp: keyof T,filter: GroupFilter<T>){
    this.toGroup = toGroup;
    this.summer = summer;
    this.identifier = identifier;
    this.idProp = idProp;
    this.groupFilter = filter;
  }
  refilter(): GroupedObject<T>[] {
    return this.rootGroup.getGroupedObjects();
  }
  group(groupSelector: GroupSelector<T>): GroupedObject<T>[] {
    this.doGrouping(groupSelector);
    return this.rootGroup.getGroupedObjects();
  }
  regroup(groupSelector: GroupSelector<T>): GroupedObject<T>[] {
    this.rootGroup.clear();
    this.doGrouping(groupSelector); // But want to retain selection
    if(this.currentSort!=null){
      this.rootGroup.sort(this.currentSort.key,this.currentSort.ascending);
    }
    return this.rootGroup.getGroupedObjects();
  }

  private ownProperties:string[];
  private getPropertyNames(object:T){
    if(this.ownProperties == undefined){
      this.ownProperties = Object.getOwnPropertyNames(object);
    }
    return this.ownProperties;
  }
  private doGrouping(groupSelector: GroupSelector<T>){
    this.toGroup.forEach(obj => {
      const propertyNames = this.getPropertyNames(obj);
      const groupHierarchy = groupSelector(obj);
      this.rootGroup.groupFilter = this.groupFilter;
      this.rootGroup.addToGroup(groupHierarchy, obj,this.identifier,this.idProp,this.summer,propertyNames,this.groupFilter);
    })
    
  }
  toggleGroupExpansion(groupId: string): GroupedObject<T>[] {
    this.rootGroup.toggleGroupExpansion(groupId);
    return this.rootGroup.getGroupedObjects();
  }

  expandCollapseAll(expand:boolean):GroupedObject<T>[]{
    this.rootGroup.expandCollapseAll(expand);
    return this.rootGroup.getGroupedObjects();
  }
 
  sort(key: keyof T, ascending: boolean): GroupedObject<T>[] {
    this.rootGroup.sort(key,ascending);
    return this.rootGroup.getGroupedObjects();
  }

}