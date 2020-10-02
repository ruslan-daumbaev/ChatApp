import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { IMessage } from '../models/message.model';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private hubConnection: HubConnection;
  private messageReceivedSource = new Subject<IMessage>();
  private connectedCountChangedSource = new Subject<number>();

  public messageReceived$ = this.messageReceivedSource.asObservable();
  public connectedCountChanged$ = this.connectedCountChangedSource.asObservable();

  constructor() {
    this.createConnection();
  }

  private createConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(API_URL + 'chat-ws')
      .build();
  }

  public startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.on('Notify', (data: IMessage) =>
          this.messageReceivedSource.next(data)
        );
        this.hubConnection.on('ConnectedChanged', (data: number) =>
          this.connectedCountChangedSource.next(data)
        );
      })
      .catch((err) => {
        console.log(err);
        setTimeout(() => {
          this.startConnection();
        }, 5000);
      });
  }

  public stopConnection(): void {
    this.hubConnection.stop();
  }
}
