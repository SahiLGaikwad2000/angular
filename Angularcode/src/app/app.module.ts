import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule,routingcomp} from './app-routing.module';
import { AppComponent } from './app.component';
import { DemoPipe } from './pipes/demo.pipe';
import { EmployeeloginComponent } from './employeelogin/employeelogin.component';
import { DummyComponent } from './dummy/dummy.component';
import { RequestcreateComponent } from './requestcreate/requestcreate.component';
import { AuthComponent } from './auth/auth.component';
import { DetailsComponent } from './details/details.component';
import { DeleteComponent } from './delete/delete.component';
import { ManagerloginComponent } from './managerlogin/managerlogin.component';
import { TravelagentloginComponent } from './travelagentlogin/travelagentlogin.component';
import { EditComponent } from './edit/edit.component';
import { AdminloginComponent } from './adminlogin/adminlogin.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    DemoPipe,
    routingcomp,
    DummyComponent,
    RequestcreateComponent,
    AuthComponent,
    DetailsComponent,
    DeleteComponent,
    ManagerloginComponent,
    TravelagentloginComponent,
    EditComponent,
    AdminloginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
