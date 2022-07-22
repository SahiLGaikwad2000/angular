import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent  {
  members1:any
  select:any

  async Add(element:any){
    // GlobalConstants.display=true
//     this.select = document.getElementById('sel');
// var value = this.select.options[this.select.selectedIndex].value;
    
      // this.router.navigateByUrl("/register")
  
      let url="https://localhost:8000/api/Authenticate/register"
    let obj={
      
        "name": element.name,
        "userTypeId":element.sel,
         "userId":element.uname ,
        "password":element.pass,
         "loginId":element.email
      }
      console.log(element.selectedIndex);
      
      console.log(element.uname);
      console.log(element.pass);
      console.log(element.email);
      console.log(element.name);
      
    
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
    console.log("iiiiiiiiii");
    
     console.log(apires);
    // this.members1=apires
    // console.log(this.members1)
    GlobalConstants.display=false

     this.router.navigateByUrl("/admin")
  }
  // toggle(){
  //   this.router.navigateByUrl("/admin")

  // }
  


  
 
  constructor(private router: Router) {}

  ngOnInit(): void {
    GlobalConstants.display=true
  }

}
