import { Component, ChangeDetectionStrategy, signal, inject, Signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-simple-get',
  template: `
    <button 
      type="button" 
      (click)="onGetClick()" 
      [disabled]="loading()"
      class="get-btn"
      [style.backgroundColor]="'#1976d2'"
      [style.color]="'#fff'"
      [style.padding]="'0.5rem 1rem'"
      [style.border]="'none'"
      [style.borderRadius]="'4px'"
      [style.cursor]="loading() ? 'not-allowed' : 'pointer'"
    >
      {{ loading() ? 'Loading...' : 'Get Data' }}
    </button>

    <div>
      @if(message){
        <p>{{message}}</p>
      }
    </div>
    <!-- <div {{@if="result() as data" style.marginTop="'1rem'"}}>
      <pre>{{ data | json }}</pre>
    </div>
    <div @if="error()" style.color="'red'">{{ error() }}</div> -->
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    class: 'simple-get-container'
  },
  standalone: true
})
export class SimpleGetComponent {
  private http = inject(HttpClient);
  
  message: string = "";
  loading = signal(false);
  result = signal<unknown | null>(null);
  error = signal<string | null>(null);

  onGetClick() {
    console.log('Get button clicked');
    
    this.loading.set(true);
    this.result.set(null);
    this.error.set(null);

    this.http.get('/api/Home/Test', { responseType: 'text' }).subscribe({
      next: data => {
        this.result.set(data);
        this.loading.set(false);
        this.message = data;
      },
      error: err => {
        this.error.set('Failed to fetch data');
        this.loading.set(false);
      }
    });
  }
}