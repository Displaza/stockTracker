import { Component, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { NewsItem, NewsService } from '../../core/news.service';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'market-news',
    template: `
        <div class="news-feed">

  @for (item of news(); track item.id) {
    <div class="news-card">

      <div class="vote-bar"></div>

      <div class="content">
        <a [href]="item.url" class="headline" target="_blank" rel="noopener">
          {{ item.headline }}
        </a>

        <div class="meta">
          <span class="category">{{ item.category }}</span>
          •
          <span class="date">
            {{ item.datetime * 1000 | date:'medium' }}
          </span>
          •
          <span class="source">{{ item.source }}</span>
        </div>

        <p class="summary">{{ item.summary }}</p>
      </div>

      @if (item.image) {
        <img [src]="item.image" alt="" class="thumbnail" />
      }

    </div>
  }

  <!-- Pagination -->
  <div class="pagination">
    <button (click)="onPageChange(page() - 1)" [disabled]="page() === 1">
      Previous
    </button>

    <span>
      Page {{ page() }} of {{ totalPages }}
    </span>

    <button (click)="onPageChange(page() + 1)" 
            [disabled]="page() * pageSize() >= totalCount()">
      Next
    </button>
  </div>

</div>
    `,
    styleUrls: ['./marketNews.component.css'],
    standalone: true,
    imports: [DatePipe]})

export class MarketNewsComponent {
    //injecting is the more modern approach of DI.
    private newsService = inject(NewsService);

    page = signal(1);
    pageSize = signal(10);
    news = signal<NewsItem[]>([]);
    totalCount = signal(0);
    
    constructor() {
        this.loadNews(); // Load initial data
    }
    
    loadNews(): void {
        this.newsService.getNews(this.page(), this.pageSize()).subscribe(response => {
        this.news.set(response.items);
        this.totalCount.set(response.totalCount);
        });
    }
    
    onPageChange(newPage: number): void {
        this.page.set(newPage);
        this.loadNews();
    }

    get totalPages(): number{
        return Math.ceil(this.totalCount() / this.pageSize());
    }
}