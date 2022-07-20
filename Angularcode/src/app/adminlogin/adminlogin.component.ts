import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrls: ['./adminlogin.component.css']
})
export class AdminloginComponent implements OnInit  {
  username=GlobalConstants.username
  member:any=[]

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
  this.member=apires
}

detailchk(element:any){
  GlobalConstants.detail=element

 console.log(element);
  this.router.navigateByUrl('/Details')
  
}
editchk(element:any){
  GlobalConstants.detail=element
  if(GlobalConstants.detail.currentStatus=="SUBMITTED"){
  
  
  this.router.navigateByUrl('/edit')
  }

}
adduser(){
  // GlobalConstants.detail=element
  
  
  
  this.router.navigateByUrl('/register')
  }

constructor(private router: Router) {}




// constructor(private router: Router) {}

  // trq():  any {
  //   console.log(GlobalConstants.username)
  //   this.chkclick1()
   
  // }
  ngOnInit(){
    console.log(GlobalConstants.username)
    this.chkclick1()

  }
}
  


  

  


