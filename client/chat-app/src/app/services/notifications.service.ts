import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../environments/environment.prod';
import { IMessage } from '../models/message.model';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private messageReceived = new EventEmitter<IMessage>();
  private connectionEstablished = new EventEmitter<boolean>();

  private connectionIsEstablished = false;
  private hubConnection: HubConnection;

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
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
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

  public registerOnServerEvents(callback: (n: IMessage) => any): void {
    this.hubConnection.on('Notify', (data: IMessage) => {
      callback(data);
    });
  }
}
