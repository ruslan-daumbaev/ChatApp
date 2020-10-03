import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
  NbButtonModule,
  NbCardModule,
  NbChatModule,
  NbLayoutModule,
  NbThemeModule,
} from '@nebular/theme';
import { ChatRoomComponent } from './components/chat-room/chat-room.component';
import { MessagesService } from './services/messages.service';
import { NotificationsService } from './services/notifications.service';
import { FormsModule } from '@angular/forms';
import { InfiniteScrollChatComponent } from './components/chat-window/chat-window.component';

@NgModule({
  declarations: [AppComponent, ChatRoomComponent, InfiniteScrollChatComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NbChatModule,
    NbCardModule,
    NbLayoutModule,
    NbButtonModule,
    NbThemeModule.forRoot({ name: 'default' }),
  ],
  providers: [MessagesService, NotificationsService],
  bootstrap: [AppComponent],
})
export class AppModule {}
