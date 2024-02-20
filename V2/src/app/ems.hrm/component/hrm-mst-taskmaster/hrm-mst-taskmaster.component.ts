import { Component, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../../ems.utilities/services/socket.service';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-hrm-mst-taskmaster',
  templateUrl: './hrm-mst-taskmaster.component.html',
  styleUrls: ['./hrm-mst-taskmaster.component.scss']
})
export class HrmMstTaskmasterComponent {

  addtaskFormData  = { 
    txt_task_name: '',
    txt_task_description: '',
    cboteam:{
      team_name:'',
      team_gid:''
    },


  };
  edittaskFormData = {
    txt_edit_task_name: '',
    txttask_code: '',
    txt_edit_task_description:'',
    cboteamedit:{
      team_name:'',
      team_gid:''

    },
    

  };

  
  statusFormData = {
    txttask_name: '',
    rbo_status: '',
    txtremarks: '',

  };
  
  TaskAddForm!: FormGroup;
  stsform!: FormGroup;
  task_list: any;
  task_gid: any; 
  team_gid:any;
  TaskEditForm: any; 
  parametervalue:any;
  taskinactivelog_list: any;
  assignedto_list: any;
  team_list: any;
  SocketService: any;

  constructor(private service: SocketService,private el: ElementRef,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService, private FormBuilder: FormBuilder) {
  
    this.createtaskAddForm(); 
    this.createtaskeditForm();
    this.createstsform();  
  } 


  createtaskAddForm(){

    this.TaskAddForm = this.FormBuilder.group({

      txt_task_name: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],

      // txt_task_description: ['',[Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txt_task_description: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),

      cboteam:['',Validators.required],

    });

 

  }

  createtaskeditForm(){ 
    this.TaskEditForm = this.FormBuilder.group({
      txt_edit_task_name: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]], 
      // txt_edit_task_description: ['',[Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txt_edit_task_description: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      txttask_code: [''],
      cboteamedit:['',Validators.required],  
       
      

    }); 
  }
  createstsform(){
    this.stsform=this.FormBuilder.group({
      // txtremarks: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txtremarks: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      rbo_status: [''] 
    })
  }

  ngOnInit() {
    debugger
    var url= 'HrmMaster/GetTaskSummary';
    this.NgxSpinnerService.show();
    this.SocketService.get(url).subscribe((result:any)=>{
      this.NgxSpinnerService.hide();
      this.task_list= result.master_list;  
     
    });
    
    var url = 'EmployeeOnboard/GetTeamList';
    this.SocketService.get(url).subscribe((result: any) => {
      this.team_list = result.teamlist ;
    });
  }  

  addtask(){ 
    
    var params = {
      task_name: this.addtaskFormData.txt_task_name,
      task_description: this.addtaskFormData.txt_task_description,
      team_name:this.addtaskFormData.cboteam.team_name,
      team_gid:this.addtaskFormData.cboteam.team_gid



    }
    
    this.NgxSpinnerService.show();
      var url = 'HrmMaster/PostTaskAdd';
      this.SocketService.post(url,params).subscribe((result:any) => {
        if(result.status == true){
          this.ToastrService.success(result.message);
          this.NgxSpinnerService.hide();
          this.ngOnInit();
         
        }
        else {
          this.ToastrService.warning(result.message);
          this.NgxSpinnerService.hide();
        }
        
      })
    }
  edittask(task_gid:any ){
    this.task_gid = task_gid;
    
    this.NgxSpinnerService.show();
     var params = {
        task_gid: task_gid,
        
    }

    var url = 'HrmMaster/EditTask';
    this.SocketService.getparams(url, params).subscribe((result:any) => {
          this.edittaskFormData.txt_edit_task_name = result.task_name;
          this.edittaskFormData.txttask_code = result.task_code;
          this.edittaskFormData.cboteamedit={team_name: result.team_name,
            team_gid: result.team_gid}
          this.edittaskFormData.txt_edit_task_description = result.task_description;
          this.task_gid = result.task_gid;
         
          this.NgxSpinnerService.hide();
          console.log(result)
          
   
    });

    }

  update(){
     
    var params = {
      task_gid: this.task_gid,
      task_name: this.edittaskFormData.txt_edit_task_name,
      task_code: this.edittaskFormData.txttask_code,
      team_name:this.edittaskFormData.cboteamedit.team_name,
      team_gid:this.edittaskFormData.cboteamedit.team_gid,
      task_description: this.edittaskFormData.txt_edit_task_description, 

    }
    this.NgxSpinnerService.show();
    var url = 'HrmMaster/UpdateTask';
    this.SocketService.post(url, params).subscribe((result:any) => {    
    if(result.status == true){
      this.NgxSpinnerService.hide();
      this.ToastrService.success(result.message);
      this.ngOnInit();
      }
      else {
        this.ToastrService.warning(result.message);
        this.NgxSpinnerService.hide(); 
      }   
    })
    }

    deletetask(parameter:any){
      this.parametervalue = parameter
    }
    ondelete(){
      
      this.NgxSpinnerService.show();
      var params = {
        task_gid: this. parametervalue
    }
      var url = 'HrmMaster/DeleteTask';
      this.SocketService.getparams(url, params).subscribe((result:any) => {
        if(result.status == true){
          this.NgxSpinnerService.hide();
          this.ToastrService.success("Task Deleted Successfully");
          this.ngOnInit();
            }
            else {
              this.ToastrService.warning("Error Occurred ");
          this.NgxSpinnerService.hide(); 
            }   
          })
    }

    AddTask(){
      this.TaskAddForm.reset();
    }

    Status_update(task_gid:any){
      this.task_gid = task_gid
      this.stsform.reset();
      var params = {
        task_gid: task_gid
    }
    this.NgxSpinnerService.show();
    var url = 'HrmMaster/EditTask';
    this.SocketService.getparams(url, params).subscribe((result:any) => {
      this.task_gid = result.task_gid
      this.statusFormData.txttask_name = result.task_name;
      this.statusFormData.rbo_status = result.Status;
      this.NgxSpinnerService.hide();     
    });
    var url ='HrmMaster/TaskInactiveLogview'
    this.SocketService.getparams(url, params).subscribe((result:any) => {
      this.taskinactivelog_list = result.master_list;
    });
    }
    update_status(){
 
      this.NgxSpinnerService.show();
      var params = {
        task_gid : this.task_gid,
        remarks: this.statusFormData.txtremarks,
        rbo_status:this.statusFormData.rbo_status
    }
    var url = 'HrmMaster/InactiveTask';
    this.SocketService.post(url, params).subscribe((result:any) => {
      console.log(result.status)
      this.NgxSpinnerService.hide();
        if(result.status == true){
          this.ToastrService.success(result.message)
          this.NgxSpinnerService.hide();
            }
            
            else {
              this.ToastrService.info(result.message)
              this.NgxSpinnerService.hide();
            }  
            this.ngOnInit()
                 
        })
    
    };

   
}


