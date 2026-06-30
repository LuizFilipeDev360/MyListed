import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-post-genre-change',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './post-genre-change.component.html',
  styleUrl: './post-genre-change.component.css'
})
export class PostGenreChangeComponent {

  genre: any = {
          name: ''
      };

      constructor(private genreApi: GenreService) {}

    postGenre(){

        this.genreApi.postGenre(this.genre).subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.error('Erro ao postar gênero', err);
          }
        });
      }
    
}
