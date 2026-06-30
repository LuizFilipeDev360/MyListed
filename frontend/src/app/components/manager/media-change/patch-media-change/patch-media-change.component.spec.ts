import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatchMediaChangeComponent } from './patch-media-change.component';

describe('PatchMediaChangeComponent', () => {
  let component: PatchMediaChangeComponent;
  let fixture: ComponentFixture<PatchMediaChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatchMediaChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatchMediaChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
