import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-delete-genre-change',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './delete-genre-change.component.html',
  styleUrl: './delete-genre-change.component.css'
})
export class DeleteGenreChangeComponent {
  
  id: any;

  genre: any = {
          name: ''
      };

      constructor(private genreApi: GenreService) {}

    deleteGenre(){

        this.id = this.id !== null ? Number(this.id) : null;

        this.genreApi.deleteGenre(this.id).subscribe({
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
