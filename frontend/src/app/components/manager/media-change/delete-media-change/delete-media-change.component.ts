import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MediaService } from '../../../../services/media.service';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-delete-media-change',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './delete-media-change.component.html',
  styleUrl: './delete-media-change.component.css'
})
export class DeleteMediaChangeComponent {

        id: any;
  
        genreList: any[] = [];
      
        media: any = {
            title: '',
            year: null,
            kind: null,
            description: null,
            imageUrl: null,
            genreNames: []
        };

        constructor(private mediaApi: MediaService, private genreApi: GenreService) {}


        deleteMedia(){
          this.mediaApi.deleteMedia(this.id).subscribe({
            next: (res) => {
              console.log(res);
            },
            error: (err) => {
              console.error('Erro ao editar mídia', err);
              console.log(err.error.errors);
            }
          });
        }

        loadMedia(){
           if (!this.id || this.id <= 0) {
            return;
          }

          this.mediaApi.getMediaById(this.id).subscribe({
            next: (response: any) => {

              this.media.title = response.title;
              this.media.year = response.year;

              this.media.kind = response.kind.id;

              this.media.description = response.description;
              this.media.imageUrl = response.imageUrl;

              this.media.genreNames = response.genres
                ? response.genres.map((g: any) => String(g.name))
                : [];

                console.log(this.media.genreNames)
            },
            error: (err) => {
              console.log(err);
            }
          });
        }
}
