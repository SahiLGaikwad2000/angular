import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalConstants } from '../common/global-constants';

@Component({
  selector: 'app-modifyuser',
  templateUrl: './modifyuser.component.html',
  styleUrls: ['./modifyuser.component.css']
})
export class ModifyuserComponent implements OnInit {
  members:any
  async Modify(){
    let url="https://localhost:8000/api/User/GetAllUsers"
    let res=await fetch(url)
    let apires2;
    apires2=await res.json()
    console.log(apires2);
    this.members=apires2;
    console.log(this.members);
    

  }
  deletechk(element:any){
    GlobalConstants.detail=element

   // console.log(element);
     this.router.navigateByUrl('/deleteadmin')
  }

  editchk(element:any){
    GlobalConstants.detail=element
    this.router.navigateByUrl("/editadmin")

  }

  constructor(private router: Router) {}

  ngOnInit(): any {
   this.Modify()

    
  }

}
