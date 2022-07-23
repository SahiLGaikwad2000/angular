import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from '../common/global-constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-editadmin',
  templateUrl: './editadmin.component.html',
  styleUrls: ['./editadmin.component.css']
})
export class EditadminComponent implements OnInit {
  empdetail:any
  role: string[] = [];
  async chkclick(){
  let url="https://localhost:8000/api/User/GetAllUsers"
    let res=await fetch(url)
    let apires2;
    apires2=await res.json()
    console.log("iiiiii");
    console.log(apires2);
    
    for(const emp in apires2){
      // console.log(apires2[emp].userTypeId);
      

    
    
    
    // console.log(apires2.userTypeId)
    if(apires2[emp].userTypeId=="MANAGER"){

      this.role.push(apires2[emp].name)




    }
    
  }
  console.log(this.role);
  
  }
  async updateclick(element:any){
    let url="https://localhost:8000/api/User/EditUser"
    let obj={
      "id": this.empdetail.id,
  "name": element.Name,
  "userTypeId":element.sel, 
  "loginId": this.empdetail.loginId,
  "managerUserId": element.manid
      }
      
      console.log("qazwsx");
      
      console.log(obj);
      
      
    
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
    
    //  console.log(apires);
    this.router.navigateByUrl("/modify")
    

  }



  constructor(private router: Router) {}

  ngOnInit(): void {
    GlobalConstants.display=true
    this.empdetail=GlobalConstants.detail
    console.log("in details");
    console.log(this.empdetail);
    console.log("qwertyu");
    
    this.chkclick()
  }

}
