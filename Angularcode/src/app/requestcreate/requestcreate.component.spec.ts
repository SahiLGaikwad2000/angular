import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestcreateComponent } from './requestcreate.component';

describe('RequestcreateComponent', () => {
  let component: RequestcreateComponent;
  let fixture: ComponentFixture<RequestcreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestcreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestcreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
