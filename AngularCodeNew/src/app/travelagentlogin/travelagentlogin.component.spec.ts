import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TravelagentloginComponent } from './travelagentlogin.component';

describe('TravelagentloginComponent', () => {
  let component: TravelagentloginComponent;
  let fixture: ComponentFixture<TravelagentloginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TravelagentloginComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TravelagentloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
