import { Component} from '@angular/core';

@Component({
  selector: 'app-system-dashboard',
  templateUrl: './system-dashboard.component.html',
  styleUrls: ['./system-dashboard.component.scss']
})
export class SystemDashboardComponent { 


  chartOptions: any = {};
 
  constructor() {}

  ngOnInit(): void {
    this.chartOptions = getChartOptions(350);
  }
}


function getChartOptions(height: number) {
  const labelColor = '#000'; 
  const borderColor = '#e6ccb2';
  const secondaryColor = '#f1841e'
  const baseColor1 = '#047beb';
  const secondaryColor1 = '#e63423'
  const baseColor = '#06a813';  

  return {
    series: [

      
      {
        name: 'Active users',
        data: [50, 60, 70, 80, 60, 50, 70, 60],
      },
      {
        name: 'Inactive Users',
        data: [50, 60, 70, 80, 60, 50, 70, 60],
      },
      {
        name: 'Onboarding',
        data: [20, 40, 30, 70, 60, 10, 20, 30],
      },
      {
        name: 'Relieving',
        data: [50, 60, 70, 80, 60, 50, 70, 60],
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

    colors: [baseColor, secondaryColor,baseColor1, secondaryColor1],
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

