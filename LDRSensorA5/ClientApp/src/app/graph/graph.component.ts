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

  constructor(private ldrService:LDRService) {
    Chart.register(...registerables);
    this.ldrData = new LDRData(0,0,new Date())
   }

  ngOnInit(): void {
      const ctx = document.getElementById('myChart') as HTMLCanvasElement;
      this.chart = new Chart(ctx, {
          type: 'bar',
          data: {
              labels: ['Time'],
              datasets: [{
                  label: 'Current',
                  data: [0],
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
      // setInterval(()=>{
      //   this.plotUpdates(counter) 
      // },1000);
  }

  plotUpdates(counter:number)
  {
    let val = Math.ceil(Math.random() * (10 - 2 + 1) + 2)
    console.log(val)
    this.chart?.data.datasets[0].data.push(val)
    this.chart?.data.labels?.push(counter);
    counter++;
    this.chart?.update()
    
  }
}
