export function safeExecuteExternal(executor:(external:any)=>void) {
    try {
        if(window.external){
            executor(window.external as any);
        }
    } catch { }
}

export function logExternal(message:string){
    safeExecuteExternal(external=>external.Log(message));
  }
