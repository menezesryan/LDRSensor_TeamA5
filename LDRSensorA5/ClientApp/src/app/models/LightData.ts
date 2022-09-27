export class LDRData
{
    Lux : number
    Current : number
    TimeStamp : Date

    constructor(Current : number, Lux : number, TimeStamp : Date)
    {
        this.Current = Current
        this.Lux = Lux
        this.TimeStamp = TimeStamp
    }
}