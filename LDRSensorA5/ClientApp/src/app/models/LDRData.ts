export class LDRData
{
    lux : number
    current : number
    timeStamp : Date

    constructor(Current : number, Lux : number, TimeStamp : Date)
    {
        this.current = Current
        this.lux = Lux
        this.timeStamp = TimeStamp
    }
}