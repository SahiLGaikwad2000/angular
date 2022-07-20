import { Component, NgModule, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';
@Component({
  selector: 'app-requestcreate',
  templateUrl: './requestcreate.component.html',
  styleUrls: ['./requestcreate.component.css']
})
export class RequestcreateComponent {
  
  

  // constructor() { }
  constructor(private router: Router) {}
  // handledata(data:any) { 
    
  //   // let d=document.getElementById('source').value;
  //   console.log(data);
  //   GlobalConstants.message="changed"
    
    
  // }
  async chkclick(data:any){
    let userid='';
    let userName='';
    let loginid='';
    let url="https://localhost:8000/api/User/GetAllUsers"
    let res=await fetch(url)
    let apires;
    apires=await res.json()
    console.log(apires)
    console.log(GlobalConstants.auth)
    for (const employee in apires){
      if (apires[employee].userName==GlobalConstants.auth){
        console.log("inside if")
         userid=apires[employee].userId
        userName=apires[employee].name
        loginid=apires[employee].loginId
        
      }
      
      

    }





    let url1="https://localhost:8000/api/JourneyTickets/CreateRequest"
    let obj={
      "requestId": "",
      "source": data.source,
      "destination": data.destination,
      "userId": userid,
      "name":userName,
      "travelDate": data.traveldate,
      "bookingTime": "2022-07-17T12:02:35.338Z",
      "loginId": loginid,
      "travelMode": data.mode,
      "currentStatus": "SUBMITTED"
    }
    let res1=await fetch(url1,{
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      method:"POST",
      body:JSON.stringify(obj)
  
      
    })
    let apires1;
    apires1=await res1.json()
    console.log(apires1);
    this.router.navigateByUrl('/login')
   


















    

  }
  ngOnInit(): any {
    GlobalConstants.display=true
  }

}
