export class ManualModeData
{
    current:number
    relayState:boolean

    constructor(current:number, relayState:boolean)
    {
        this.current = current
        this.relayState = relayState
    }
}