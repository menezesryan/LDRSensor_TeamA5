export class ConnectionParameters
{
    baudRate : number
    dataBit : number
    startBit : number
    stopBit : number
    parityBit : number

    constructor(BaudRate:number, DatBit:number,StartBit:number,StopBit:number,ParityBit:number)
    {
        this.baudRate = BaudRate
        this.dataBit = DatBit
        this.startBit = StartBit
        this.stopBit = StopBit
        this.parityBit = ParityBit
    }

}