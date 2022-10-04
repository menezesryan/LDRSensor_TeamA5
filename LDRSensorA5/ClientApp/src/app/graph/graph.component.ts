import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Subscription } from 'rxjs';
import { LDRService } from '../ldr.service';
import { LDRData } from '../models/LDRData';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})

export class GraphComponent implements OnInit, OnDestroy {
  chart: Chart | undefined
  ldrData: LDRData
  subscription: Subscription
  XAxisLabels: string[] = []
  YAxisValues: number[] = []
  intervalId: any

  constructor(private ldrService: LDRService) {
    Chart.register(...registerables);
    this.ldrData = new LDRData(0, 0, new Date())
    this.subscription = Subscription.EMPTY

  }
  ngOnDestroy(): void {
    clearInterval(this.intervalId)
    if (this.subscription)
      this.subscription.unsubscribe()

    this.chart?.destroy()
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

    let counter = 0;
    this.intervalId = setInterval(() => {
      this.subscription = this.ldrService.getLDRData().subscribe(data => {
        this.ldrData.current = parseFloat(data.current.toFixed(2));
        this.ldrData.lux = parseFloat(data.current.toFixed(2))
        this.ldrData.timeStamp = data.timeStamp
        this.plotUpdates(data)
      })

    }, 1000);
  }

  plotUpdates(ldrData: LDRData) {
    console.log(ldrData)
    // this.chart?.data.datasets[0].data.push(ldrData.current)
    // this.chart?.data.labels?.push(ldrData.timeStamp);
    if (this.XAxisLabels.length > 10) {
      this.XAxisLabels.shift()
      this.YAxisValues.shift()
    }

    var datepipe = new DatePipe("en-US")
    var time = datepipe.transform(ldrData.timeStamp, "mediumTime")

    this.XAxisLabels.push(time!)
    this.YAxisValues.push(ldrData.current)
    this.chart?.update()
  }
}
