export class ConnectionParameters {
    portName: string
    baudRate: number
    dataBit: number
    startBit: number
    stopBit: number
    parityBit: number

    constructor(portName: string, BaudRate: number, DatBit: number, StartBit: number, StopBit: number, ParityBit: number) {
        this.portName = portName
        this.baudRate = BaudRate
        this.dataBit = DatBit
        this.startBit = StartBit
        this.stopBit = StopBit
        this.parityBit = ParityBit
    }

}