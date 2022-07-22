import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
  
})



export class AppComponent {
  
  // data:any;
  // ngOnInit(): void {
  //  this.data=this.chkclick1()
  // }

  title = 'AngularProject';
  // member : string[] = [];
  templ : string[] = [];
  
 member:any=[]
  




  async chkclick(){
    let url="https://localhost:8000/api/User/GetAllUsers"
    let res=await fetch(url)
    let apires;
    apires=await res.json()
    console.log(apires)
  }

  async chkclick1(){
    let url="https://localhost:8000/api/JourneyTickets/GetAllRequests"
    let obj={
      // "id":"02-07-2022-20-43-35",
      "id":"",
      "loginId": "employee0015@employee.com"
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
    this.member=apires
    console.log(this.member)
    // return apires
   // console.log(apires)
    // for(let res in apires)
    // {
      
    //   console.log(`${res}= ${apires[res]}`);
      
    // }
    // for (let key in apires) { 
      
    //   // let value='';
    
    //   // get the value
    //   let value = apires[key];
    //   console.log(value);
    //   this.member.push(value.source,value.destination,value.travelDate)
      
    //   // this.member.push(this.templ)
    //  // console.log(this.member)

    // //  let a= this.member.push(value);
    // //   console.log(this.member.push(value));
      
  
    //   //console.log(key + " - " +  value); 
      
    // }
    // console.log(this.member);
    
    
  }
  async chkclick2(){
    let url="https://localhost:8000/api/Authenticate/login"
    let obj={
      "password": "@Employee0015",
      "loginId": "Employee0015@employee.com"
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
 
    }
  }