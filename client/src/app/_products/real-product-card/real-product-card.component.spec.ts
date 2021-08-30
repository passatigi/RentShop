import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealProductCardComponent } from './real-product-card.component';

describe('RealProductCardComponent', () => {
  let component: RealProductCardComponent;
  let fixture: ComponentFixture<RealProductCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RealProductCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RealProductCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
