import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostMediaChangeComponent } from './post-media-change.component';

describe('PostMediaChangeComponent', () => {
  let component: PostMediaChangeComponent;
  let fixture: ComponentFixture<PostMediaChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostMediaChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostMediaChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
