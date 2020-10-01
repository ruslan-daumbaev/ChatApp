import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  constructor(private http: HttpClient) {}

  public getMessages(): Observable<any[]> {
    return this.http.get<any[]>(this.getUrl());
  }

  public sendMessage(messageText: string, user: string): Observable<any> {
    return this.http.post(this.getUrl(), {
      messageText,
      user,
    });
  }

  private getUrl(): string {
    return `${API_URL}messages`;
  }
}
