import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Libro, LibroCreate } from '../models/libro.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LibroService {
  private apiUrl = `${environment.apiUrl}/Libros`;

  constructor(private http: HttpClient) {}

  getLibros(): Observable<Libro[]> {
    return this.http.get<Libro[]>(this.apiUrl);
  }

  getLibro(id: number): Observable<Libro> {
    return this.http.get<Libro>(`${this.apiUrl}/${id}`);
  }

  createLibro(libro: LibroCreate): Observable<Libro> {
    return this.http.post<Libro>(this.apiUrl, libro);
  }

  updateLibro(id: number, libro: LibroCreate): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, libro);
  }

  deleteLibro(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
