import { Component, OnInit } from '@angular/core';
import { UserMediaService } from '../../services/user-media.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-list.component.html',
  styleUrl: './my-list.component.css'
})
export class MyListComponent implements OnInit{
  
  UserMediaList: any[] = [];

  constructor(private api: UserMediaService, private router: Router) {}

  ngOnInit() {
    console.log('ENTROU NO MY LIST'); // 

    this.api.getUserMedia().subscribe((data: any) => {
      this.UserMediaList = data;
      console.log(data);
    });
  }

  goToMedia(id: number) {
    this.router.navigate(['/media', id]);
  }
}
