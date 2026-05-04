import { Component, OnInit } from '@angular/core';
import { MediaService } from '../../services/media.service';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  mediaList: any[] = [];

  constructor(private api: MediaService, private router: Router,   private route: ActivatedRoute) {}

  ngOnInit() {
    this.api.getMedia().subscribe((data: any) => {
      this.mediaList = data;
      console.log(data); // 👈 importante pra debug
    });
  }

  goToMedia(id: number) {
    this.router.navigate(['/media', id]);
  }

}
