import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteMediaChangeComponent } from './delete-media-change.component';

describe('DeleteMediaChangeComponent', () => {
  let component: DeleteMediaChangeComponent;
  let fixture: ComponentFixture<DeleteMediaChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteMediaChangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteMediaChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
