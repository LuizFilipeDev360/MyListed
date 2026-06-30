import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreChangeComponent } from './genre-change.component';

describe('GenreChangeComponent', () => {
  let component: GenreChangeComponent;
  let fixture: ComponentFixture<GenreChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenreChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
