import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MediaService } from '../../../services/media.service';
import { GenreService } from '../../../services/genre.service';
import { PostMediaChangeComponent } from "./post-media-change/post-media-change.component";
import { PutMediaChangeComponent } from './put-media-change/put-media-change.component';
import { PatchMediaChangeComponent } from './patch-media-change/patch-media-change.component';
import { DeleteMediaChangeComponent } from './delete-media-change/delete-media-change.component';

@Component({
  selector: 'app-media-change',
  standalone: true,
  imports: [FormsModule, PostMediaChangeComponent,PutMediaChangeComponent,PatchMediaChangeComponent,DeleteMediaChangeComponent],
  templateUrl: './media-change.component.html',
  styleUrl: './media-change.component.css'
})
export class MediaChangeComponent {
  openSection: string | null = null;

  toggle(section: string) {
    this.openSection =
      this.openSection === section ? null : section;
  }
}

