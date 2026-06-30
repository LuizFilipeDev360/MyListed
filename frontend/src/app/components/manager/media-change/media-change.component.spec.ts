import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaChangeComponent } from './media-change.component';

describe('MediaChangeComponent', () => {
  let component: MediaChangeComponent;
  let fixture: ComponentFixture<MediaChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediaChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediaChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
