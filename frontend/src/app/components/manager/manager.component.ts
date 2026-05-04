import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MediaService } from '../../services/media.service';
import { GenreService } from '../../services/genre.service';

@Component({
  selector: 'app-manager',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './manager.component.html',
  styleUrl: './manager.component.css'
})
export class ManagerComponent {

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

  postMedia(){

    this.media.year = this.media.year !== null ? Number(this.media.year) : null;
    this.media.description = this.media.description?.trim() || null;
    this.media.imageUrl = this.media.imageUrl?.trim() || null;

    this.mediaApi.postMedia(this.media).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.error('Erro ao postar mídia', err);
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

}
