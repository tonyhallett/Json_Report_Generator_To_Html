export type GroupHierarchy = Array<string>
export type GroupSelector<T> = (object: T) => GroupHierarchy;
