import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PutMediaChangeComponent } from './put-media-change.component';

describe('PutMediaChangeComponent', () => {
  let component: PutMediaChangeComponent;
  let fixture: ComponentFixture<PutMediaChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PutMediaChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PutMediaChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
