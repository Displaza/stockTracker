import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";

@Injectable({
    providedIn: "root"
})
export class NewsService {
    private apiUrl = '/api/Home/getNews'; //can probably make this shared somewhere

    constructor(private http: HttpClient){}

    getNews(page: number, pageSize: number): Observable<PaginatedNews> {
        return this.http.get<PaginatedNews>(
        `${this.apiUrl}?page=${page}&pageSize=${pageSize}`
        );
    }
}

export interface PaginatedNews {
    items: NewsItem[];
    totalCount: number;
    page: number;
    pageSize: number;
}

export interface NewsItem {
    id: number;
    category: string;
    datetime: number;
    headline: string;  
    image: string;
    related: string;
    source: string;
    summary: string;
    url: string;
}