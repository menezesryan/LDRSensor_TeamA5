import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Chart,registerables } from 'chart.js';
import { LDRService } from '../ldr.service';
import { LDRData } from '../models/LDRData';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})

export class GraphComponent implements OnInit {
  chart : Chart | undefined
  ldrData:LDRData
  XAxisLabels:string[] =[]
  YAxisValues:number[]=[]

  constructor(private ldrService:LDRService) {
    Chart.register(...registerables);
    this.ldrData = new LDRData(0,0,new Date())
   }

  ngOnInit(): void {
      const ctx = document.getElementById('myChart') as HTMLCanvasElement;
      this.chart = new Chart(ctx, {
          type: 'bar',
          data: {
              labels: this.XAxisLabels,
              datasets: [{
                  label: 'Current',
                  data: this.YAxisValues,
                  backgroundColor: [
                      'rgba(255, 99, 132, 0.2)'
                  ],
                  borderColor: [
                    
                  ],
                  borderWidth: 1
              }]
          }
      });

      let counter =0;
      setInterval(()=>{
        this.ldrService.getLDRData().subscribe(data=>{
          this.ldrData = data;
          this.plotUpdates(data)
        })
         
      },1000);
  }

  plotUpdates(ldrData:LDRData)
  {
    console.log(ldrData)
    // this.chart?.data.datasets[0].data.push(ldrData.current)
    // this.chart?.data.labels?.push(ldrData.timeStamp);
    if(this.XAxisLabels.length>10)
    {
      this.XAxisLabels.shift()
      this.YAxisValues.shift()
    }

    var datepipe = new DatePipe("en-US")
    var time = datepipe.transform(ldrData.timeStamp,"mediumTime")
  
    this.XAxisLabels.push(time!)
    this.YAxisValues.push(ldrData.current)
    this.chart?.update()
  }
}
