import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PutGenreChangeComponent } from './put-genre-change.component';

describe('PutGenreChangeComponent', () => {
  let component: PutGenreChangeComponent;
  let fixture: ComponentFixture<PutGenreChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PutGenreChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PutGenreChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
