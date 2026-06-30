import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MediaService } from '../../services/media.service';
import { UserMediaService } from '../../services/user-media.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-media',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './media.component.html',
  styleUrl: './media.component.css'
})
export class MediaComponent {

  constructor(private route : ActivatedRoute, private mediaApi: MediaService, private userMediaApi: UserMediaService) {}

  media: any;

  mediaUser: any;

  isEditModalOpen = false;

  postMediaId = {
    mediaId: 0
  };

  likeDto = {
    liked: false
  };

  watchDto = {
    watched: false
  };

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

  addToMyList(){
    const id = this.route.snapshot.paramMap.get('id');

    this.postMediaId.mediaId = Number(id);

    this.userMediaApi.postUserMedia(this.postMediaId).subscribe({
          next: (res) => {
            console.log(res);
            window.location.reload();
          },
          error: (err) => {
            console.error('Erro ao adicionar no MyList', err);
          }
        });
  }

  openEditModal() {
    this.isEditModalOpen = true;
  }

  closeEditModal() {
    this.isEditModalOpen = false;
  }

  likeMedia(){
      const id = this.route.snapshot.paramMap.get('id');

      this.likeDto.liked = !this.mediaUser.liked;

      this.userMediaApi.patchUserMedia(Number(id), this.likeDto)
        .subscribe({
          next: (res) => {
            this.mediaUser.liked = this.likeDto.liked;
            console.log(res);
          },
          error: (err) => {
            console.error(err);
          }
        });
  }
  watchMedia(){
      const id = this.route.snapshot.paramMap.get('id');

      this.watchDto.watched = !this.mediaUser.watched;

      this.userMediaApi.patchUserMedia(Number(id), this.watchDto)
        .subscribe({
          next: (res) => {
            this.mediaUser.watched = this.watchDto.watched;
            console.log(res);
          },
          error: (err) => {
            console.error(err);
          }
        });
  }

  editUserMedia(){
    const id = this.route.snapshot.paramMap.get('id');
    
    this.userMediaApi.patchUserMedia(Number(id),this.mediaUser).subscribe({
            next: (res) => {
              this.closeEditModal();
              console.log(res);
            },
            error: (err) => {
              console.error('Erro ao editar mídia', err);
              console.log(err.error.errors);
            }
          });
  }

  deleteUserMedia(){
    const id = this.route.snapshot.paramMap.get('id');
    
    this.userMediaApi.deleteUserMedia(Number(id)).subscribe({
            next: (res) => {
              console.log(res);
              window.location.reload();
            },
            error: (err) => {
              console.error('Erro ao remover da lista', err);
              console.log(err.error.errors);
            }
          });
  }
}
