import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminloginComponent } from './adminlogin/adminlogin.component';
import { AuthComponent } from './auth/auth.component';
import { DeleteComponent } from './delete/delete.component';
import { DetailsComponent } from './details/details.component';
import { DummyComponent } from './dummy/dummy.component';
import { EditComponent } from './edit/edit.component';
import { EmployeeloginComponent } from './employeelogin/employeelogin.component';
import { ManagerloginComponent } from './managerlogin/managerlogin.component';
import { RegisterComponent } from './register/register.component';
import { RequestcreateComponent } from './requestcreate/requestcreate.component';
import { TravelagentloginComponent } from './travelagentlogin/travelagentlogin.component';

const routes: Routes = [
  {path:'login',component:EmployeeloginComponent},
  {path:'',component:DummyComponent},
  {path:'Createreq',component:RequestcreateComponent},
  {path:'Auth',component:AuthComponent},
  {path:'Details',component:DetailsComponent},
  {path:'delete',component:DeleteComponent},
  {path:'manager',component:ManagerloginComponent},
  {path:'travelagent',component:TravelagentloginComponent},
  {path:'edit',component:EditComponent},
  {path:'admin',component:AdminloginComponent},
  {path:'register',component:RegisterComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingcomp=[EmployeeloginComponent,DummyComponent,RequestcreateComponent,AuthComponent,DetailsComponent,DeleteComponent,ManagerloginComponent,TravelagentloginComponent,EditComponent,AdminloginComponent,RegisterComponent]
