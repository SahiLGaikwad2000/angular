import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
empdetail:any
usrname:any

constructor(private router: Router) {}

 
  
  ngOnInit(): void {
    GlobalConstants.display=true
    this.empdetail=GlobalConstants.detail
  
    
    console.log("in details");
    
    console.log(this.empdetail)
this.usrname=GlobalConstants.username
console.log(this.usrname);


  }

  route(){
   // console.log(this.try);
    if(GlobalConstants.usertypeid=="EMPLOYEE"){
      // GlobalConstants.display=true
      console.log("details emp");
      
      console.log(GlobalConstants.detail)
      this.router.navigateByUrl('/login')

    }
    if(GlobalConstants.usertypeid=="MANAGER"){
      // GlobalConstants.display=true
      this.router.navigateByUrl('/manager')

    }
    if(GlobalConstants.usertypeid=="TRAVEL_AGENT"){
      this.router.navigateByUrl('/travelagent')

    }
    if(GlobalConstants.usertypeid=="ADMIN"){
      this.router.navigateByUrl('/admin')

    }
    
    
  }

}
