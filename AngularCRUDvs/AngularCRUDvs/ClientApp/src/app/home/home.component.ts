import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  innerWidth: number = 0;
  innerHeight: number = 0;

  constructor() {
     /* TODO document why this constructor is empty */ 
     }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight - 100; //screen.height;

    //console.log("Home this.innerHeight",this.innerHeight);

  }
}

