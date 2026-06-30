import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteGenreChangeComponent } from './delete-genre-change.component';

describe('DeleteGenreChangeComponent', () => {
  let component: DeleteGenreChangeComponent;
  let fixture: ComponentFixture<DeleteGenreChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteGenreChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteGenreChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
