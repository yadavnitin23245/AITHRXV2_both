import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label, SingleDataSet } from 'ng2-charts';
@Component({
  selector: 'app-master-dashboard',
  templateUrl: './master-dashboard.component.html',
  styleUrls: ['./master-dashboard.component.less']
})
export class MasterDashboardComponent implements OnInit {

  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Test Maruti Dealer', stack: 'a' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Test Honda Dealer', stack: 'a' }
  ];

  public chart = {
    "datasets": [
      { "data": [0, 30, 20, 40, 35, 45, 33, 0, 0], "label": "Test Maruti Dealer" },
      { "data": [0, 50, 60, 55, 59, 30, 40, 0, 0], "label": "Test Honda Dealer" },
      // { "data": [45, 45, 45, 45, 45, 45, 45, 45, 45], "label": "Line", "type": "line" }
    ],
    "labels": ["FirstPlaceholder", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "LastPlaceholder"],
    "options": {
      "legend": {
        "text": "You awesome chart with average line",
        "display": true,
      },
      "scales": {
        "yAxes": [{
          "ticks": {
            "beginAtZero": true
          }
        }],
        "xAxes": [{
          "ticks": {
            "min": "Monday",
            "max": "Sunday",
          }
        }],
      }
    }
  };

  pieChartOptions: ChartOptions;
  pieChartLabels: Label[];
  pieChartData: SingleDataSet;
  pieChartType: ChartType;
  pieChartLegend: boolean;
  pieChartPlugins = [];

  constructor() { }

  ngOnInit(): void {
    this.pieChartOptions = this.createOptions();
    this.pieChartLabels = ['Test Maruti Dealer', 'Test Honda Dealer', 'Test Maruti Dealer'];
    this.pieChartData = [50445, 33655, 15900];
    this.pieChartType = 'pie';
    this.pieChartLegend = true;
    //  this.pieChartPlugins = [pluginLabels];
  }
  private createOptions(): ChartOptions {
    return {
      responsive: true,
      maintainAspectRatio: true,
      plugins: {
        labels: {
          render: 'percentage',
          fontColor: ['green', 'white', 'red'],
          precision: 2
        }
      },
    };
  }

}
