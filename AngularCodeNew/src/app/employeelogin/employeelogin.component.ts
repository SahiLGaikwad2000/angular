import { Component, Input, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employeelogin',
  templateUrl: './employeelogin.component.html',
  styleUrls: ['./employeelogin.component.css']
})
export class EmployeeloginComponent implements OnInit {
  
 username=''

display=GlobalConstants.display
//  $scope.disableTagButton = {'visibility': 'hidden'};

  // constructor() {
   
  //   // console.log(GlobalConstants.username);
    
  //   // this.username=GlobalConstants.username
  //  }
  constructor(private router: Router) {}
  templ : string[] = [];
  
 
 member:any=[]
//  this.username=GlobalConstants.username

checklog(){
  console.log("checked");
  
  this.username=GlobalConstants.username
  
}


 async chkclick1(){
  let url="https://localhost:8000/api/JourneyTickets/GetAllRequests"
  let obj={
    // "id":"02-07-2022-20-43-35",
    "id":"",
    "loginId": GlobalConstants.auth
  }
  let res=await fetch(url,{
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    method:"POST",
    body:JSON.stringify(obj)

    
  })
  let apires;
  apires=await res.json()
  // console.log(apires);
  this.member=apires
  console.log(this.member)

  
}
ndisp(){
  this.display=false
}





trq():any{
  console.log("inside trq");
  
  this.display=true
  console.log(GlobalConstants.username)
  this.chkclick1()
  
  
  return this.member
}

  ngOnInit(): any {
    // if(GlobalConstants.display){
      this.chkclick1()
    // }
    // console.log(GlobalConstants.username)
    // this.chkclick1()
    // this.display=GlobalConstants.display
    console.log(GlobalConstants.display)
    this.checklog()
    
    // return this.member
  }

  detailchk(element:any){
    GlobalConstants.detail=element

   console.log(element);
    this.router.navigateByUrl('/Details')
    
  }

  deletechk(element:any){
    GlobalConstants.detail=element

   // console.log(element);
     this.router.navigateByUrl('/delete')
  }








}
