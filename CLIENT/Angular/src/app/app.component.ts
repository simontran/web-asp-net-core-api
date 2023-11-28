import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SignalRClient';
    private hubConnectionBuilder!: HubConnection;
    offers: any[] = [];
    constructor() {}
    ngOnInit(): void {
        this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('http://192.168.21.116:11111/offers').configureLogging(LogLevel.Information).build();
        this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
        this.hubConnectionBuilder.on('SendOffersToUser', (result: any) => {
            this.offers.push(result);
        });
    }
}
