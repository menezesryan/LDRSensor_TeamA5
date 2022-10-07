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
  graphType: string
  chartConfig: any
  chartTypes: string[]
  timeInterval: string[]
  database: LDRData[] = []
  numValues: number

  constructor(private ldrService: LDRService) {
    Chart.register(...registerables);
    this.ldrData = new LDRData(0, 0, new Date())
    this.subscription = Subscription.EMPTY
    this.graphType = "bar"

    this.chartTypes = ["Bar", "Line"]
    this.timeInterval = ["10 sec", "1 min", "2 min"]

    this.numValues = 10;
  }
  ngOnDestroy(): void {
    clearInterval(this.intervalId)
    if (this.subscription)
      this.subscription.unsubscribe()

    this.chart?.destroy()
  }

  ngOnInit(): void {
    this.chartConfig = {
      type: 'bar',
      data: {
        labels: this.XAxisLabels,
        datasets: [{
          label: 'Current',
          data: this.YAxisValues,
          backgroundColor: [
            'rgba(255, 90, 100, 0.45)'
          ],
          borderColor: [
            '#151424'
          ],
          borderWidth: 1.5
        }]
      },
      options: {
        maintainAspectRatio: false,
        scales: {
          x: {
            min: 0
          },
          y: {
            beginAtZero: true
          }
        },
      }
    }
    const ctx = document.getElementById('myChart') as HTMLCanvasElement;
    this.chart = new Chart(ctx, this.chartConfig);
    var country = document.getElementById("chartSelect")! as HTMLSelectElement;
    if (country != null) {
      country.options.selectedIndex = 1;
      // country.getS
    }
    let counter = 0;
    this.intervalId = setInterval(() => {
      this.subscription = this.ldrService.getLDRData().subscribe(data => {
        this.ldrData.current = parseFloat(data.current.toFixed(2));
        this.ldrData.lux = parseFloat(data.lux.toFixed(2))
        this.ldrData.timeStamp = data.timeStamp
        this.plotUpdates(data)
      })

    }, 1000);
  }

  plotUpdates(ldrData: LDRData) {
    console.log(ldrData)


    if (this.XAxisLabels.length > this.numValues) {
      this.XAxisLabels.shift()
      this.YAxisValues.shift()
    }

    this.chartConfig.type = this.graphType


    var datepipe = new DatePipe("en-US")
    var time = datepipe.transform(ldrData.timeStamp, "mediumTime")

    this.XAxisLabels.push(time!)
    this.YAxisValues.push(ldrData.current)
    this.chart?.update()
  }

  onChartChange(evt: any) {
    console.log(evt.target.value)
    this.graphType = evt.target.value.toLowerCase().trim()
  }

  onDurationChange(evt: any) {
    console.log(evt.target.value)
    var duration = evt.target.value.trim();

    this.ldrService.getDatabaseData().subscribe((data) => {
      this.database = data;
      var flag = false;
      var temp = this.database
      var len = this.database.length;

      if (duration == "10 sec") {
        this.database.splice(0, len - 10);
        this.numValues = 10;
        flag = true;
      }
      else if (duration == "1 min") {
        this.database.splice(0, len - 60);
        this.numValues = 60;
        flag = true;
      }
      else if (duration == "2 min") {
        this.database.splice(0, len - 120)
        this.numValues = 120;
        flag = true;
      }
      else if (duration == "5 min") {
        this.database.splice(0, len - 300)
        this.numValues = 300;
        flag = true;
      }

      if (flag == true) {
        this.XAxisLabels.length = 0
        this.YAxisValues.length = 0
        this.database.forEach(element => {
          var c = parseFloat(element.current.toFixed(2))
          var l = parseFloat(element.lux.toFixed(2))
          var datepipe = new DatePipe("en-US")
          var time = datepipe.transform(element.timeStamp, "mediumTime")
          this.XAxisLabels.push(time!)
          this.YAxisValues.push(c)
        });
      }
    })


  }
}
