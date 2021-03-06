import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { IMessage } from '../models/message.model';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  constructor(private http: HttpClient) {}

  public getMessages(pageSize: number, anchorId?: number): Observable<IMessage[]> {
    let params = new HttpParams().set('pageSize', pageSize.toString());
    if (anchorId) {
      params = params.set('anchorMessage', anchorId.toString());
    }
    return this.http.get<IMessage[]>(this.getUrl(), { params });
  }

  public sendMessage(messageText: string, user: string): Observable<IMessage> {
    return this.http.post<IMessage>(this.getUrl(), {
      messageText,
      user,
    });
  }

  private getUrl(): string {
    return `${API_URL}messages`;
  }
}
