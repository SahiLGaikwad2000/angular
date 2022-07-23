import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrls: ['./adminlogin.component.css']
})
export class AdminloginComponent implements OnInit {
  members:any
  members1:any
  display=GlobalConstants.display


toggledisplay(){
  this.display=true
}

editchk(element:any){
  GlobalConstants.detail=element
  this.router.navigateByUrl('/edit')
  

}
deletechk(element:any){
  GlobalConstants.detail=element
  this.router.navigateByUrl('/delete')
  

}


 async trq(){
  

  

  let url="https://localhost:8000/api/User/GetAllRequest"
    let res=await fetch(url)
    let apires2;
    apires2=await res.json()
    console.log(apires2);
    this.members=apires2;
    
  
  }
  modifyuser(){
    this.router.navigateByUrl("/modify")
  }

  detailchk(element:any){
    GlobalConstants.detail=element
    console.log("qqqqqq");
    GlobalConstants.username=element.name;
    
    
    console.log(GlobalConstants.detail);
    

   console.log(element);
    this.router.navigateByUrl('/Details')
    
  }
  constructor(private router: Router) {}
   

  async adduser(){
    this.router.navigateByUrl("/register")

  //   let url="https://localhost:8000/api/Authenticate/register"
  // let obj={
    
  //     "name": "employee33",
  //     "userTypeId": "EMPLOYEE",
  //     "userId": "employee33",
  //     "password": "@Employee33",
  //     "loginId": "employee33@employee.com"
  //   }
  
  // let res=await fetch(url,{
  //   headers: {
  //     'Accept': 'application/json',
  //     'Content-Type': 'application/json'
  //   },
  //   method:"POST",
  //   body:JSON.stringify(obj)

    
  // })
  // let apires;
  // apires=await res.json()
  // console.log("iiiiiiiiii");
  
  //  console.log(apires);
  // this.members1=apires
  // console.log(this.members1)
}

  
ngOnInit(): void{
  this.display=GlobalConstants.display
  console.log(this.display);
  console.log(GlobalConstants.display);
  
    this.trq()
  
  
  

  // this.display=false
  
  }
}






  
    
    
    // return this.member
  

  // constructor() { }

  // ngOnInit(): void{

  // }


