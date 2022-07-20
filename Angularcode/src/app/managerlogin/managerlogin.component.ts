import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-managerlogin',
  templateUrl: './managerlogin.component.html',
  styleUrls: ['./managerlogin.component.css']
})
export class ManagerloginComponent implements OnInit {
  username=GlobalConstants.username
  member:any=[]
  display=GlobalConstants.display

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



constructor(private router: Router) {}

  ngOnInit():  any {
    console.log(GlobalConstants.username)
    this.chkclick1()
   // this.checklog()
    
   //  return this.member
  }
  }


