import { Component } from '@angular/core';
import {NgOptimizedImage} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-home-screen',
  imports: [
    RouterLink
  ],
  templateUrl: './home-screen.html',
  styleUrl: './home-screen.css'
})
export class HomeScreen {
}
