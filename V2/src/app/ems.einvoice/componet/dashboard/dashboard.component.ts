import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent {

  chartOptions: any;
  Date: string;

  constructor(private router: Router, private service: SocketService) {
    this.Date = new Date().toString();
    DashboardComponent.constructor(); {
      setInterval(() => {
        this.Date = new Date().toString();
      }, 1000);
    }
  }

  ngOnInit() {
    this.chartOptions = getChartOptions(350);

    setInterval(() => {
      this.Date = new Date().toString();
    }, 1000);
  }

  static reinitialization() { }

  product() {
    this.router.navigate(['/einvoice/CrmMstProductAdd']);
  }

  customer() {
    this.router.navigate(['/einvoice/CrmMstCustomerAdd']);
  }

  employee() {
    this.router.navigate(['/system/SysMstEmployeeAdd']);
  }

  branch() {
    this.router.navigate(['/einvoice/SysMstBranch']);
  }

  department() {
    this.router.navigate(['/system/SysMstDepartment']);
  }
}

function getChartOptions(height: number) {
  const labelColor = '#000000';
  const borderColor = '#e6ccb2';
  const strokeColor = '#6e0a0a';
  const color = '#06a813';

  return {
    chart: {
      fontFamily: 'inherit',
      type: 'area',
      height: 400,
      toolbar: { show: false },
      sparkline: { enabled: false },
    },

    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
      axisBorder: { show: false },
      axisTicks: { show: false },
      labels: {
        show: true,
        style: { colors: labelColor, fontSize: '12px' },
      },
      crosshairs: {
        show: false,
        position: 'front',
        stroke: {
          color: borderColor,
          width: 1,
          dashArray: 3,
        },
      },
    },

    series: [
      {
        name: 'Total Sales',
        data: [30, 45, 32, 70, 40, 40, 30, 45, 32, 10, 60, 90],
      },
    ],

    legend: {
      show: false,
    },

    dataLabels: {
      enabled: false,
    },

    fill: {
      type: 'solid',
      opacity: 0,
    },

    stroke: {
      curve: 'smooth',
      show: true,
      width: 3,
      colors: [strokeColor],
    },

    yaxis: {
      labels: {
        show: true,
        style: {
          colors: labelColor,
          fontSize: '12px',
        },
      },
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

    tooltip: {
      style: {
        fontSize: '12px',
      },
      y: {
        formatter: function (val: number) {
          return val;
        },
      },
      marker: {
        show: false,
      },
    },

    colors: ['transparent'],
    markers: {
      colors: [color],
      strokeColors: [strokeColor],
      strokeWidth: 3,
    },
  };
}
