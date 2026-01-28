import {Component, inject, signal} from "@angular/core";
import {CommonModule} from "@angular/common";
import {StockSymbol} from "../../shared/models/symbol";
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
    selector: 'symbol-search',
    template: `
        <h2>Symbol Search</h2>
        Search: <input type="text" [formControl]="searchControl">
        <button type="button" (click)="searchSymbol()">Submit</button>

        @if (displayResponse() !== []){
            <!-- <p>Response: {{displayResponse()}}</p> -->
             <div class="card-grid">
                @for (symbol of displayResponse(); track $index) {
                    <!-- <li>{{symbol.description}}</li> -->
                     <!-- <p>{{$index}}<\p> -->
                     <!-- <li>{{symbol.description}}</li> -->

                    <!-- <div class="card" style="margin-bottom: 10px; padding: 10px; border: 1px solid #000000ff; border-radius: 5px; background-color: #000000ff;"> -->
                    <div class="card">
                        <p><strong>Description:</strong> {{symbol.description}}</p>
                        <p class="type">Type: {{symbol.type}}</p>
                    </div>   
                }
            </div>
        }
    `,
    imports: [ReactiveFormsModule],
    styleUrls: ['./symbols.component.css'],
    standalone: true
})

export class SymbolsComponent {
    private http = inject(HttpClient);
    searchControl = new FormControl('');
    displayResponse = signal([] as StockSymbol[]);
    
    searchSymbol() :void {
        
        this.http.get<StockSymbol[]>('/api/Home/searchSymbol', {
            params: { symbol: this.searchControl.value ?? '' }
        }).subscribe({
            next: response => {
                // console.log(response)
                this.displayResponse.set(response);
                console.log(this.displayResponse());
            },
            error: error => {
            console.log('Error:', error);
        }
        })
    }
}
