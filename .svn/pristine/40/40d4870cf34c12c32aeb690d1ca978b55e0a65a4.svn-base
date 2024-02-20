import { Component } from '@angular/core';

@Component({
  selector: 'app-rsk-mst-dashboard',
  templateUrl: './rsk-mst-dashboard.component.html',
  styleUrls: ['./rsk-mst-dashboard.component.scss']
})
export class RskMstDashboardComponent { 

  rskHIchartOptions: any = {}; 
 
 
  constructor() {}

  ngOnInit(): void {
    this.rskHIchartOptions = getChartOptions(350);
  }
 
}

function getChartOptions(height: number) {
  const labelColor = '#000'; 
  const borderColor = '#f56669';
  const secondaryColor = '#C0C0C0';
  const baseColor = '#1E90FF';  

  return {
    series: [

      
      {
        name: 'Allocated',
        data: [50, 60, 70, 80, 70, 50, 70, 20],
      },
      {
        name: 'Visit Completed',
        data: [30, 20, 90, 20, 40, 50, 70, 60],
      },
     
  
    ],
    chart: {
      fontFamily: 'inherit',
      type: 'bar',
      height: height,
      toolbar: {
        show: false,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '50%',
        borderRadius: 5,
      },
    },
    legend: {
      show: false,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 2,
      colors: ['transparent'],
    },
    xaxis: {
      categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        style: {
          colors: labelColor,
          fontSize: '12px',
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: labelColor,
          fontSize: '12px',
        },
      },
    },
    fill: {
      type: 'solid',
    },
    states: {
      normal: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      hover: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      active: {
        allowMultipleDataPointsSelection: false,
        filter: {
          type: 'none',
          value: 0,
        },
      },
    },

    colors: [baseColor, secondaryColor],
    grid: {
      padding: {
        top: 10,
      },
      borderColor: borderColor,
      strokeDashArray: 4,
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
  };
 
}
