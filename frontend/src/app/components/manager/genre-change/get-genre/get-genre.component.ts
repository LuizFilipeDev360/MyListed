import { Component } from '@angular/core';
import { GenreService } from '../../../../services/genre.service';

@Component({
  selector: 'app-get-genre',
  standalone: true,
  imports: [],
  templateUrl: './get-genre.component.html',
  styleUrl: './get-genre.component.css'
})
export class GetGenreComponent {

   genreList: any[] = [];

  constructor(private genreApi: GenreService) {}

    ngOnInit() {
      this.genreApi.getGenre().subscribe((data: any) => {
      this.genreList = data;
      console.log(data); // 👈 importante pra debug
      });
    }
}
