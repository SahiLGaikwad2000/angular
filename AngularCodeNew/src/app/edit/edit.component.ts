import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  empdetail:any
  usrname:any
  usrid=GlobalConstants.usertypeid
   async updatechk(data:any){
    
    
    console.log("checking updatachk");
    console.log(this.empdetail.requestId);
    console.log(data.status);
    
    
    
    
  let url="https://localhost:8000/api/JourneyTickets/EditRequest"
  let obj={
    
      "requestId": this.empdetail.requestId,
      "source": "string",
      "destination": "string",
      "userId": "string",
      "name": "string",
      "travelDate": "2022-07-19T04:42:26.695Z",
      "bookingTime": "2022-07-19T04:42:26.695Z",
      "loginId": "string",
      "travelMode": "string",
      
      "currentStatus": data.status
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
  console.log(apires);
  if(data.status){

  }
 
  if(res.ok){
    this.route()
    // this.router.navigateByUrl('/manager')
  }
  else{
    this.router.navigateByUrl('/edit')
  }
  
}
route(){
  // console.log(this.try);
   if(GlobalConstants.usertypeid=="EMPLOYEE"){
     this.router.navigateByUrl('/login')

   }
   if(GlobalConstants.usertypeid=="MANAGER"){
    GlobalConstants.display=true
     this.router.navigateByUrl('/manager')

   }
   if(GlobalConstants.usertypeid=="TRAVEL_AGENT"){
    this.router.navigateByUrl('/travelagent')

  }
  if(GlobalConstants.usertypeid=="ADMIN"){
    this.router.navigateByUrl('/admin')

  }
   
   
 }

 constructor(private router: Router) {}

  ngOnInit(): void {
    GlobalConstants.display=true
    this.empdetail=GlobalConstants.detail
    console.log("in details");
    console.log(this.empdetail.name);
    

    this.usrname=GlobalConstants.username
  }
}


