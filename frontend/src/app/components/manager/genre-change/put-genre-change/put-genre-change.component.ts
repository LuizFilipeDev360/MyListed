import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-put-genre-change',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './put-genre-change.component.html',
  styleUrl: './put-genre-change.component.css'
})
export class PutGenreChangeComponent {

  id: any;

  genre: any = {
          name: ''
      };

      constructor(private genreApi: GenreService) {}

    putGenre(){

        this.id = this.id !== null ? Number(this.id) : null;

        this.genreApi.putGenre(this.genre, this.id).subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.error('Erro ao editar gênero', err);
          }
        });
      }

      loadGenre(){
           if (!this.id || this.id <= 0) {
            return;
          }

          this.genreApi.getGenreById(this.id).subscribe({
            next: (response: any) => {

              this.genre.name = response.name;
            },
            error: (err) => {
              console.log(err);
            }
          });
        }
    
}
