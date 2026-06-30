import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MediaService } from '../../../../services/media.service';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-put-media-change',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './put-media-change.component.html',
  styleUrl: './put-media-change.component.css'
})
export class PutMediaChangeComponent {

        id: any;
  
        genreList: any[] = [];
      
        media: any = {
            title: '',
            year: null,
            kind: 0,
            description: null,
            imageUrl: null,
            genreIds: []
        };
      
        constructor(private mediaApi: MediaService, private genreApi: GenreService) {}
      
        ngOnInit() {
          this.genreApi.getGenre().subscribe((data: any) => {
            this.genreList = data;
            console.log(data); // 👈 importante pra debug
          });
        }
      
        putMedia(){

          this.id = this.id !== null ? Number(this.id) : null;
      
          this.media.year = this.media.year !== null ? Number(this.media.year) : null;
          this.media.description = this.media.description?.trim() || null;
          this.media.imageUrl = this.media.imageUrl?.trim() || null;
      
          this.mediaApi.putMedia(this.id,this.media).subscribe({
            next: (res) => {
              console.log(res);
            },
            error: (err) => {
              console.error('Erro ao editar mídia', err);
              console.log(err.error.errors);
            }
          });
        }
      
        onGenreChange(event: any) {
          const id = Number(event.target.value);
      
          if (event.target.checked) {
            this.media.genreIds.push(id);
          } else {
            this.media.genreIds = this.media.genreIds.filter((g: number) => g !== id);
          }
      
          console.log(this.media.genreIds); // debug
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

              this.media.genreIds = response.genres
                ? response.genres.map((g: any) => Number(g.id))
                : [];
            },
            error: (err) => {
              console.log(err);
            }
          });
        }
      
}
