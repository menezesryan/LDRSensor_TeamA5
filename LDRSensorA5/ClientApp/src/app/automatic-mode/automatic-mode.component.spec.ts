import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomaticModeComponent } from './automatic-mode.component';

describe('AutomaticModeComponent', () => {
  let component: AutomaticModeComponent;
  let fixture: ComponentFixture<AutomaticModeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutomaticModeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomaticModeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
