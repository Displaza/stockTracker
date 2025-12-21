import  {Component, inject} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'app-form',
    template: `
        Favorite Color: <input type="text" [formControl]="favoriteColorControl">
        <button type="button" (click)="submitColour()">Submit</button>
    `,
    imports: [ReactiveFormsModule],
})
export class FormComponent {
    private http = inject(HttpClient);
    favoriteColorControl = new FormControl('');

    submitColour() :void {
        console.log("Submitting...");
        const httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
            })
        };
        
        const myData = { Colour: this.favoriteColorControl.value}
        this.http.post('/api/Home/ColourPost', myData, httpOptions).subscribe({
            next: response => {
                console.log(response)
            },
            error: error => {
            console.log('Error:', error);
        }
        })
    }
    
}