import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { IMessage } from '../models/message.model';
import { MessagesService } from '../services/messages.service';
import { NotificationsService } from '../services/notifications.service';

const USERNAME_STORAGE_KEY = 'chat-app-user';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './chat-room.component.html',
})
export class ChatRoomComponent implements OnInit, OnDestroy {
  public messages: IMessage[] = [];
  public userName = '';
  public activeUserName: string;
  public usersInChat = 0;

  private unsubscribe$ = new Subject();

  constructor(
    private messagesService: MessagesService,
    private notificationsService: NotificationsService,
    private cdr: ChangeDetectorRef
  ) {}

  public ngOnInit(): void {
    this.activeUserName = sessionStorage.getItem(USERNAME_STORAGE_KEY);
    if (this.activeUserName) {
      this.joinChat();
    }
  }

  private joinChat(): void {
    this.notificationsService.startConnection();
    this.notificationsService.connectedCountChanged$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((count) => {
        this.usersInChat = count;
        this.cdr.markForCheck();
      });
    this.messagesService
      .getMessages()
      .pipe(take(1))
      .subscribe((data) => {
        this.messages = data;
        this.notificationsService.messageReceived$
          .pipe(takeUntil(this.unsubscribe$))
          .subscribe((message) => {
            this.messages.push(message);
            this.cdr.markForCheck();
          });
        this.cdr.markForCheck();
      });
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.unsubscribe();
    this.notificationsService.stopConnection();
  }

  public sendMessage(arg: { message: string }): void {
    this.messagesService
      .sendMessage(arg.message, this.activeUserName)
      .pipe(take(1))
      .subscribe();
  }

  public onJoinClick(): void {
    this.activeUserName = this.userName;
    sessionStorage.setItem(USERNAME_STORAGE_KEY, this.activeUserName);
    this.joinChat();
  }
}
