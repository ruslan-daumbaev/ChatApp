import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  Output,
  Renderer2,
} from '@angular/core';
import { NbChatComponent } from '@nebular/theme';
import { IMessage } from 'src/app/models/message.model';

@Component({
  selector: 'app-nb-chat',
  templateUrl: './chat-window.component.html',
})
export class InfiniteScrollChatComponent
  extends NbChatComponent
  implements AfterViewInit {
  constructor(public renderer: Renderer2) {
    super();
  }

  @Input()
  public messagesList: IMessage[] = [];

  @Output()
  public sendClick = new EventEmitter<any>();

  @Output()
  public previousPageRequested = new EventEmitter<void>();

  public ngAfterViewInit(): void {
    this.renderer.listen(
      this.scrollable['scrollable'].nativeElement,
      'scroll',
      ($event) => {
        this.loadPreviousPage($event);
      }
    );
  }

  public loadPreviousPage(event: any): void {
    if (event.srcElement.scrollTop === 0) {
      this.previousPageRequested.emit();
    }
  }

  public sendMessage(event: any): void {
    this.sendClick.emit(event);
  }
}
