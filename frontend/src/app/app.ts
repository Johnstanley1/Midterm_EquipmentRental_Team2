import {Component, signal} from '@angular/core';
import { RouterOutlet, RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {NavBar} from './nav-bar/nav-bar';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, RouterModule, NavBar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('equipment-rental-frontend');
}
