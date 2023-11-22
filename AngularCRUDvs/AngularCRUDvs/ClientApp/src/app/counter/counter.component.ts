import { Component } from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  innerWidth: number = 0;
  innerHeight: number = 0;

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight - 100; //screen.height;

    // console.log("counter this.innerHeight", this.innerHeight);

  }

  public incrementCounter() {
    this.currentCount++;
  }
}

