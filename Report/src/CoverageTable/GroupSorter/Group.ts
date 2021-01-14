import { GroupedObject, GroupedObjectProperties } from "./GroupedObject";
import { GroupHierarchy } from "./GroupSelector";
import { GroupFilter } from "./groupSorter";
import { GroupValueSummer } from "./GroupValueSummer";

export class Group<T> {
  

  static terminal = "___terminal";
  static root = "___root";

  private level: number;

  private groupId: string;
  private groupName: string;
  private groups: Array<Group<T>> = [];

  private summer: GroupValueSummer<T>;
  public groupFilter: GroupFilter<T>
  private identifier: keyof T;
  private idProp: keyof T;

  private propertyNames: string[];

  private terminal: GroupedObject<T>;

  constructor(level: number, groupId: string, groupName: string) {
    this.level = level;
    this.groupId = groupId;
    this.groupName = groupName;
  }

  private getNextLevel() {
    if (this.level == -1) {
      return 1;
    }
    return this.level + 1;
  }
  private getNextGroupId(nextPartGroupId: string) {
    if (this.isRoot()) {
      return nextPartGroupId;
    }
    return `${this.groupId}.${nextPartGroupId}`;
  }

  private addTerminal(object: T) {
    const groupObjectProperties: GroupedObjectProperties = {
      groupId: "",
      groupLevel: 0,
      isCollapsed: false
    };
    this.terminal = Object.assign(groupObjectProperties, object);
  }

  public isTerminal() {
    return this.groupId === Group.terminal;
  }
  private isRoot() {
    return this.groupId === Group.root;
  }

  private addGroup(group: Group<T>) {
    this.groups.push(group);
  }
  isString(thing: any) {
    return typeof thing === 'string';
  }

  clear() {
    if (this.isRoot()) {
      this.groups = [];
    }
  }
  sort(key: keyof T, ascending: boolean) {
    if(!this.isTerminal()){
      // later, collator - https://stackoverflow.com/questions/2802341/javascript-natural-sort-of-alphanumerical-strings/38641281
      
      this.groups.forEach(g=>{
        g.sort(key,ascending);
      })
          
      const sortedGroups = this.groups.sort((g1, g2) => {
        let compared = 0;
        const g1Header = g1.getHeader();
        const g2Header = g2.getHeader();
        const g1Value = g1Header[key];
        const g2Value = g2Header[key];
        // will do switch - later date could do dates
        if (typeof g1Value === 'string') { // for now no null/undefined
          compared = (g1Value as string).localeCompare(g2Value as any as string);
          if (!ascending) {
            compared = compared * -1;
          }
        }

        //todo nulls and undefined - but have enough to get on with
        else if (typeof g1Value === 'number' && typeof g2Value === 'number') {
          compared = g1Value > g2Value ? 1 : -1;
          if (!ascending) {
            compared = compared * -1;
          }
        }
        return compared;
      });
      this.groups = sortedGroups;
      
    }
  }

  toggleGroupExpansion(groupId: string) {
    if (groupId == this.groupId) {
      const header = this.getHeader();
      this.header = {...header,isCollapsed:!header.isCollapsed};
    } else {
      this.groups.forEach(g => g.toggleGroupExpansion(groupId));
    }
  }

  
  expandCollapseAll(expand: boolean) {
    if(!this.isRoot()){
      const header = this.getHeader();
      this.header = {...header,isCollapsed:!expand};
    }
    
    this.groups.forEach(g=>g.expandCollapseAll(expand));
  }
  //#endregion
  addToGroup(groupedHierarchy: GroupHierarchy, object: T, identifier: keyof T, idProp: keyof T, summer: GroupValueSummer<T>, propertyNames: string[],groupFilter:GroupFilter<T>) {
    this.identifier = identifier;
    this.idProp = idProp;
    this.summer = summer;
    this.groupFilter = groupFilter;
    this.propertyNames = propertyNames;

    if (groupedHierarchy.length === 0) {
      const singleGroup = new Group<T>(0, Group.terminal, object[identifier].toString());
      singleGroup.addTerminal(object);
      singleGroup.groupFilter = groupFilter;
      this.addGroup(singleGroup);

    } else {
      const groupName = groupedHierarchy.pop();
      const nextGroupId = this.getNextGroupId(groupName);

      let matchedGroup = this.groups.find(g => g.groupId === nextGroupId);
      if (matchedGroup === undefined) {
        matchedGroup = new Group<T>(this.getNextLevel(), nextGroupId, groupName);
        this.addGroup(matchedGroup);
      }

      matchedGroup.addToGroup(groupedHierarchy, object, identifier, idProp, summer, propertyNames,groupFilter);
    }

  }
  header:GroupedObject<T>

  getHeader(): GroupedObject<T> {
    if (this.isTerminal()) {
      return this.terminal;
    }
    if(!this.header){
      const headers = this.groups.map(g => g.getHeader());
      const header:T = this.summer(headers);
      this.addHeaderProperties(header);
      this.header = header as GroupedObject<T>;
    }
    
    return this.header;
  }

  //todo type header
  private addHeaderProperties(header: any) {
    header[this.identifier as string] = this.groupName;
    header[this.idProp as string] = this.groupId;
    header.groupId = this.groupId;
    header.groupLevel = this.level;
    header.isCollapsed = false;
  }

  getGroupedObjects(): GroupedObject<T>[] {
    // can probably memoize at both ends
    const filtered = this.groupFilter(this);
    if(filtered){
      return [];
    }
    if (this.isTerminal()) {
      return [this.terminal];
    }

    const descendantGroupedObjects = this.groups.reduce((prev, group) => prev.concat(group.getGroupedObjects()), [] as GroupedObject<T>[]);
    if(descendantGroupedObjects.length == 0){
      return [];
    }
    const header = this.getHeader();
    if (header.isCollapsed) {
      return [header];
    }

    let groupedObjects = [header].concat(descendantGroupedObjects);
    if (this.isRoot()) {
      groupedObjects.shift();
    }
    return groupedObjects;
  }
}
