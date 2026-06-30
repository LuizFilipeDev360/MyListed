import { Component } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Router } from '@angular/router';
import { MediaService } from '../../services/media.service';

@Component({
  selector: 'app-results',
  standalone: true,
  imports: [],
  templateUrl: './results.component.html',
  styleUrl: './results.component.css'
})
export class ResultsComponent {
  
  mediaList: any;

  constructor(private route: ActivatedRoute, private api: MediaService, private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const term = params['search_query'] || '';

      this.api.getMediaByString(term).subscribe((data: any) => {
        this.mediaList = data;
        console.log(data);
      });
    });
  }

  goToMedia(id: number) {
    this.router.navigate(['/media', id]);
  }
}
