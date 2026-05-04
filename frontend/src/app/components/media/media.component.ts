import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MediaService } from '../../services/media.service';
import { UserMediaService } from '../../services/user-media.service';

@Component({
  selector: 'app-media',
  standalone: true,
  imports: [],
  templateUrl: './media.component.html',
  styleUrl: './media.component.css'
})
export class MediaComponent {

  constructor(private route : ActivatedRoute, private mediaApi: MediaService, private userMediaApi: UserMediaService) {}

  media: any;

  mediaUser: any;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    if(id){
      this.mediaApi.getMediaById(Number(id)).subscribe((data: any) => {
        this.media = data;
        console.log(data);
      });

      this.userMediaApi.getUserMediaById(Number(id)).subscribe({
        next: (data: any) => {
          this.mediaUser = data;
          console.log('DATA:', data);
        },
        error: (err) => {
          console.log('Não encontrado', err);
          this.mediaUser = null;
        }
      });
    }
  }
}
