import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostGenreChangeComponent } from './post-genre-change.component';

describe('PostGenreChangeComponent', () => {
  let component: PostGenreChangeComponent;
  let fixture: ComponentFixture<PostGenreChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostGenreChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostGenreChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
