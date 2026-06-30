import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetGenreComponent } from './get-genre.component';

describe('GetGenreComponent', () => {
  let component: GetGenreComponent;
  let fixture: ComponentFixture<GetGenreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetGenreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetGenreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
