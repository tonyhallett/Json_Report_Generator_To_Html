import { GroupSorter } from "../src/CoverageTable/GroupSorter/groupSorter"

describe('groupSorter', () => {
  interface ToGroup{
    assembly:string
    namespace:string
    name:string,
    number:number,
  }
  const objects:Array<ToGroup> = [
    {
      assembly:"Assembly1",
      namespace:"Namespace1",
      name:"A1N1Class1",
      number:1,
    },
    {
      assembly:"Assembly1",
      namespace:"Namespace1",
      name:"A1N1Class2",
      number:2,
    },
    {
      assembly:"Assembly1",
      namespace:"Namespace2",
      name:"A1N2Class1",
      number:3,
    },
    {
      assembly:"Assembly1",
      namespace:"Namespace2",
      name:"A1N2Class2",
      number:4,
    },

    {
      assembly:"Assembly2",
      namespace:"Namespace1",
      name:"A2N1Class1",
      number:5,
    },
    {
      assembly:"Assembly2",
      namespace:"Namespace1",
      name:"A2N1Class2",
      number:6,
    },
    {
      assembly:"Assembly2",
      namespace:"Namespace2",
      name:"A2N2Class1",
      number:7,
    },
    {
      assembly:"Assembly2",
      namespace:"Namespace2",
      name:"A2N2Class2",
      number:8,
    }
  ];
  it('should be able to group - single group', () => {
    const groupSorter = new GroupSorter<ToGroup>();

    var grouped = groupSorter.group(objects,"name",obj => {
      return ["All"]
    },(key,groupedObjects) => {
      switch(key){
        case "number":
          return groupedObjects.map(g=>g.number).reduce((a,b) => a + b, 0);
        default:
          return "";
      }
    })

    expect(grouped.length).toBe(9);
    const groupRow = grouped[0];
    expect(groupRow.name).toBe("All");
    expect(groupRow.groupId).toBe("All");
    expect(groupRow.groupLevel).toBe(1);
    expect(groupRow.isCollapsed).toBe(false);
    expect(groupRow.number).toBe(36);
    for(let i=0;i<objects.length;i++){
      const object = objects[i];
      const regularRow = grouped[i+1];
      expect(regularRow.groupLevel).toBe(0);
      expect(regularRow.assembly).toBe(object.assembly);
      expect(regularRow.namespace).toBe(object.namespace);
      expect(regularRow.name).toBe(object.name);
    }

  });

  it('should be able to group by single level', () => {
    const groupSorter = new GroupSorter<ToGroup>();
    

    var grouped = groupSorter.group(objects,"name",obj => {
      return [obj.assembly];
    },(key,groupedObjects) => {
      switch(key){
        case "number":
          return groupedObjects.map(g=>g.number).reduce((a,b) => a + b, 0);
        default:
          return "";
      }
    })
    
    expect(grouped.length).toBe(10);

    function expectAssembly(index:number,expectedAssemblyName:string,expectedSum:number){
      var assembly = grouped[index];
      expect(assembly.groupId).toBe(expectedAssemblyName);
      expect(assembly.name).toBe(expectedAssemblyName);
      expect(assembly.groupLevel).toBe(1);
      expect(assembly.isCollapsed).toBe(false);
      expect(assembly.number).toBe(expectedSum);
    }
    function expectTerminal(start:number,end:number,shift:number){
      for(let i=start;i<end;i++){
        var terminal = grouped[i];
        var object = objects[i-shift];
        expect(terminal.groupLevel).toBe(0);
        expect(terminal.assembly).toBe(object.assembly);
        expect(terminal.namespace).toBe(object.namespace);
        expect(terminal.name).toBe(object.name); 
        expect(terminal.number).toBe(object.number);
      }
    }

    expectAssembly(0,"Assembly1",10);
    expectTerminal(1,5,1);
    expectAssembly(5,"Assembly2",26);
    expectTerminal(6,objects.length,2);
    
  })
  //need a test that groupo ids are unique
  it('should be able to group by multiple levels', () => {
    const groupSorter = new GroupSorter<ToGroup>();
    

    var grouped = groupSorter.group(objects,"name",obj => {
      return [obj.namespace,obj.assembly];
    },(key,groupedObjects) => {
      switch(key){
        case "number":
          return groupedObjects.map(g=>g.number).reduce((a,b) => a + b, 0);
        default:
          return "";
      }
    })

    /*
      ass1
        ns1
          c1
          c2
        ns2
          c1
          c2
      ass2
        ns1
          c1
          c2
        ns2
          c1
          c2

    */

    expect(grouped.length).toBe(14);

    function expectGroup(index:number,expectedName:string,expectedSum:number, expectedLevel:number){
      var assembly = grouped[index];
      expect(assembly.groupId).toBe(expectedName);
      expect(assembly.name).toBe(expectedName);
      expect(assembly.groupLevel).toBe(expectedLevel);
      expect(assembly.isCollapsed).toBe(false);
      expect(assembly.number).toBe(expectedSum);
    }
    

    function expectTerminal(groupIndex:number,objectIndex:number){
      var terminal = grouped[groupIndex];
      var object = objects[objectIndex];
      expect(terminal.groupLevel).toBe(0);
      expect(terminal.assembly).toBe(object.assembly);
      expect(terminal.namespace).toBe(object.namespace);
      expect(terminal.name).toBe(object.name); 
      expect(terminal.number).toBe(object.number);
    }

    expectGroup(0,"Assembly1",10,1);
    expectGroup(1,"Assembly1.Namespace1",3, 2);
    expectTerminal(2,0);
    expectTerminal(3,1);
    expectGroup(4,"Assembly1.Namespace2",7,2);
    expectTerminal(5,2);
    expectTerminal(6,3)
    expectGroup(7,"Assembly2",26,1);
    expectGroup(8,"Assembly2.Namespace1",11,2);
    expectTerminal(9,4);
    expectTerminal(10,5);
    expectGroup(11,"Assembly2.Namespace2",15,2);
    expectTerminal(12,6);
    expectTerminal(13,7);

  })
  // 
  //then sorting

  // then expand/collapse assume will just not return
})