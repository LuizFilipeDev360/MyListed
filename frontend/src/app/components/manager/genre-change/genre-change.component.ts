import { Component } from '@angular/core';
import { PostGenreChangeComponent } from './post-genre-change/post-genre-change.component';
import { GetGenreComponent } from "./get-genre/get-genre.component";
import { PutGenreChangeComponent } from "./put-genre-change/put-genre-change.component";
import { DeleteGenreChangeComponent } from "./delete-genre-change/delete-genre-change.component";

@Component({
  selector: 'app-genre-change',
  standalone: true,
  imports: [PostGenreChangeComponent, GetGenreComponent, PutGenreChangeComponent, DeleteGenreChangeComponent],
  templateUrl: './genre-change.component.html',
  styleUrl: './genre-change.component.css'
})
export class GenreChangeComponent {
  openSection: string | null = null;

  toggle(section: string) {
    this.openSection =
      this.openSection === section ? null : section;
  }
}
